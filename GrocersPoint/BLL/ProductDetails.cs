using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
   public class ProductDetails
    {
        public string Prod_Id { get; set; }

        [Display(Name = "Product Name")]
        [Required]
        public string Prod_Name { get; set; }

        [Display(Name = "Packed Quanity")]
        [Required]
        public string Prod_Quantity { get; set; }


        [Display(Name = "Product Image")]
        [Required]
        public string Prod_Img { get; set; }

        [Display(Name = "Sub Category")]
        [Required]
        public string Sub_Cat_Name { get; set; }

        [Display(Name = "Brand")]
        [Required]
        public string Brand_Name { get; set; }

        public int Price { get; set; }

       [Display(Name = "Offer Price")] 
       public int Offer_Price { get; set; }

       [Display(Name = "Purchase Price")] 
        public int Purchase_Price { get; set; }

    }
}
