using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  razorweb.models 
{
    [Table("posts")]
    public class Article {
        [Key]
        public int Id {set; get;}
        [StringLength(255, MinimumLength = 5, ErrorMessage = "{0} phai dai tu {2} toi {1}")]
        [Required(ErrorMessage = "{0} phai nhap")]
        [Column(TypeName = "nvarchar")]
        [DisplayName("Tieu de")]
        public string Tittle {set; get;}
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} phai nhap")]
        [DisplayName("Ngay tao")]
        public DateTime Created {set; get;}
        [Column(TypeName = "ntext")]
        [DisplayName("Noi dung")]
        public string Content {set; get;}
    }
}