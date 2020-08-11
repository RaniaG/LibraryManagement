using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }
    }
}
