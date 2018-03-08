using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Treinamento.Presentation.WebMVC
{
    public static class HttpPedidosClient
    {
        public static async Task<List<Entities.Pedido>> Get()
        {
            var listaPedidos = new List<Entities.Pedido>();
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri("http://localhost:49819/api/pedidos");
                var response = await httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var obj = await response.Content.ReadAsStringAsync();

                    listaPedidos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Pedido>>(obj);
                }
            }
            return listaPedidos;
        }

        public static async Task<Entities.Pedido> Get(int id)
        {
            var pedido = new Entities.Pedido();
            using (var httpClient = new HttpClient())
            {
                var uri = new Uri($"http://localhost:49819/api/pedidos/{id}");
                var response = await httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var obj = await response.Content.ReadAsStringAsync();
                    pedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Pedido>(obj);
                }
            }
            return pedido;
        }

        public static async Task<Entities.Pedido> Post(Entities.Pedido pedido)
        {
            var novoPedido = new Entities.Pedido();

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

                var uri = new Uri("http://localhost:49819/api/pedidos");
                var response = await httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var obj = await response.Content.ReadAsStringAsync();
                    novoPedido = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Pedido>(obj);
                }
            }
            return novoPedido;
        }

        public static async void Put(Entities.Pedido pedido)
        {
            var novoPedido = new Entities.Pedido();

            using (var http = new HttpClient())
            {
                var uri = new Uri("http://localhost:49819/api/pedidos");

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");
                var response = await http.PutAsync(uri, content);
            }
        }

        public static async void Del(int id)
        {
            using (var http = new HttpClient())
            {
                var uri = new Uri($"http://localhost:49819/api/pedidos/{id}");

                var response = await http.DeleteAsync(uri);

            }
        }
    }
}