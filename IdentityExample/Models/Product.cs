using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public double Price { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
