using DapperAndEfcore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace DapperAndEfcore.Models
{
    public class Stud_Subj
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Stud_id { get; set; }
        [Required]
        public string Subj_id { get; set; }
    }
}
