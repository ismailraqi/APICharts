using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Charts.Models;

namespace Charts.Controllers
{
    [RoutePrefix("Absence")]
    public class AbsenteismeController : ApiController
    {
        private RHEntities db = new RHEntities();

        // GET Days of Absences By Dep : Absence/departement/{id:int}   
        [HttpGet]
        [Route("departement/{id:int}")]
        public IHttpActionResult GetTF_Absenteisme(int id)
        {
            var DepName = db.Dim_DepEmp.Find(id).Department;
            var absByDepId = db.TF_Absenteisme.Where(x => x.id_dept == id).ToList();
            if (absByDepId == null)
            {
                return NotFound();
            }

            var absenteismeList = new List<AbsenteismeModel>();
            var AbsJr = new List<decimal?>();
            foreach (var a in absByDepId)
            {
                var absenteisme = new AbsenteismeModel();
                absenteisme.Id = a.Id;
                absenteisme.Cle_CoreDataSet = a.Cle_CoreDataSet;
                absenteisme.Date = a.Date;
                absenteisme.Copie_de_jrs_absence = a.Copie_de_jrs_absence;
                absenteisme.Taux = a.Taux;
                absenteisme.Taux_Assiduite = a.Taux_Assiduite;
                absenteisme.Id_Perf_Score = a.Id_Perf_Score;
                absenteisme.id_manager = a.id_manager;
                absenteisme.id_statut = a.id_statut;
                absenteisme.id_sex = a.id_sex;
                absenteisme.id_marital = a.id_marital;
                absenteisme.id_absence = a.id_absence;
                absenteisme.id_dept = a.id_dept;
                AbsJr.Add(absenteisme.Copie_de_jrs_absence);

                absenteismeList.Add(absenteisme);

            }
            var SumJrs = DepName + " : " + AbsJr.Sum();


            //return Ok(absenteismeList);
            return Ok(SumJrs);

        }

        // % Absence By Sex : Absence/sex/{id:int}
        [HttpGet]
        [Route("Sex/pourcentage/{id:int}")]
        public IHttpActionResult GetAbsenceBySex(int id)
        {
            var Sex = db.Dim_Sex.Find(id).Sex ;
            var abs = db.TF_Absenteisme.Count();
            var absBySexId = db.TF_Absenteisme.Where(x => x.id_sex == id).Count();
            if (absBySexId == null)
            {
                return NotFound();
            }
            var TauxAbs = Sex + " : " + (absBySexId * 100) / abs;
            return Ok(TauxAbs);
        }

        // % Absence By Statut Marital : Absence/Marital/1
        [HttpGet]
        [Route("Marital/{ids:int}")]
        public IHttpActionResult GetAbsenceByStatutMarital(int ids)
        {
            var StatutMarital = db.Dim_StatusMarital.Find(ids).MaritalDesc;
            var abs = db.TF_Absenteisme.Count();
            var absByMaritalId = db.TF_Absenteisme.Where(x => x.id_marital == ids).Count();
            if ( absByMaritalId == null)
            {
                return NotFound();
            }
            var TauxAbs = StatutMarital + " : " + (absByMaritalId * 100) / abs;
            return Ok(TauxAbs);
        }

        // Get Days of Absence By Manager : Absence/Manager/1
        [HttpGet]
        [Route("Manager/{id:int}")]
        public IHttpActionResult GetDaysAbsenceByManager(int id)
        {
            var MangerName = db.Dim_Manager.Find(id).Manager_Name;
            var AbsenceByM = db.TF_Absenteisme.Where(x => x.id_manager == id).ToList();
            if (AbsenceByM == null)
            {
                return NotFound();
            }
            var AbsJrByM = new List<decimal?>();
            foreach (var m in AbsenceByM)
            {
                AbsJrByM.Add(m.Copie_de_jrs_absence);
            }
            var SumJ = MangerName + " : " + AbsJrByM.Sum();
            return Ok(SumJ);
        }

        // % assiduite par Dep
        [HttpGet]
        [Route("Assiduite/departement/{id:int}")]
        public IHttpActionResult GetAbsByDep(int id)
        {
            var DepName = db.Dim_DepEmp.Find(id).Department;
            var AbsByAss = db.TF_Absenteisme.Where(x => x.id_dept == id).ToList();
            var AbsJr = new List<decimal?>();
            foreach(var a in AbsByAss)
            {
                AbsJr.Add(a.Taux_Assiduite);
            }
            var TauxAssiduite = DepName + " : " + AbsJr.Sum();

            return Ok(TauxAssiduite);
        }


        // % assiduite par Statut
        [HttpGet]
        [Route("Assiduite/Statut/{id:int}")]
        public IHttpActionResult GetAbsByStatut(int id)
        {
            var StatutName = db.Dim_Status.Find(id).Employment_Status;
            var AbsByStatut = db.TF_Absenteisme.Where(x => x.id_statut == id).ToList();
            var AbsJr = new List<decimal?>();
            foreach (var a in AbsByStatut)
            {
                AbsJr.Add(a.Taux_Assiduite);
            }
            var TauxAssiduite = StatutName + " : " + AbsJr.Sum();

            return Ok(TauxAssiduite);
        }



        // PUT: api/Absenteisme/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTF_Absenteisme(int id, TF_Absenteisme tF_Absenteisme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tF_Absenteisme.Id)
            {
                return BadRequest();
            }

            db.Entry(tF_Absenteisme).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TF_AbsenteismeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Absenteisme
        [ResponseType(typeof(TF_Absenteisme))]
        public IHttpActionResult PostTF_Absenteisme(TF_Absenteisme tF_Absenteisme)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TF_Absenteisme.Add(tF_Absenteisme);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tF_Absenteisme.Id }, tF_Absenteisme);
        }

        // DELETE: api/Absenteisme/5
        [ResponseType(typeof(TF_Absenteisme))]
        public IHttpActionResult DeleteTF_Absenteisme(int id)
        {
            TF_Absenteisme tF_Absenteisme = db.TF_Absenteisme.Find(id);
            if (tF_Absenteisme == null)
            {
                return NotFound();
            }

            db.TF_Absenteisme.Remove(tF_Absenteisme);
            db.SaveChanges();

            return Ok(tF_Absenteisme);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TF_AbsenteismeExists(int id)
        {
            return db.TF_Absenteisme.Count(e => e.Id == id) > 0;
        }
    }
}