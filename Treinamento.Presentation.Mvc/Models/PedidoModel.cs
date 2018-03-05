using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Treinamento.Presentation.Mvc.Models
{
    public class PedidoModel
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Numero { get; set; }
        public String TipoPedido { get; set; }
        public String Data { get; set; }
    }
}