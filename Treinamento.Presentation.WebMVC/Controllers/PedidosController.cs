using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Treinamento.Presentation.WebMVC.Models;
using Treinamento.Entities;

namespace Treinamento.Presentation.WebMVC.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        private PedidosService.IPedidosService _pedidosBO;

        public PedidosController()
        {
            _pedidosBO = new PedidosService.PedidosServiceClient();
        }

        [HttpGet]
        //[OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            List<Models.PedidosModel> pedidos = new List<Models.PedidosModel>();

            var listaPedidos = _pedidosBO.RetornarItens();

            foreach (var item in listaPedidos)
            {
                var pedido = new Models.PedidosModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao,
                    Data = item.Data.ToString("dd/MM/yyyy"),
                    Numero = item.Numero,
                    TipoPedido = Enum.GetName(item.TipoPedido.GetType(), item.TipoPedido)
                };
                pedidos.Add(pedido);
            }
            return View(pedidos);
        }
        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Criar(PedidosModel pedido)
        {
            if (ModelState.IsValid)
            {
                var novoPedido = new Treinamento.Entities.Pedido
                {
                    Descricao = pedido.Descricao,
                    Numero = pedido.Numero,
                    Data = DateTime.Now,
                    TipoPedido = Entities.Enum.TipoPedido.Interno
                };

                _pedidosBO.GravarPedido(novoPedido);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var pedido = _pedidosBO.RetornarPedido(id);
            var novoPedidoModel = new Models.PedidosModel
            {
                Codigo = pedido.Codigo,
                Descricao = pedido.Descricao,
                Numero = pedido.Numero,
                Data = pedido.Data.ToString(),
                TipoPedido = Enum.GetName(pedido.TipoPedido.GetType(), pedido.TipoPedido)
            };
            return View(novoPedidoModel);
        }

        [HttpPut]
        public ActionResult Editar(PedidosModel pedido)
        {
            if (ModelState.IsValid)
            {
                var novoPedido = new Pedido()
                {
                    Codigo = pedido.Codigo,
                    Descricao = pedido.Descricao
                };

                _pedidosBO.AtualizarPedido(novoPedido);

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Excluir(int id)
        {
            var pedido = _pedidosBO.RetornarPedido(id);
            var novoPedidoModel = new Models.PedidosModel
            {
                Codigo = pedido.Codigo,
                Descricao = pedido.Descricao,
                Numero = pedido.Numero,
                Data = pedido.Data.ToString(),
                TipoPedido = Enum.GetName(pedido.TipoPedido.GetType(), pedido.TipoPedido)
            };
            return View(novoPedidoModel);
        }

        [HttpDelete]
        public ActionResult ConfirmarExcluir(int codigo)
        {
            _pedidosBO.ExcluirPedido(codigo);
            return RedirectToAction("Index");
        }
    }
}