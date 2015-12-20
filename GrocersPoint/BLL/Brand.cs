using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace BLL
{
    public class Brand
    {
        [Key]
        public int Brand_Id { get; set; }
        [Required]
        public string Brand_Name { get; set; }
    }
}
