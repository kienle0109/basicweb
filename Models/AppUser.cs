using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace razorweb.models
{       
    public class AppUser : IdentityUser {
        [Column(TypeName="nvarchar")]
        [StringLength(400)]
        public string? HomeAddress {set; get;}
        [DataType(DataType.Date)]
        public DateTime? BirthDate {set; get;}
    }
}