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

        Treinamento.Business.Interfaces.IPedidos _pedidosBO;

        public Form1()
        {
            InitializeComponent();
            _pedidosBO = Treinamento.Business.Factory.FactoryPedidos.CriarClassePedidos();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            GerarDados();      
        }

        private void GerarDados()
        {
            
            Entities.Pedido novoPedido = new Entities.Pedido
            {
                Data = DateTime.Now,
                Descricao = $"Meu pedido numero {Guid.NewGuid()}",
                TipoPedido = Entities.Enum.TipoPedido.Interno,
                Numero = DateTime.Now.Second.ToString()
            };

            try
            {
                _pedidosBO.GravarPedido(novoPedido);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            AtualizarGrid();
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

            try
            {
                _pedidosBO.AtualizarPedido(_pedido);
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            AtualizarGrid();
            txt_description.Clear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            ExcluirPedido();
        }

        private void ExcluirPedido()
        {
            _pedido = (Treinamento.Entities.Pedido)gridView.Rows[gridView.CurrentCell.RowIndex].DataBoundItem;

            try
            {
                _pedidosBO.ExcluirPedido(_pedido);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            AtualizarGrid();
           
        }

        private void AtualizarGrid()
        {
            var itens = _pedidosBO.RetornarItens();
            gridView.DataSource = itens;

            _pedido = null;
        }
    }
}
