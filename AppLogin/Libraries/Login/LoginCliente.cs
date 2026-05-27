using AppLogin.Models;
using MySqlX.XDevAPI;
using Newtonsoft.Json;

namespace AppLogin.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private Sessao.Sessao _sessao;

        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            //Serializar
            string clienteJsonString = JsonConvert.SerializeObject(cliente);

            _sessao.Cadastrar(Key, clienteJsonString);
        }

        //public Cliente GetCliente()
        //{
        //    //Deserializar
        //    if (_sessao.Existe(Key))
        //    {
        //        string clienteJsonString = _sessao.Consultar(Key);
        //        return JsonConvert.DeserializeObject<Cliente>(clienteJsonString);
        //    }

        //    return ;
        //}
    }
}
