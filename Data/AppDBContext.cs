using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities;
using Entities.Novelty;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=ICTAWebsiteDB; Trusted_Connection=true; TrustServerCertificate = True");
            //optionsBuilder.UseSqlServer(@"Server=db;Database=ICTAWebsiteDB;User Id=sa;Password=cOdingHm145_;TrustServerCertificate=True");
        }

        public DbSet<Auth> Users { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Regulations> Regulations { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<NoveltyFile> NoveltyFiles { get; set; }
        public DbSet<NoveltyLike> NoveltiesLikesDislikes { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public DbSet<BaseRules> BaseRules { get; set; }
        public DbSet<BaseRulesFilesPhotos> BaseRulesFilesPhotos { get; set; }
        public DbSet<CompOffer> CompOffers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}
