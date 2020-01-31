using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DesafioQSTI.Data.Repositories;
using DesafioQSTI.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DesafioQSTI.Models;

namespace DesafioQSTI.Data
{
    public class MySqlDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<ServicoCliente> ServicoCliente { get; set; }
        public DbSet<ExecucaoServico> ExecucaoServico { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable(nameof(Repository.Cliente));
            modelBuilder.Entity<Servico>().ToTable(nameof(Repository.Servico));
            modelBuilder.Entity<ServicoCliente>().ToTable(nameof(Repository.ServicoCliente));
            modelBuilder.Entity<ExecucaoServico>().ToTable(nameof(Repository.ExecucaoServico));

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Servico>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ServicoCliente>().Property(f => f.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ExecucaoServico>().Property(f => f.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<IdentityRole>().HasData(new {Id = "1", Name = "admin", NormalizedName = "ADMIN"});
            modelBuilder.Entity<IdentityUser>().HasData(new
            {
                Id = "1", UserName = "admin", NormalizedName = "ADMIN", EmailConfirmed = true, PhoneNumberConfirmed = true,
                TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MySql"));
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<DesafioQSTI.Models.ClienteViewModel> ClienteViewModel { get; set; }
    }
}
