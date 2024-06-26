﻿using CrudAppDotNet8.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CrudAppDotNet8.Helpers;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DataContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // in memory database used for simplicity, change to a real db for production applications
        options.UseInMemoryDatabase("TestDb")
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
    }

    public DbSet<User> Users { get; set; }
}
