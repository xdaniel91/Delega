﻿using Delega.Api.Database.Mapping;
using Delega.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace Delega.Api.Database
{
    public class DelegaContext : DbContext
    {
        public DbSet<Person> person { get; set; }
        public DbSet<Lawyer> lawyer { get; set; }
        public DelegaContext(DbContextOptions<DelegaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PersonMap());
            builder.ApplyConfiguration(new LawyerMap());
        }
    }
}
