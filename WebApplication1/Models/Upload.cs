using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcBasic.Models
{
    public class Upload
    {
        [DisplayName("ファイル")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public string file { get; set; }

    }
}