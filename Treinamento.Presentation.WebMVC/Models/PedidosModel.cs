using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Treinamento.Presentation.WebMVC.Models
{
    public class PedidosModel
    {
        [Display(Name = "Código")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatória!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
        [Display(Name = "Tipo do pedido")]
        public string TipoPedido { get; set; }
        public string Data { get; set; }
        protected bool Desligado { get; set; }
    }
}