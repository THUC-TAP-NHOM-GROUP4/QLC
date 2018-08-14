﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Request.Models;

namespace Request.Models.Migrations
{
    [DbContext(typeof(RequestDbContext))]
    [Migration("20180811121444_AddRequest")]
    partial class AddRequest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Request.Models.Requests", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<string>("ContentName");

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("FinishDate");

                    b.Property<string>("Note");

                    b.Property<decimal>("Price");

                    b.Property<string>("ResquestName");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("StateId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("RequestId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Request.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
