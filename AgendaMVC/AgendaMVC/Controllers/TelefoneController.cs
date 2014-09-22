using AgendaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgendaMVC.Controllers
{
    public class TelefoneController : Controller
    {
        public ActionResult Adicionar(int idContato)
        {
            ViewData["idContato"] = idContato;
            List<Telefone> lFones = null;
            using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
            {
                var query = from fones in db.Telefone where fones.idContato == idContato select fones;
                lFones = query.ToList<Telefone>();

                Contato contato = db.Contato.Find(idContato);
                ViewData["codigo"] = contato.idContato;
                ViewData["nome"] = contato.nome;
                ViewData["empresa"] = contato.empresa;
            }
            return View(lFones);
        }

        public ActionResult AdicionarV2()
        {
            TelefoneV2 telefonev2 = new TelefoneV2();
            using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
            {
                var contatos = db.Contato.ToList();
                telefonev2.lContatos = contatos.Select(x => new SelectListItem
                                                                {
                                                                    Value = x.idContato.ToString(),
                                                                    Text = x.nome
                                                                });
            }
            return View(telefonev2);
        }

        [HttpPost]
        public ActionResult AdicionarV2(FormCollection form)
        {
            int idContato = Convert.ToInt32(form["idContato"]);
            using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
            {
                Contato contato = new Contato();
                contato = db.Contato.Find(idContato);
                Telefone fone = new Telefone
                {
                    Contato = contato,
                    fone = form["telefone"],
                    idContato = contato.idContato,
                    idTelefone = 0
                };
                db.Telefone.Add(fone);
                db.SaveChanges();
            }
            return RedirectToAction("Listar", "Contato");
        }

        public ActionResult Apagar(int idTelefone)
        {
            int idCont = 0;
            using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
            {
                Telefone telefone = db.Telefone.Find(idTelefone);
                idCont = telefone.idContato;
                db.Telefone.Remove(telefone);
                db.SaveChanges();

                Contato contato = db.Contato.Find(telefone.idContato);
                ViewData["codigo"] = contato.idContato;
                ViewData["nome"] = contato.nome;
                ViewData["empresa"] = contato.empresa;
            }
            return RedirectToAction("Adicionar", new { idContato = idCont });
        }

        [HttpPost]
        public ActionResult Adicionar(FormCollection form)
        {
            int idContato = Convert.ToInt32(form["idContato"]);
            List<Telefone> lFones = null;
            using (dbAgendaMVCEntities db = new dbAgendaMVCEntities())
            {
                Contato contato = new Contato();
                contato = db.Contato.Find(idContato);
                Telefone fone = new Telefone
                {
                    Contato = contato,
                    fone = form["telefone"],
                    idContato = contato.idContato,
                    idTelefone = 0
                };
                db.Telefone.Add(fone);
                db.SaveChanges();

                ViewData["codigo"] = contato.idContato;
                ViewData["nome"] = contato.nome;
                ViewData["empresa"] = contato.empresa;
                ViewData["idContato"] = contato.idContato;
                var query = from fones in db.Telefone where fones.idContato == contato.idContato select fones;
                lFones = query.ToList<Telefone>();
            }
            return View(lFones);
        }
	}
}