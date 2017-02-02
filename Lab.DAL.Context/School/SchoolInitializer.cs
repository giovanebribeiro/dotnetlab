using Lab.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using static Lab.DAL.Models.Enrollment;

namespace Lab.DAL.Context.School
{
    public class SchoolInitializer: DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student {FirstMidName="Alice", LastName="Lab", EnrollmentDate=DateTime.Now },
                new Student {FirstMidName="Bob", LastName="Android", EnrollmentDate=DateTime.Parse("2016-10-02") },
                new Student {FirstMidName="Carlos", LastName="Pereba", EnrollmentDate=DateTime.Parse("2015-11-03") },
                new Student {FirstMidName="Davi", LastName="Silva", EnrollmentDate=DateTime.Now }
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course { Id = 1050, Title="Matemática", Credits=3},
                new Course { Id = 1051, Title="Português", Credits=3},
                new Course { Id = 1052, Title="Física", Credits=2},
                new Course { Id = 1053, Title="Química", Credits=1},
            };
            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment {StudentId = 1, CourseId = 1050, Grade = Grad.A },
                new Enrollment {StudentId = 1, CourseId = 1051, Grade = Grad.B },
                new Enrollment {StudentId = 1, CourseId = 1052, Grade = Grad.C },
                new Enrollment {StudentId = 1, CourseId = 1053, Grade = Grad.A },
                new Enrollment {StudentId = 2, CourseId = 1050, Grade = Grad.A },
                new Enrollment {StudentId = 2, CourseId = 1051, Grade = Grad.B },
                new Enrollment {StudentId = 2, CourseId = 1052, Grade = Grad.B },
                new Enrollment {StudentId = 2, CourseId = 1053, Grade = Grad.C },
                new Enrollment {StudentId = 3, CourseId = 1050, Grade = Grad.A },
                new Enrollment {StudentId = 3, CourseId = 1051, Grade = Grad.A },
                new Enrollment {StudentId = 3, CourseId = 1052, Grade = Grad.C },
                new Enrollment {StudentId = 3, CourseId = 1053, Grade = Grad.D },
                new Enrollment {StudentId = 4, CourseId = 1050, Grade = Grad.A },
                new Enrollment {StudentId = 4, CourseId = 1051, Grade = Grad.C },
                new Enrollment {StudentId = 4, CourseId = 1052, Grade = Grad.C },
                new Enrollment {StudentId = 4, CourseId = 1053, Grade = Grad.D },
            };
            enrollments.ForEach(e => context.Enrollments.Add(e));
            context.SaveChanges();
        }
    }
}
