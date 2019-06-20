using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ASP.NET_Web_Application.Models;
using Newtonsoft.Json;

namespace ASP.NET_Web_Application.Controllers
{
    public class PratosController : Controller
    {
        private PratoDBContext db = new PratoDBContext();

        // GET: Pratos
        public ActionResult Index(string searchString)
        {
            var pratos = from m in db.Pratos select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                pratos = pratos.Where(s => s.nome.Contains(searchString));
            }

            var tableList = new List<Prato> { };
            int numRows = db.Pratos.Count();

            foreach (var item in pratos)
            {
                tableList.Add(new Prato()
                {
                    nome = item.nome,
                    descricao = item.descricao,
                    url_img = item.url_img,
                    valor = item.valor
                });
            }

            String URL = "http://localhost:10000/tacos";

            using (var webClient = new System.Net.WebClient())
            {
                String json = webClient.DownloadString(URL);
                var item = new List<Prato>();
                JsonConvert.PopulateObject(json, item);

                for (int i = 0; i < item.Count(); i++)
                {

                    foreach (var itemTable in tableList)
                    {
                        if (itemTable.nome == item[i].nome && item[i].valor < itemTable.valor)
                        {
                            itemTable.valor = item[i].valor * 0.9;
                        }
                    }

                    //tableList.Add(item[i]);
                }
            }
            //return View(pratos);
            return View(tableList);

        }

        // GET: Pratos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prato prato = db.Pratos.Find(id);
            if (prato == null)
            {
                return HttpNotFound();
            }
            return View(prato);
        }

        // GET: Pratos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pratos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,url_img,nome,descricao,valor")] Prato prato)
        {
            if (ModelState.IsValid)
            {
                db.Pratos.Add(prato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prato);
        }

        // GET: Pratos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prato prato = db.Pratos.Find(id);
            if (prato == null)
            {
                return HttpNotFound();
            }
            return View(prato);
        }

        // POST: Pratos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,url_img,nome,descricao,valor")] Prato prato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prato);
        }

        // GET: Pratos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prato prato = db.Pratos.Find(id);
            if (prato == null)
            {
                return HttpNotFound();
            }
            return View(prato);
        }

        // POST: Pratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prato prato = db.Pratos.Find(id);
            db.Pratos.Remove(prato);
            db.SaveChanges();
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
