using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treinamento.Business
{
    class ValidacaoPedidos : Interfaces.IValidacaoPedidos
    {
        public void Validar(Entities.Pedido pedido)
        {
            if (pedido.Descricao.Trim().Equals(string.Empty))
                throw new Exception("Descrição vazia.");

            if (pedido.Numero.Trim().Equals(string.Empty))
                throw new Exception("Número vazio.");

            if (pedido.Data == DateTime.MinValue)
                throw new Exception("Data inexistente.");
        }
    }
}
