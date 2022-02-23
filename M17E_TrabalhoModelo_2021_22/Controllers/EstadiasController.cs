﻿using System;
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
    public class EstadiasController : Controller
    {
        private M17E_TrabalhoModelo_2021_22Context db = new M17E_TrabalhoModelo_2021_22Context();

        // GET: Estadias
        public async Task<ActionResult> Index()
        {
            var estadia = db.Estadia.Include(e => e.cliente).Include(e => e.quarto);
            return View(await estadia.ToListAsync());
        }

        // GET: Estadias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = await db.Estadia.FindAsync(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            // incluir o cliente e o quarto
            estadia.quarto = await db.Quartos.FindAsync(estadia.QuartoID);
            estadia.cliente = await db.Clientes.FindAsync(estadia.ClienteID);
            return View(estadia);
        }

        // GET: Estadias/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome");
            ViewBag.QuartoID = new SelectList(db.Quartos.Where(q => q.Estado == true), "Id", "Tipo_Quarto");
            return View();
        }

        // POST: Estadias/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EstadiaID,data_entrada,data_saida,valor_pago,ClienteID,QuartoID")] Estadia estadia)
        {
            if (ModelState.IsValid)
            {
                estadia.data_saida = estadia.data_entrada;
                estadia.valor_pago = 0;
                // alterar estado do quarto
                var quarto=db.Quartos.Find(estadia.QuartoID);
                quarto.Estado = false;
                db.Entry(quarto).CurrentValues.SetValues(quarto);
                db.Estadia.Add(estadia);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", estadia.ClienteID);
            ViewBag.QuartoID = new SelectList(db.Quartos.Where(q => q.Estado == true), "Id", "Tipo_Quarto", estadia.QuartoID);
            return View(estadia);
        }

        // GET: Estadias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = await db.Estadia.FindAsync(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", estadia.ClienteID);
            ViewBag.QuartoID = new SelectList(db.Quartos, "Id", "Tipo_Quarto", estadia.QuartoID);
            return View(estadia);
        }

        // POST: Estadias/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EstadiaID,data_entrada,data_saida,valor_pago,ClienteID,QuartoID")] Estadia estadia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadia).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Clientes, "ClienteID", "Nome", estadia.ClienteID);
            ViewBag.QuartoID = new SelectList(db.Quartos.Where(q => q.Estado == true || q.Id == estadia.QuartoID), "Id", "Id", estadia.QuartoID);
            return View(estadia);
        }

        // GET: Estadias/Delete/5
        /*public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = await db.Estadia.FindAsync(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            return View(estadia);
        }

        // POST: Estadias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Estadia estadia = await db.Estadia.FindAsync(id);
            db.Estadia.Remove(estadia);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }*/

        public async Task<ActionResult> ListaEstadiasEmCurso()
        {
            var estadia = db.Estadia.Where(e => e.valor_pago==0 && e.data_entrada==e.data_saida).Include(e => e.cliente).Include(e => e.quarto);
            return View(await estadia.ToListAsync());
        }

        public async Task<ActionResult> ProcessaSaida(int? id)
        {
            if (id == null)
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estadia estadia = await db.Estadia.FindAsync(id);
            if (estadia == null)
            {
                return HttpNotFound();
            }
            //calcular o valor a pagar
            //dados do quarto
            var dadosQuarto = await db.Quartos.FindAsync(estadia.QuartoID);
            //duração da estadia em dias
            TimeSpan dias = DateTime.Now.Date.Subtract(estadia.data_entrada);
            int diasPagar = (int)(dias.TotalDays<=0 ? 1 : dias.TotalDays);
            estadia.valor_pago = diasPagar * dadosQuarto.Custo_dia;
            estadia.data_saida = DateTime.Now.Date;
            ViewBag.dias = diasPagar;
            estadia.cliente = await db.Clientes.FindAsync(estadia.ClienteID);
            return View(estadia);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProcessaSaida(Estadia estadia)
        {
            // atualizar a estadia
            db.Entry(estadia).State = EntityState.Modified;
            // atualizar o quarto
            var quarto = await db.Quartos.FindAsync(estadia.QuartoID);
            quarto.Estado = true;
            db.Entry(quarto).CurrentValues.SetValues(quarto);
            await db.SaveChangesAsync();
            // redirecionar para
            return RedirectToAction("ListaEstadiasEmCurso");
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
