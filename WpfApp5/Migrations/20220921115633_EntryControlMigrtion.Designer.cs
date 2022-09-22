﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WpfApp5.DB;

#nullable disable

namespace WpfApp5.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220921115633_EntryControlMigrtion")]
    partial class EntryControlMigrtion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WpfApp5.DB.Acaunt", b =>
                {
                    b.Property<int>("AcauntId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AcauntId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcauntId");

                    b.ToTable("Acaunts");
                });

            modelBuilder.Entity("WpfApp5.DB.EntryControl", b =>
                {
                    b.Property<int>("EntryControlId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EntryControlId"), 1L, 1);

                    b.Property<int>("AcauntId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeEntryControl")
                        .HasColumnType("datetime2");

                    b.HasKey("EntryControlId");

                    b.HasIndex("AcauntId");

                    b.ToTable("EntryControls");
                });

            modelBuilder.Entity("WpfApp5.DB.EntryControl", b =>
                {
                    b.HasOne("WpfApp5.DB.Acaunt", "Acaunt")
                        .WithMany()
                        .HasForeignKey("AcauntId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Acaunt");
                });
#pragma warning restore 612, 618
        }
    }
}
