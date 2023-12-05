using DapperAndEfcore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DapperAndEfcore.Models
{
    public class tblAcademics
    {
        public int Id { get; set; }
        public string Stud_id { get; set; }
        public string Class { get; set; } = string.Empty;
        public string Subject_id { get; set; } = string.Empty;

    }
}
