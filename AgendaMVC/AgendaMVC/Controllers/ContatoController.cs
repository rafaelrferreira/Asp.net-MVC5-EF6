using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgendaMVC.Models;
using System.Data;

namespace AgendaMVC.Controllers
{
    public class ContatoController : Controller
    {
        public ActionResult Inserir()
        {
            return View();
        }

        public ActionResult Listar()
        {
            List<Contato> contatos = null;
            using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
            {
                contatos = db.Contato.ToList();
            }
            return View(contatos);
        }

        public ActionResult Editar(int idContato)
        {
            Contato contato = new Contato();
            using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
            {
                contato = db.Contato.Find(idContato);
            }
            return View(contato);
        }

        [HttpPost]
        public ActionResult Inserir(Contato _contato)
        {
            if (ModelState.IsValid)
            {
                using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
                {
                    db.Contato.Add(_contato);
                    db.SaveChanges();
                }
                return RedirectToAction("Listar");
            }
            ModelState.AddModelError("", "Erro ao inserir");
            return View();
        }

        [HttpPost]
        public ActionResult Editar(Contato _contato)
        {
            if (ModelState.IsValid)
            {
                using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
                {
                    db.Entry(_contato).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Listar");
            }
            ModelState.AddModelError("", "Erro ao editar");
            return View();
        }
	}
}