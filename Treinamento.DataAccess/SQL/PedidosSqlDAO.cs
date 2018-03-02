using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treinamento.DataAccess.Interfaces;
using Treinamento.Entities;

namespace Treinamento.DataAccess.SQL
{
    public class PedidosSqlDAO : IPedidosDAO
    {
        private readonly SqlConnection _conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MEU_DB"].ToString());
        
        void IPedidosDAO.AtualizarPedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        void IPedidosDAO.ExcluirPedido(int codigoPedido)
        {
            throw new NotImplementedException();
        }

        Pedido IPedidosDAO.GravarNovoPedido(Pedido pedido)
        {
            string sqlQuery = $@"INSERT INTO dbo.tb_pedido(nr_numero_pedido, ds_pedido, cd_tipo_pedido, dt_pedido)  
                                VALUES(@nr_pedido, @ds_pedido, @cd_tipo_pedido, @dt_pedido);
                                SELECT SCOPE_IDENTITY();";

            SqlCommand sqlCommand = new SqlCommand(sqlQuery, _conexao);

            sqlCommand.Parameters.Add("@nr_pedido", SqlDbType.VarChar);
            sqlCommand.Parameters["@nr_pedido"].Value = pedido.Numero;

            sqlCommand.Parameters.Add("@ds_pedido", SqlDbType.VarChar);
            sqlCommand.Parameters["@ds_pedido"].Value = pedido.Descricao;

            sqlCommand.Parameters.Add("@cd_tipo_pedido", SqlDbType.Int);
            sqlCommand.Parameters["@cd_tipo_pedido"].Value = pedido.TipoPedido;

            sqlCommand.Parameters.Add("@dt_pedido", SqlDbType.DateTime);
            sqlCommand.Parameters["@dt_pedido"].Value = pedido.Data;


            _conexao.Open();
            pedido.Codigo = Convert.ToInt32(sqlCommand.ExecuteScalar());
            _conexao.Close();

            return pedido;
        }

        List<Pedido> IPedidosDAO.RetornarPedidos()
        {
            string sqlQuery = @"SELECT cd_pedido, nr_numero_pedido, ds_pedido, cd_tipo_pedido, dt_pedido FROM tb_pedido";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, _conexao);
            _conexao.Open();

            List<Pedido> pedidos = new List<Pedido>();
            using (IDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    Entities.Pedido pedido = new Entities.Pedido
                    {
                        Codigo = Convert.ToInt32(sqlDataReader["cd_pedido"]),
                        Descricao = sqlDataReader["ds_pedido"].ToString(),
                        Numero = sqlDataReader["nr_numero_pedido"].ToString(),
                        TipoPedido = (Entities.Enum.TipoPedido)Convert.ToInt32(sqlDataReader["cd_tipo_pedido"]),
                        Data = Convert.ToDateTime(sqlDataReader["dt_pedido"])
                    };

                    pedidos.Add(pedido);
                }
            }

            return pedidos;
        }
    }
}
