using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M17E_TrabalhoModelo_2021_22.Models
{
    public class Quarto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Tem de indicar o piso do quarto")]
        [Range(1,5,ErrorMessage ="O piso tem de estar entre 1 e 5")]
        public int Piso { get; set; }

        [Display(Name ="Lotação")]
        [Required(ErrorMessage ="Indique a lotação do quarto")]
        [Range(1,5,ErrorMessage ="O quarto tem de ter uma lotação entre 1 e 5")]
        public int Lotacao { get; set; }

        [Display(Name = "Custo diário")]
        [Required(ErrorMessage = "Indique o custo diário")]
        [Range(0, 1000, ErrorMessage = "O preço deve ser superior ou igual a 0 e inferior a 1000")]
        [DataType(DataType.Currency)]
        public decimal Custo_dia { get; set; }

        [Display(Name = "Casa de banho")]
        public bool Casa_banho { get; set; }
        public bool Estado { get; set; }
        [Display(Name = "Tipo de Quarto")]
        [Required(ErrorMessage ="Tem de indicar o tipo de quarto")]
        [StringLength(20)]
        [MinLength(3, ErrorMessage ="O tipo tem de ter pelo menos 3 letras")]
        public string Tipo_Quarto { get; set; }
        public Quarto()
        {
            Estado = true;
        }
    }
}