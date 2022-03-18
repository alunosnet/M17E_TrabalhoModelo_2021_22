using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using M17E_TrabalhoModelo_2021_22.Data;
using M17E_TrabalhoModelo_2021_22.Models;
using PagedList;

namespace M17E_TrabalhoModelo_2021_22.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private M17E_TrabalhoModelo_2021_22Context db = new M17E_TrabalhoModelo_2021_22Context();

        // GET: Clientes
        public async Task<ActionResult> Index(int? page)
        {
            var clientes = db.Clientes.ToList();

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var umapagina = clientes.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

            ViewBag.umapagina = umapagina;

            return View(await db.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClienteID,Nome,Morada,CP,Email,Telefone,DataNascimento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //não permitir emails repetidos
                int contar = db.Clientes.Where( c=> c.Email==cliente.Email).ToList().Count();
                if (contar > 0)
                {
                    ModelState.AddModelError("Email", "O email já existe.");
                    return View(cliente);
                }
                db.Clientes.Add(cliente);
                await db.SaveChangesAsync();
                // guardar fotografia
                HttpPostedFileBase fotografia = Request.Files["fotografia"];
                if (fotografia != null && fotografia.ContentLength > 0)
                {
                    string nome = Server.MapPath("~/Fotos/") + cliente.ClienteID + ".jpg";
                    fotografia.SaveAs(nome);
                }
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClienteID,Nome,Morada,CP,Email,Telefone,DataNascimento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                int contar = db.Clientes.Where(c => c.Email == cliente.Email && c.ClienteID != cliente.ClienteID).ToList().Count();
                if (contar > 0)
                {
                    ModelState.AddModelError("Email", "O email já existe.");
                    return View(cliente);
                }
                db.Entry(cliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles ="Administrador")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();
            //TODO: apagar imagem
            var ficheiro = Server.MapPath("~/Fotos/") + id + ".jpg";
            if (System.IO.File.Exists(ficheiro))
                System.IO.File.Delete(ficheiro);

            return RedirectToAction("Index");
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
