using PreRegistro_CadastroUsuario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PreRegistro_CadastroUsuario.Controllers
{
    public class UsuarioController : Controller
    {
        Dao dao = new Dao();
        SendMail email = new SendMail();

        // GET: Usuario
        public ActionResult Index()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["USUARIO"] == "null" || System.Configuration.ConfigurationManager.AppSettings["ID_USUARIO"] == "null")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.Clear();
                return View(dao.CarregarTodosUsuarios());
            }

        }



        //****************** CADASTRAR USUARIO ******************
        // GET: Usuario/Create
        public ActionResult Create()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["USUARIO"] == "null" || System.Configuration.ConfigurationManager.AppSettings["ID_USUARIO"] == "null")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            string senha = usuario.GerarSenha();
            try
            {
                if (ModelState.IsValid)
                {
                    if (dao.CadastrarUsuario(usuario, senha))
                    {

                        ModelState.Clear();
                    }
                }
                ViewBag.Message = "Cadastro realizado com sucesso!";
                this.email.EnviarEmailCadastro(usuario, senha);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }



        //****************** EDITAR USUARIO ******************

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["USUARIO"] == "null" || System.Configuration.ConfigurationManager.AppSettings["ID_USUARIO"] == "null")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(dao.CarregarUsuarioPorId(id));
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dao.AtualizarUsuario(usuario))
                    {

                        ModelState.Clear();
                    }
                }
                ViewBag.Message = "Atualização realizada com sucesso!";
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        //*********** EDIÇÃO - GERAR NOVA SENHA ***************
        public ActionResult GerarSenhaNova(int idUsuario,string nomeUsuario, string email, string cpf)
        {
            Usuario usuario = new Usuario();
            string novaSenha = usuario.GerarSenha();

            if (string.IsNullOrEmpty(email))
            {
                ViewBag.ErrorMessageEmail = "O usuário deve ter um e-mail válido cadastrado";               
            }
            else
            {
                if (dao.AtualizarSenha(idUsuario, novaSenha))
                {
                    ViewBag.Message = "A senha foi gerada e será enviada para o e-mail cadastrado: " + email;
                    this.email.EnviarEmailNovaSenha(nomeUsuario, email, novaSenha);
                }
                else
                {
                    ViewBag.ErrorMessage = "Ocorreu um erro ao gerar uma senha nova!";
                }
            }

            return RedirectToAction("Edit/"+idUsuario);

        }

        //*********** GERAR NOVA SENHA PARA USUARIOS ATIVOS ***************
        public ActionResult GerarSenhaNovaUsuariosAtivos()
        {
            List<Usuario> listaUsuariosAtivos = new List<Usuario>();
            Usuario usuario = new Usuario();
            string novaSenha;

            listaUsuariosAtivos = dao.CarregarTodosUsuariosAtivos();

            foreach (var item in listaUsuariosAtivos)
            {
                if (string.IsNullOrEmpty(item.email))
                {
                    //do nothing
                }
                else
                {
                    novaSenha = usuario.GerarSenha();
                    if (dao.AtualizarSenha(item.idUsuario, novaSenha))
                    {
                        ViewBag.Message = "A senha foi gerada e será enviada para o e-mail cadastrado: " + item.email;
                        this.email.EnviarEmailNovaSenha(item.nome, item.email, novaSenha);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Ocorreu um erro ao gerar uma senha nova!";
                    }
                }
            }

            return RedirectToAction("Index");

        }

        //****************** DELETAR USUARIO ******************

        public ActionResult Delete(int id)
        {
            dao.DeletarUsuario(id);
            return RedirectToAction("Index");
        }

        public ActionResult AtivarTodosUsuarios()
        {
            dao.AtivarTodosUsuarios();
            return RedirectToAction("Index");
        }

        public ActionResult DesativarTodosUsuarios()
        {
            dao.DesativarTodosUsuarios();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Buscar(string parametro)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            listaUsuarios = dao.BuscarUsuarios(parametro);
            return View("Index",listaUsuarios);
        }

        public ActionResult LimparBusca()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Sair()
        {
            dao.LimparCredenciais();
            return RedirectToAction("Index");
        }

    }
}
