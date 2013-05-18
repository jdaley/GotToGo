using GotToGo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GotToGo
{
    public class GotToGoContext : DbContext
    {
        public GotToGoContext()
            : base(GotToGoConfig.Current.ConnectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Toilet>().Property(t => t.Lat).HasPrecision(14, 10);
            modelBuilder.Entity<Toilet>().Property(t => t.Lng).HasPrecision(14, 10);
        }

        public DbSet<Toilet> Toilets { get; set; }
    }
}