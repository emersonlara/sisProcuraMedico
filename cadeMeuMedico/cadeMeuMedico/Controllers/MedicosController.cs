using cadeMeuMedico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace cadeMeuMedico.Controllers
{
    public class MedicosController : Controller
    {

        private CadeMeuMedicoBDEntities bd = new CadeMeuMedicoBDEntities();
        // GET: Medicos

        public ActionResult Index()
        {

            var medicos = bd.Medicos.Include(m => m.Cidades).Include(m => m.Especialidades).ToList();
            return View(medicos);
        }

        public ActionResult Adicionar()
        {
            ViewBag.IDCidade = new SelectList(bd.Cidades, "IDCidade", "Nome");
            ViewBag.IDEspecialidade = new SelectList(bd.Especialidades, "IDEspecialidade", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult Adicionar(Medicos medico)
        {
            if (ModelState.IsValid)
            {
                bd.Medicos.Add(medico);
                bd.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.IDCidade = new SelectList(bd.Cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(bd.Especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);

            return View(medico);
        }


        public ActionResult Excluir() {
            ;
            return View();
        }

        [HttpPost]
        public ActionResult Excluir(long id)
        {
            try
            {
                Medicos medico = bd.Medicos.Find(id);
                bd.Medicos.Remove(medico);
                bd.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch
            {
               
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(long id)
        {
            Medicos medico = bd.Medicos.Find(id);
            ViewBag.IDCidade = new SelectList(bd.Cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(bd.Especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);
            return View(medico);
        }

        [HttpPost]
        public ActionResult Edit(Medicos medico)
        {
            if (ModelState.IsValid)
            {
                bd.Entry(medico).State = EntityState.Modified;
                bd.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCidade = new SelectList(bd.Cidades, "IDCidade", "Nome", medico.IDCidade);
            ViewBag.IDEspecialidade = new SelectList(bd.Especialidades, "IDEspecialidade", "Nome", medico.IDEspecialidade);
            return View(medico);
        }




    }
       
}