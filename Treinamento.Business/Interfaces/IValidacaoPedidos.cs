using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treinamento.Business.Interfaces
{
    public interface IValidacaoPedidos
    {
        void Validar(Entities.Pedido pedido);
    }
}
