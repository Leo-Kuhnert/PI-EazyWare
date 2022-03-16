using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PI_EAZYWARE.Models;
using Microsoft.AspNetCore.Http;

namespace PI_EAZYWARE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        /****************************************CONTROLLER INDEX *******************************************/
        
        public IActionResult Index()
        {
            return View();
        }
        
         /**************************************CONTROLLERS USUÁRIOS*******************************************/
        
        /*CADASTRO DE USUÁRIOS*/
         public IActionResult Cadastro()
        {   
            
            if(HttpContext.Session.GetString("conta") == "colaborador")
            return RedirectToAction("Login");
            
            return View();
        }

        [HttpPost]  
         public IActionResult Cadastro(Usuario user)
        {   
            UsuarioRepository ur = new UsuarioRepository();
            ur.Insert(user);
            ViewBag.Cadastro = "Usuário cadastrado com sucesso!";
            return View();
        }

        /*LISTAGEM DE USUÁRIOS*/      
        public IActionResult Listar_Usuarios()
        {   
            if(HttpContext.Session.GetInt32("id_usuarios") == null)
            return RedirectToAction("Login");

            if(HttpContext.Session.GetString("conta") == "colaborador")
            return RedirectToAction("Login");

            if(HttpContext.Session.GetString("conta") == "usuario")
            return RedirectToAction("Login");
            
            UsuarioRepository ur = new UsuarioRepository();
            List<Usuario> listagem = ur.Listar_Usuarios();
            return View(listagem);
        }

        /*ALTERAÇÃO DE USUÁRIOS*/    
        [HttpGet]
         public IActionResult Alterar_Usuario(int id)
        {   
            UsuarioRepository ur = new UsuarioRepository();
            
            Usuario user = ur.RetornoUsuario(id);
        
            return View(user);
        }

        [HttpPost]
        public IActionResult Alterar_Usuario(Usuario usuario)
        {  

            UsuarioRepository ur = new UsuarioRepository();
            
            ur.Alterar(usuario);
             
            return RedirectToAction("Listar_Usuarios"); 
            
        }

        /*EXCLUSÃO DE USUÁRIOS*/ 
        [HttpGet]
        public IActionResult Excluir_Usuario(int id){

            UsuarioRepository ur = new UsuarioRepository();
            ur.Excluir(id);
            return RedirectToAction("Listar_Usuarios");
        }


        /***********************************CONTROLLERS PACOTES*****************************************/

        /*PRODUTOS*/
        public IActionResult Produtos()
        {  
            
            if(HttpContext.Session.GetInt32("id_usuarios") == null)
            return RedirectToAction("Login");
            
            UsuarioRepository ur = new UsuarioRepository();
            List<Produtos> produtos = ur.Listar_Produtos();
            return View(produtos);
        }
        
        /*REGISTRO DE PACOTES*/
        public IActionResult Registro_Produtos()
        {   
            if(HttpContext.Session.GetInt32("id_usuarios") == null)
            return RedirectToAction("Login");

            if(HttpContext.Session.GetString("conta") == "usuario")
            return RedirectToAction("Login");
            
            return View();
        }

        [HttpPost]  
         public IActionResult Registro_Produtos(Produtos produto)
        {   
            UsuarioRepository ur = new UsuarioRepository();
            ur.Insert_Produtos(produto);
            ViewBag.Cadastro = "Produto registrado com sucesso!";
            return View();
        }
        
        /*LISTAGEM DE PACOTES*/
         public IActionResult Listar_Produtos(){
          
          if(HttpContext.Session.GetInt32("id_usuarios") == null)
            return RedirectToAction("Login");

            if(HttpContext.Session.GetString("conta") == "usuario")
            return RedirectToAction("Login");
          
          UsuarioRepository ur = new UsuarioRepository();
          
          List<Produtos> listagem = ur.Listar_Produtos();
          
          return View(listagem);

        }

        /*ALTERAÇÃO DE PRODUTOS*/
         [HttpGet]
        public IActionResult Alterar_Produto(int id){
            
            UsuarioRepository ur = new UsuarioRepository();
            
            Produtos p = ur.RetornoProduto(id);
        
            return View(p);
        }

         [HttpPost]
        public IActionResult Alterar_Produto(Produtos produto){
         
         UsuarioRepository ur = new UsuarioRepository();
            
            ur.Alterar_P(produto);
             
            return RedirectToAction("Listar_Produtos");
        }

        /*EXCLUSÃO DE PACOTES*/
        [HttpGet]
        public IActionResult Excluir_Produtos(int id){

            UsuarioRepository ur = new UsuarioRepository();

            ur.Excluir_P(id);
            
            return RedirectToAction("Produtos");
        }
        
        [HttpGet]
        public IActionResult Excluir_Produto(int id){

            UsuarioRepository ur = new UsuarioRepository();

            ur.Excluir_P(id);
            
            return RedirectToAction("Listar_Produtos");
        }

        /**********************************CONTROLLERS ATENDIMENTO***********************************/

        /*ATENDIMENTO*/
         public IActionResult Atendimento()
        {   
             if(HttpContext.Session.GetInt32("id_usuarios") == null)
            return RedirectToAction("Login");
            
            return View();
        }
        
        [HttpPost]
        public IActionResult Atendimento(Atendimento a){
            
            UsuarioRepository ur = new UsuarioRepository();
            ur.Insert_Atendimento(a);
            
            ViewBag.Atendimento = "A sua dúvida foi enviada com sucesso!";
            return View();
        }

        /********************************LOGIN********************************************/   
         public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
          public IActionResult Login(Usuario user)
        {   
            UsuarioRepository ur = new UsuarioRepository();
            Usuario usuario = ur.Login(user); 
            
            if(usuario != null){
            
            HttpContext.Session.SetInt32("id_usuarios", usuario.id);
            HttpContext.Session.SetString("nome", usuario.nome);
            HttpContext.Session.SetString("conta", usuario.conta);

            ViewBag.Sucesso = "Login realizado com sucesso!";
            
            } else{

                ViewBag.Sucesso = "Falha no login!";
            }
            return View();
        }

        /***************************************Logout*********************************************/

        public IActionResult Logout(){

            HttpContext.Session.Clear();

            return View("Index");
        }

    }
}