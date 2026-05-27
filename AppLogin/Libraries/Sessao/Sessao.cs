using System.Globalization;

namespace AppLogin.Libraries.Sessao
    {
    public class Sessao
    {
        //Interface com uma biblioteca para manipular a sessao
        IHttpContextAccessor _context;

        public Sessao(IHttpContextAccessor context)
        {
            _context = context;
        }

        //CRUD
        public void Cadastrar(string Key, string Valor)
        {
            _context.HttpContext.Session.SetString(Key, Valor);
        }
        public string Consultar(string Key)
        {
            return _context.HttpContext.Session.GetString(Key);
        }
        //Verificar se existe sessao criada 
        public bool Existe(string Key)
        { 
           if(_context.HttpContext.Session.GetString(Key) == null)
            {
            return false;
            }
            return true;
        }
        //remover sessao
        public void Remover(string Key)
        {
            _context.HttpContext.Session.Remove(Key);
        }

        public void RemoverTodos()
        {
            _context.HttpContext.Session.Clear();
        }

        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                _context.HttpContext.Session.Remove(Key);
            }
            _context.HttpContext.Session.SetString(Key, Valor);
        }
    }







    }


