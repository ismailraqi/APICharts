using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Charts.Models
{
    public class AbsenteismeModel
    {
        public int Id { get; set; }
        public int? Cle_CoreDataSet { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Copie_de_jrs_absence { get; set; }
        public decimal? Taux { get; set; }
        public decimal? Taux_Assiduite { get; set; }
        public int? Id_Perf_Score { get; set; }
        public int? id_manager { get; set; }
        public int? id_statut { get; set; }
        public int? id_sex { get; set; }
        public int? id_marital { get; set; }
        public int? id_absence { get; set; }
        public int? id_dept { get; set; }
    }
}