using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecomm_Project_101.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name="Postal code")]
        [Required]
        public string PostalCode { get; set; }
        [Display(Name ="Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
 
        [Display(Name="Is Authorized Company" )]
        public bool IsAuthorizedCompany { get; set; }
    }
}
