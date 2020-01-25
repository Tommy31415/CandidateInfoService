using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CandidateInfoService.Models
{

    public class CandidateContext : DbContext
    {
        public CandidateContext(DbContextOptions<CandidateContext> options)
            : base(options)
        {
        }

        public DbSet<CandidateInfo> Candidates { get; set; }

    }
}
