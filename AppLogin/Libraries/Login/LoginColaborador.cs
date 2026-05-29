using AppLogin.Models;
using MySqlX.XDevAPI;
using Newtonsoft.Json;

namespace AppLogin.Libraries.Login
{
    public class LoginColadorador
    {
        private string Key = "Login.Colaborador";
        private Sessao.Sessao _sessao;

        public LoginColadorador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            //Serializar
            string colaboradorJsonString = JsonConvert.SerializeObject(colaborador);

            _sessao.Cadastrar(Key, colaboradorJsonString);
        }

        public Colaborador GetColaborador()
        {
            //Deserializar
            if (_sessao.Existe(Key))
            {
                string colaboradorJsonString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJsonString);
            }
            else
            {
                return null;
            }
        }

        //Remove a sessão e desloga colaborador
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
