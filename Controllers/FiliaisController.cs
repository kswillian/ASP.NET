using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_Web_Application.Models;

namespace ASP.NET_Web_Application.Controllers
{
    public class FiliaisController : Controller
    {
        private FilialDBContext db = new FilialDBContext();

        // GET: Filiais
        public ActionResult Index(string searchString)
        {
            var filiais = from f in db.Filiais
                          select f;
            if (!String.IsNullOrEmpty(searchString))
            {
                filiais = filiais.Where(s => s.nome.Contains(searchString));
            }

            return View(filiais);
        }


        // GET: Filiais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filial filial = db.Filiais.Find(id);
            if (filial == null)
            {
                return HttpNotFound();
            }
            return View(filial);
        }

        // GET: Filiais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filiais/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,cep,logradouro,bairro,uf")] Filial filial)
        {
            if (ModelState.IsValid)
            {
                db.Filiais.Add(filial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(filial);
        }

        // GET: Filiais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filial filial = db.Filiais.Find(id);
            if (filial == null)
            {
                return HttpNotFound();
            }
            return View(filial);
        }

        // POST: Filiais/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,cep,logradouro,bairro,uf")] Filial filial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filial);
        }

        // GET: Filiais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filial filial = db.Filiais.Find(id);
            if (filial == null)
            {
                return HttpNotFound();
            }
            return View(filial);
        }

        // POST: Filiais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Filial filial = db.Filiais.Find(id);
            db.Filiais.Remove(filial);
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
