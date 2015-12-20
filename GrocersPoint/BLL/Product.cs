using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace BLL
{
   public class Product
    {
       [Display(Name = "Product Id")]
       
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
       public string Sub_Cat_Id { get; set; }

       [Display(Name = "Brand")]
       [Required]
       public string Brand_Id { get; set; }

       public int Price { get; set; }

       public int Offer_Price { get; set; }

       public int Purchase_Price { get; set; }



      
      

    }
}
