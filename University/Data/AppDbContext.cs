using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentHead> DepartmentHeads { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Engineer> Engineers { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialty> Specialties { get; set; }

    public virtual DbSet<SpecialtySubject> SpecialtySubjects { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=UniversityApp;Username=postgres;Password=123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("Department_pkey");

            entity.ToTable("Department");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Faculty).WithMany(p => p.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Department_FacultyId_fkey");
        });

        modelBuilder.Entity<DepartmentHead>(entity =>
        {
            entity.HasKey(e => e.DepartmentHeadId).HasName("Department_Head_pkey");

            entity.ToTable("Department_Head");

            entity.HasOne(d => d.Employee).WithMany(p => p.DepartmentHeads)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Department_Head_EmployeeId_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employee_pkey");

            entity.ToTable("Employee");

            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.Salary).HasPrecision(10, 2);

            entity.HasOne(d => d.Chief).WithMany(p => p.InverseChief)
                .HasForeignKey(d => d.ChiefId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Employee_ChiefId_fkey");

            entity.HasOne(d => d.Dept).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Employee_DeptId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Employee_RoleId_fkey");
        });

        modelBuilder.Entity<Engineer>(entity =>
        {
            entity.HasKey(e => e.EngineerId).HasName("Engineer_pkey");

            entity.ToTable("Engineer");

            entity.Property(e => e.Specialty).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Engineers)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Engineer_EmployeeId_fkey");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.ExamId).HasName("Exam_pkey");

            entity.ToTable("Exam");

            entity.Property(e => e.Auditorium).HasMaxLength(20);

            entity.HasOne(d => d.Student).WithMany(p => p.Exams)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Exam_StudentId_fkey");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Exam_SubjectId_fkey");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Exams)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Exam_TeacherId_fkey");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("Faculty_pkey");

            entity.ToTable("Faculty");

            entity.Property(e => e.Abbreviation)
                .HasMaxLength(5)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.SpecialtyId).HasName("Specialty_pkey");

            entity.ToTable("Specialty");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Dept).WithMany(p => p.Specialties)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Specialty_DeptId_fkey");
        });

        modelBuilder.Entity<SpecialtySubject>(entity =>
        {
            entity.HasKey(e => e.SpecialtySubjectId).HasName("Specialty_Subject_pkey");

            entity.ToTable("Specialty_Subject");

            entity.HasOne(d => d.Specialty).WithMany(p => p.SpecialtySubjects)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Specialty_Subject_SpecialtyId_fkey");

            entity.HasOne(d => d.Subject).WithMany(p => p.SpecialtySubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Specialty_Subject_SubjectId_fkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("Student_pkey");

            entity.ToTable("Student");

            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.RegistrationNumber).HasMaxLength(20);

            entity.HasOne(d => d.Specialty).WithMany(p => p.Students)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Student_SpecialtyId_fkey");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("Subject_pkey");

            entity.ToTable("Subject");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.ExecutorDept).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.ExecutorDeptId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Subject_ExecutorDeptId_fkey");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("Teacher_pkey");

            entity.ToTable("Teacher");

            entity.Property(e => e.Degree).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Teacher_EmployeeId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
