namespace HW8.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using HW8.Models;

    public partial class RecordContext : DbContext
    {
        public RecordContext()
            : base("name=RecordContext_Azure")
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TimeRecord> TimeRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
