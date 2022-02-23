using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M17E_TrabalhoModelo_2021_22.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Indique um nome de utilizador")]
        [StringLength(50)]
        [MinLength(2,ErrorMessage = "Nome muito pequeno")]
        public string nome { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Indique a password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Indique o perfil")]
        [Display(Name = "Perfil do utilizador")]
        public int perfil { get; set; }
        [Display(Name = "Estado da conta")]
        public bool estado { get; set; }
        //dropdown perfil
        public IEnumerable<System.Web.Mvc.SelectListItem> perfis { get; set; }
    }
}