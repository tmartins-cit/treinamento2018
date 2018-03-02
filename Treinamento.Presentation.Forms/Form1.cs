using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Treinamento.Presentation.Forms
{
    public partial class Form1 : Form
    {
        private Treinamento.Entities.Pedido _pedido;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            Business.Interfaces.IPedidos pedidos = Treinamento.Business.Factory.FactoryPedidos.CriarClassePedidos();
           

            Entities.Pedido novoPedido = new Entities.Pedido
            {
                Data = DateTime.Now,
                Descricao =  $"Meu pedido numero {Guid.NewGuid()}",
                TipoPedido = Entities.Enum.TipoPedido.Interno,
                Numero = DateTime.Now.Second.ToString()
            };

            pedidos.GravarPedido(novoPedido);


            var itens = pedidos.RetornarItens();

            gridView.DataSource = itens;

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //btnExecutar_Click(sender, e);
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            CarregarPedido();
        }

        private void CarregarPedido()
        {
            _pedido = (Treinamento.Entities.Pedido)gridView.Rows[gridView.CurrentCell.RowIndex].DataBoundItem;
            txt_description.Text = _pedido.Descricao;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SalvarPedido();
        }

        private void SalvarPedido()
        {
            _pedido.Descricao = txt_description.Text;
            Treinamento.Business.Interfaces.IPedidos pedidosBO = Treinamento.Business.Factory.FactoryPedidos.CriarClassePedidos();

            pedidosBO.AtualizarPedido(_pedido);
            var itens = pedidosBO.RetornarItens();

            gridView.DataSource = itens;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            ExcluirPedido();
        }

        private void ExcluirPedido()
        {
            _pedido = (Treinamento.Entities.Pedido)gridView.Rows[gridView.CurrentCell.RowIndex].DataBoundItem;

            Treinamento.Business.Interfaces.IPedidos pedidosBO = Treinamento.Business.Factory.FactoryPedidos.CriarClassePedidos();

            pedidosBO.ExcluirPedido(_pedido.Codigo);
            var itens = pedidosBO.RetornarItens();

            gridView.DataSource = itens;
        }
    }
}
