using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Treinamento.Entities.Enum;
using Treinamento.Presentation.Mvc.Models;

namespace Treinamento.Presentation.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private Treinamento.Business.Interfaces.IPedidos _pedidosBO;
        public HomeController()
        {
            _pedidosBO = Treinamento.Business.Factory.FactoryPedidos.CriarClassePedidos();

        }
        public ActionResult Index()
        {
            var itens = _pedidosBO.RetornarItens();
            var pedidosModel = new List<PedidoModel>();
            itens.ForEach(pedido =>
            {
                PedidoModel pedidoModel = new PedidoModel()
                {
                    Codigo = pedido.Codigo,
                    Descricao = pedido.Descricao,
                    Numero = pedido.Numero,
                    TipoPedido = Enum.GetName(pedido.TipoPedido.GetType(), pedido.TipoPedido),
                    Data = pedido.Data.ToString("dd/MM/yyyy")
                };
                pedidosModel.Add(pedidoModel);
            });
            return View(pedidosModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}