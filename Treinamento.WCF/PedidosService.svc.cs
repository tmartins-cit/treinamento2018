using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Treinamento.Entities;

namespace Treinamento.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PedidosService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PedidosService.svc or PedidosService.svc.cs at the Solution Explorer and start debugging.
    public class PedidosService : IPedidosService
    {

        private Business.Interfaces.IPedidos _pedidosBO;

        public PedidosService()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string completePath = System.IO.Directory.GetParent(path).Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", completePath);

            _pedidosBO = Business.Factory.FactoryPedidos.CriarClassePedidos();
        }

        public void AtualizarPedido(Pedido pedido)
        {
            _pedidosBO.AtualizarPedido(pedido);
        }

        public void ExcluirPedido(int codigo)
        {
            _pedidosBO.ExcluirPedido(codigo);
        }

        public Pedido GravarPedido(Pedido pedido)
        {
            return _pedidosBO.GravarPedido(pedido);
        }

        public List<Pedido> RetornarItens()
        {
            return _pedidosBO.RetornarItens();
        }

        public Pedido RetornarPedido(int codigo)
        {
            return _pedidosBO.RetornarPedido(codigo);
        }
    }
}
