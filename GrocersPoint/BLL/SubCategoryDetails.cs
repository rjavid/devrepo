using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace BLL
{
   public class SubCategoryDetails
    {
        public int Sub_Cat_Id { get; set; }

          [Display(Name = "Sub Category name")]  
        public string Sub_Cat_Name { get; set; }

        [Display(Name = "Category name")] 
        public string Cat_Name { get; set; }
    }
}
