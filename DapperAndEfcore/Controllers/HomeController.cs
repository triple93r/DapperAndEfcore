using Dapper;
using DapperAndEfcore.Data;
using DapperAndEfcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace DapperAndEfcore.Controllers
{
    public class HomeController : Controller, IDisposable
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var c = _context.tbl_Subject.ToList();
            return View(c);
        }

        [HttpGet]
        public IActionResult AddEfCore()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEfCore(tbl_subject sbj)
        {
            _context.tbl_Subject.Add(sbj);
            _context.SaveChanges();
            return RedirectToAction("getAll");
        }

        [HttpGet]
        public IActionResult getAll2()
        {
            var DBCon2 = _configuration.GetSection("DBNames");
            using var conn2 = new SqlConnection(_configuration.GetConnectionString(DBCon2.Value));
            conn2.Open();
            var m1m = conn2.Query<tbl_subject>("select * from tbl_Subject", commandType: CommandType.Text);
            conn2.Close();
            //int res = conn.Execute(Sqlstr, dbparams, transaction);
            return View(m1m.ToList());
        }

        [HttpGet]
        public IActionResult AddSubject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSubject(tbl_subject sbj)
        {
            var DBCon2 = _configuration.GetSection("DBNames");
            using var conn2 = new SqlConnection(_configuration.GetConnectionString(DBCon2.Value));
            var dbparams = new DynamicParameters();
            dbparams.Add("@sName", sbj.sName, DbType.String);
            dbparams.Add("@Author", sbj.Author, DbType.String);
            dbparams.Add("@Price", sbj.Price, DbType.Decimal);
            dbparams.Add("@grade", sbj.grade, DbType.String);
            conn2.Open();
            //var Sqlstr = conn2.Query<tbl_subject>("insert into tbl_subject(sName,Author,Price,grade) values('"+sbj.sName+"','"+sbj.Author+"',"+sbj.Price+",'"+sbj.grade+"')", commandType: CommandType.Text);
            var Sqlstr = "insert into tbl_subject(sName,Author,Price,grade) values(@sName, @Author, @Price, @grade); SELECT @@IDENTITY; ";
            int res = conn2.QueryFirstOrDefault<int>(Sqlstr, dbparams);
            conn2.Close();
            
            return RedirectToAction("getAll2");
        }

        [HttpGet]
        public IActionResult EditSubject(int id)
        {
            var DBCon2 = _configuration.GetSection("DBNames");
            using var conn2 = new SqlConnection(_configuration.GetConnectionString(DBCon2.Value));
            conn2.Open();
            var b = conn2.Query<tbl_subject>("select * from tbl_Subject where Id="+id, commandType: CommandType.Text).SingleOrDefault();
            conn2.Close();
            return View(b);
        }
        [HttpPost]
        public IActionResult EditSubject(tbl_subject sbj)
        {
            var DBCon2 = _configuration.GetSection("DBNames");
            using var conn2 = new SqlConnection(_configuration.GetConnectionString(DBCon2.Value));
            var dbparams = new DynamicParameters();
            dbparams.Add("@Id", sbj.Id, DbType.Int32);
            dbparams.Add("@sName", sbj.sName, DbType.String);
            dbparams.Add("@Author", sbj.Author, DbType.String);
            dbparams.Add("@Price", sbj.Price, DbType.Decimal);
            dbparams.Add("@grade", sbj.grade, DbType.String);
            conn2.Open();
            //conn2.Query<tbl_subject>("update tbl_Subject set sName=@sName, grade=@grade where Id=@Id", commandType: CommandType.Text);
            var Sqlstr = "update tbl_Subject set sName=@sName, grade=@grade where Id=@Id;";
            int res = conn2.QueryFirstOrDefault<int>(Sqlstr, dbparams);
            conn2.Close();
            return RedirectToAction("getAll2");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
