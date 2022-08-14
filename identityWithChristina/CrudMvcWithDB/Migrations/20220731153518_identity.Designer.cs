﻿// <auto-generated />
using System;
using CrudMvcWithDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrudMvcWithDB.Migrations
{
    [DbContext(typeof(NewITI))]
    [Migration("20220731153518_identity")]
    partial class identity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CrudMvcWithDB.Models.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ManagerHireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Ins_course", b =>
                {
                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("InstructorId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("ins_Courses");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Instructor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("salary")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DeptId");

                    b.ToTable("instructors");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Stu_course", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("stu_Courses");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DeptId");

                    b.ToTable("students");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Ins_course", b =>
                {
                    b.HasOne("CrudMvcWithDB.Models.Course", "Course")
                        .WithMany("ins_Courses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrudMvcWithDB.Models.Instructor", "Instructor")
                        .WithMany("ins_Courses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Instructor", b =>
                {
                    b.HasOne("CrudMvcWithDB.Models.Department", "Department")
                        .WithMany("Instructors")
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Stu_course", b =>
                {
                    b.HasOne("CrudMvcWithDB.Models.Course", "Course")
                        .WithMany("stu_Courses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CrudMvcWithDB.Models.Student", "Student")
                        .WithMany("Stu_courses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Student", b =>
                {
                    b.HasOne("CrudMvcWithDB.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Course", b =>
                {
                    b.Navigation("ins_Courses");

                    b.Navigation("stu_Courses");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Department", b =>
                {
                    b.Navigation("Instructors");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Instructor", b =>
                {
                    b.Navigation("ins_Courses");
                });

            modelBuilder.Entity("CrudMvcWithDB.Models.Student", b =>
                {
                    b.Navigation("Stu_courses");
                });
#pragma warning restore 612, 618
        }
    }
}
