using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeggieApp.Model.Model.Product
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int product_id { get; set; }
        [Required]
        public string product_name { get; set; }
        [Required]
        public string product_image { get; set; }

        [ForeignKey("category_id")]
        public Categories? Categories { get; set; }
        public int category_id { get; set; }

        [Required]
        public int list_price { get; set; }

    }
}
