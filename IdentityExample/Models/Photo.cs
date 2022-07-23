using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string Filename { get; set; }

        public string PhotoUrl { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
