using AppLogin.Libraries.Login;
using AppLogin.Models;
using AppLogin.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppLogin.Controllers
{
    public class HomeController : Controller
    {

        //Injetar dependencia
        private IClienteRepository _clienteRepository;
        private LoginCliente _LoginCliente;

        public HomeController(IClienteRepository clienteRepository, LoginCliente loginCliente)
        {
            _clienteRepository = clienteRepository;
            _LoginCliente = loginCliente;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _clienteRepository.Login(cliente.Email, cliente.Senha);

            if (clienteDB.Email != null && clienteDB.Senha != null)
            {
                _LoginCliente.Login(clienteDB);
                return new RedirectResult(Url.Action(nameof(PainelCliente)));
            }

            else
            {
                //Errp na sessão
                ViewData["MSG_E"] = "Usuário não localizado, por favor verifique e-mail e senha digitado";
                return View();
            }
        }

        [HttpGet] 
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult PainelCliente()
        {
            // Pega o cliente logado na sessão se existir
            Cliente cliente = _LoginCliente.GetCliente();

            // verifica se há um cliente logado
            if (cliente == null)
            {
                TempData["MSG_E"] = "Você precisa fazer login para acessar o painel";
                return RedirectToAction(nameof(Login));
            }

            ViewBag.Nome = _LoginCliente.GetCliente().Nome;
            ViewBag.CPF = _LoginCliente.GetCliente().CPF;
            ViewBag.Email = _LoginCliente.GetCliente().Email;
            //return new ContentResult() { Content = "Este é o Painel do Cliente"
            return View();

        }

        public IActionResult LogoutCliente()
        {
            _LoginCliente.Logout();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
