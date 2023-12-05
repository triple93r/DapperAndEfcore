using Dapper;
using DapperAndEfcore.Data;
using DapperAndEfcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace DapperAndEfcore.Controllers
{
    public class HomeController : Controller
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
        public IActionResult getAll()
        {
            var c = _context.tbl_Subject.ToList();
            return View(c);
        }


        public IActionResult getAll2()
        {
            var DBCon2 = _configuration.GetSection("DBNames");
            using var conn2 = new SqlConnection(_configuration.GetConnectionString(DBCon2.Value));

            var dbparams = new DynamicParameters();
            dbparams.Add("@LoginId", username, DbType.String);
            //dbparams.Add("@Pwd", UIHelper.Encrypt(Pwd),DbType.String);
            dbparams.Add("@Pwd", password, DbType.String);
            conn2.Open();
            var user = conn2.Query("sp_CheckLoginId", dbparams, commandType: CommandType.StoredProcedure).SingleOrDefault();
            conn2.Close();


            var c = _context.tbl_Subject.ToList();
            return View(c);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
