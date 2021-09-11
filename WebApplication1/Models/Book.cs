using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public DateTime PublishedDate { get; set; }

        public double Quantity { get; set; }

        public int? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
