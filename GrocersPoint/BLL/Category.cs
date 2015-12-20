using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
   public class Category
    {
      
        public int Cat_Id { get; set; }
       
        [Required]
        public string Cat_Name { get; set; }
    }
}
