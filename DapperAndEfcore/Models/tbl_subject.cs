using DapperAndEfcore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DapperAndEfcore.Models
{
    public class tbl_subject
    {
        public int Id { get ; set; }
        public string sName { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string grade { get; set; } = string.Empty;
        
    }
}
