using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace M17E_TrabalhoModelo_2021_22.Models
{
    public class Estadia
    {
        [Key]
        public int EstadiaID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Tem de indicar a data de entrada")]
        [Display(Name = "Data de entrada")]
        public DateTime data_entrada { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime data_saida { get; set; }
        [DataType(DataType.Currency)]
        public decimal valor_pago { get; set; }
        [ForeignKey("cliente")] 
        [Required(ErrorMessage = "Tem de indicar um cliente")]
        [Display(Name = "Cliente")]
        public int ClienteID { get; set; }
        public Cliente cliente { get; set; }
        [ForeignKey("quarto")]
        [Required(ErrorMessage = "Tem de indicar o quarto")]
        [Display(Name = "Quarto")]
        public int QuartoID { get; set; }
        public Quarto quarto { get; set; }
    }
}