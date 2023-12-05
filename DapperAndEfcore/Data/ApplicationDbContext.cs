using DapperAndEfcore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DapperAndEfcore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<tbl_subject> tbl_Subject { get; set; }
        public DbSet<Stud_Subj> Stud_Subj { get; set; }
        public DbSet<tblAcademics> tblAcademics { get; set; }
    }
}
