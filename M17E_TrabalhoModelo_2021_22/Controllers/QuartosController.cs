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

namespace M17E_TrabalhoModelo_2021_22.Controllers
{
    public class QuartosController : Controller
    {
        private M17E_TrabalhoModelo_2021_22Context db = new M17E_TrabalhoModelo_2021_22Context();

        // GET: Quartos
        public async Task<ActionResult> Index()
        {
            return View(await db.Quartos.ToListAsync());
        }

        // GET: Quartos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = await db.Quartos.FindAsync(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }

            return View(quarto);
        }
        // GET: Quartos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quartos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Piso,Lotacao,Custo_dia,Casa_banho,Estado,Tipo_Quarto")] Quarto quarto)
        {
            if (ModelState.IsValid)
            {
                db.Quartos.Add(quarto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(quarto);
        }

        // GET: Quartos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = await db.Quartos.FindAsync(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }
            return View(quarto);
        }

        // POST: Quartos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Piso,Lotacao,Custo_dia,Casa_banho,Estado,Tipo_Quarto")] Quarto quarto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quarto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(quarto);
        }

        // GET: Quartos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quarto quarto = await db.Quartos.FindAsync(id);
            if (quarto == null)
            {
                return HttpNotFound();
            }
            return View(quarto);
        }

        // POST: Quartos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Quarto quarto = await db.Quartos.FindAsync(id);
            db.Quartos.Remove(quarto);
            await db.SaveChangesAsync();
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
