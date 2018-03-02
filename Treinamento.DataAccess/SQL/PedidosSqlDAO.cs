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
            string sqlQuery = $@"UPDATE dbo.tb_pedido SET ds_pedido = @ds_pedido WHERE cd_pedido = @cd_pedido";

            SqlCommand updateCommand = new SqlCommand(sqlQuery, _conexao);

            updateCommand.Parameters.Add("@ds_pedido", SqlDbType.VarChar);
            updateCommand.Parameters["@ds_pedido"].Value = pedido.Descricao;

            updateCommand.Parameters.Add("@cd_pedido", SqlDbType.Int);
            updateCommand.Parameters["@cd_pedido"].Value = pedido.Codigo;

            _conexao.Open();
            updateCommand.ExecuteNonQuery();
            _conexao.Close();
        }

        void IPedidosDAO.ExcluirPedido(int codigoPedido)
        {
            string sqlQuery = $@"UPDATE dbo.tb_pedido SET in_desligado = 1 WHERE cd_pedido = @cd_pedido";

            SqlCommand deleteCommand = new SqlCommand(sqlQuery, _conexao);

            deleteCommand.Parameters.Add("@cd_pedido", SqlDbType.Int);
            deleteCommand.Parameters["@cd_pedido"].Value = codigoPedido;

            _conexao.Open();
            deleteCommand.ExecuteNonQuery();
            _conexao.Close();
        }

        Pedido IPedidosDAO.GravarNovoPedido(Pedido pedido)
        {
            string sqlQuery = $@"INSERT INTO dbo.tb_pedido(nr_numero_pedido, ds_pedido, cd_tipo_pedido, dt_pedido, in_desligado)  
                                VALUES(@nr_pedido, @ds_pedido, @cd_tipo_pedido, @dt_pedido, 0);
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
            string sqlQuery = @"SELECT cd_pedido, nr_numero_pedido, ds_pedido, cd_tipo_pedido, dt_pedido FROM tb_pedido WHERE in_desligado = 0";
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

            _conexao.Close();
            return pedidos;
        }
    }
}
