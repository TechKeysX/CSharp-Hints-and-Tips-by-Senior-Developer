using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.IO;


class Program
{
    static void Main()
    {
         using var context = new SchoolContext();

        // Ensure database is created
        context.Database.EnsureCreated();

        // Seed data if not exists
        if (!context.Students.Any())
        {
            context.Students.AddRange(
                new Student { Name = "Ali", Age = 20 },
                new Student { Name = "Sara", Age = 22 },
                new Student { Name = "John", Age = 21 }
            );
            context.SaveChanges();
        }

        // LINQ query: Get students older than 20
        var olderStudents = context.Students
                                   .Where(s => s.Age > 20)
                                   .OrderBy(s => s.Name)
                                   .ToList();

        // Print results
        Console.WriteLine("Students older than 20:");
        foreach (var student in olderStudents)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}");
        }
    }
}


public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}


public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Use your own connection string
        // optionsBuilder.UseSqlite("Data Source=school.db");
           optionsBuilder.UseSqlServer("Server=(local);Database=school;Trusted_Connection=True;TrustServerCertificate=True");
    }

    }
