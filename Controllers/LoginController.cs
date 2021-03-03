using PreRegistro_CadastroUsuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;

namespace PreRegistro_CadastroUsuario.Controllers
{
    [SessionState(SessionStateBehavior.Disabled)]
    public class LoginController : Controller
    {
        Dao dao = new Dao();

        // GET: Login
        public ActionResult Index()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["USUARIO"] != "null" || System.Configuration.ConfigurationManager.AppSettings["ID_USUARIO"] != "null")
            {
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(Administrador login)
        {

            login = dao.Logar(login);

            if (login.ativo == true && login.usuario != "")
            {
                //Salva as credenciais no web.config
                dao.SalvarCredenciais(login.usuario, login.id);
                return RedirectToAction("Index", "Usuario");

            }
            else if (login.ativo == false && login.usuario != "")
            {
                //Response.Write("<script>alert('O usuário " + login.usuario + " não está ativo. Favor contactar o mantenedor do sistema.');</script>");
                ViewBag.Message = "O usuário " + login.usuario + " não está ativo. Favor contactar o mantenedor do sistema.";
                return View();
            }
            else
            {
                //Response.Write("<script>alert('Usuário ou senha incorretos!');</script>");
                ViewBag.Message = "Usuário ou senha incorretos!";
                return View();
            }
        }

    }
}