﻿using System;
using HomeApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace HomeApi.Data
{
    public sealed class HomeApiContext : DbContext //доступ к базе с использованием Entity Framework осуществляется через контекст
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Device> Devices { get; set; }
        
        public HomeApiContext(DbContextOptions<HomeApiContext> options)  : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Room>().ToTable("Rooms");
            builder.Entity<Device>().ToTable("Devices");
        }
    }
}