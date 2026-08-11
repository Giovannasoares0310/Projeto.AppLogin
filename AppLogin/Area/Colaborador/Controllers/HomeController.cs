using AppLogin.Libraries.Login;
using AppLogin.Models.Constant;
using AppLogin.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLogin.Area.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {

        private IColaboradorRepository _repositoryColaborador;
        private LoginColadorador _LoginColaborador;

        public HomeController(IColaboradorRepository repositoryColaborador, LoginColadorador loginColador)
        {
            _repositoryColaborador = repositoryColaborador;
            _LoginColaborador = loginColador;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Models.Colaborador colaborador)
        {
            Models.Colaborador colaboradorDB = _repositoryColaborador.Login(colaborador.Email, colaborador.Senha);

            if (colaboradorDB == null && colaboradorDB.Senha != null &&
                colaboradorDB.Tipo == ColaboradorTipoConstant.Comum)
            {
                _LoginColaborador.Login(colaboradorDB);
                return new RedirectResult(Url.Action(nameof(PainelComum)));
            }

            if (colaboradorDB == null && colaboradorDB.Senha != null &&
            colaboradorDB.Tipo == ColaboradorTipoConstant.Gerente)
            {
                _LoginColaborador.Login(colaboradorDB);
                return new RedirectResult(Url.Action(nameof(PainelGerente)));
            }

            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitados!";
                return View();
            }
        }

        public IActionResult PainelGerente()
        {
            ViewBag.Nome = _LoginColaborador.GetColaborador().Nome;
            ViewBag.CPF = _LoginColaborador.GetColaborador().CPF;
            ViewBag.Email = _LoginColaborador.GetColaborador().Email;
            //return new ContentResult() { Content = "Este é o Painel do Colaborador Gerente"
            return View();
        }

        public IActionResult PainelComum()
        {
            ViewBag.Nome = _LoginColaborador.GetColaborador().Nome;
            ViewBag.CPF = _LoginColaborador.GetColaborador().CPF;
            ViewBag.Email = _LoginColaborador.GetColaborador().Email;
            //return new ContentResult() { Content = "Este é o Painel do Colaborador Comum"
            return View();
        }

        public IActionResult Painel()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _LoginColaborador.Logout();
            return RedirectToAction("Login", "Home");
        }

    }
}
