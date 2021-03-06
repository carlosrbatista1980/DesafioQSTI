﻿// <auto-generated />
using System;
using DesafioQSTI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DesafioQSTI.Migrations
{
    [DbContext(typeof(MySqlDbContext))]
    [Migration("20200130115840_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DesafioQSTI.Data.Repository.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("DesafioQSTI.Data.Repository.ExecucaoServico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataHora");

                    b.Property<int?>("ServicoClienteId");

                    b.HasKey("Id");

                    b.HasIndex("ServicoClienteId");

                    b.ToTable("ExecucaoServico");
                });

            modelBuilder.Entity("DesafioQSTI.Data.Repository.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("DesafioQSTI.Data.Repository.ServicoCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClienteId");

                    b.Property<int?>("ServicoId");

                    b.Property<string>("Versao");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ServicoId");

                    b.ToTable("ServicoCliente");
                });

            modelBuilder.Entity("DesafioQSTI.Data.Repository.ExecucaoServico", b =>
                {
                    b.HasOne("DesafioQSTI.Data.Repository.ServicoCliente", "ServicoCliente")
                        .WithMany()
                        .HasForeignKey("ServicoClienteId");
                });

            modelBuilder.Entity("DesafioQSTI.Data.Repository.ServicoCliente", b =>
                {
                    b.HasOne("DesafioQSTI.Data.Repository.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("DesafioQSTI.Data.Repository.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId");
                });
#pragma warning restore 612, 618
        }
    }
}
