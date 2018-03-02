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
    }
}
