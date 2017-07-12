using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

namespace MvcBasic.Models
{
    public class Member
    {
        public int Id { get; set; }
        [DisplayName("姓")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public string LastName { get; set; }

        [DisplayName("名")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public string FirstName { get; set; }


        [DisplayName("メールアドレス")]
        [Required(ErrorMessage = "{0}は必須です。")]
        [EmailAddress(ErrorMessage ="メールアドレス形式で入力してください")]
        public string Email { get; set; }

        [DisplayName("生年月日")]
        [Required(ErrorMessage = "{0}は必須です。")]
        public DateTime Birth { get; set; }

        [DisplayName("既婚")]
        public bool Married { get; set; }

        [DisplayName("備考")]
        public string Memo { get; set; }

    }
}