using M17E_TrabalhoModelo_2021_22.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M17E_TrabalhoModelo_2021_22.Controllers
{
    [Authorize]
    public class ConsultasController : Controller
    {
        private M17E_TrabalhoModelo_2021_22Context db = new M17E_TrabalhoModelo_2021_22Context();

        // GET: Consultas
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult PesquisaCliente()
        {
            string nome = Request.Form["tbNome"];
            var clientes = db.Clientes.Where(c => c.Nome.Contains(nome));
            return View("PesquisaCliente",clientes.ToList());
        }
        public ActionResult PesquisaDinamica()
        {
            return View();
        }
        public JsonResult PesquisaNome(string nome)
        {
            var clientes = db.Clientes.Where(c => c.Nome.Contains(nome)).ToList();
            var lista = new List<Campos>();
            foreach (var c in clientes)
                lista.Add(new Campos() { nome = c.Nome });
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MelhorCliente()
        {
            string sql = @"SELECT nome,sum(valor_pago) as valor
                            FROM Estadias INNER JOIN Clientes
                            ON Estadias.ClienteID=Clientes.ClienteID
                            GROUP BY Estadias.ClienteID,nome
                            ORDER BY valor DESC";

            var melhor = db.Database.SqlQuery<Campos>(sql);
            if (melhor != null && melhor.ToList().Count > 0)
                ViewBag.melhor = melhor.ToList()[0];
            else
            {
                Campos temp = new Campos();
                temp.nome = "Não foram encontrados registos";
                ViewBag.melhor = temp;
            }
            return View();
        }
        public class Campos
        {
            public string nome { get; set; }
            public decimal valor { get; set; }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}