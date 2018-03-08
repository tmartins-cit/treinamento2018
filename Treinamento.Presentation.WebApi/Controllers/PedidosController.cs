using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Treinamento.Presentation.WebApi.Controllers
{
    public class PedidosController : ApiController
    {
        private Business.Interfaces.IPedidos _pedidos;
        public PedidosController()
        {
            _pedidos = Business.Factory.FactoryPedidos.CriarClassePedidos();
        }
        public List<Entities.Pedido> Get()
        {
            return _pedidos.RetornarItens();
        }
        public Entities.Pedido Post([FromBody] Entities.Pedido pedido)
        {
            return _pedidos.GravarPedido(pedido);
        } 
        public void Put([FromBody] Entities.Pedido pedido)
        {
            _pedidos.AtualizarPedido(pedido);
        }
        public void Delete(int id)
        {
            _pedidos.ExcluirPedido(id);
        }
        public Entities.Pedido Get(int id)
        {
            return _pedidos.RetornarPedido(id);
        }
    }
}
