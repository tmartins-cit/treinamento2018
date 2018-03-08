using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Treinamento.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPedidosService" in both code and config file together.
    [ServiceContract]
    public interface IPedidosService
    {
        [OperationContract]
        List<Entities.Pedido> RetornarItens();
        [OperationContract]
        Entities.Pedido GravarPedido(Entities.Pedido pedido);
        [OperationContract]
        void AtualizarPedido(Entities.Pedido pedido);
        [OperationContract]
        void ExcluirPedido(int codigo);
        [OperationContract]
        Entities.Pedido RetornarPedido(int codigo);
    }
}
