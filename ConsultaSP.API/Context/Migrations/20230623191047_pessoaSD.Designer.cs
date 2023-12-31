﻿// <auto-generated />
using ConsultaSP.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConsultaSP.API.Context.Migrations
{
    [DbContext(typeof(TesteContext))]
    [Migration("20230623191047_pessoaSD")]
    partial class pessoaSD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ConsultaSP.API.Entities.Pessoa", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Idade = 0,
                            Nome = "Leandro Cesar"
                        },
                        new
                        {
                            Id = 2L,
                            Idade = 0,
                            Nome = "Luciana dos Santos"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
