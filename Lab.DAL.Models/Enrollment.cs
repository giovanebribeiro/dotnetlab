using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.DAL.Models
{
    public class Enrollment
    {
        public enum Grad { A, B, C, D }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public Grad? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
