using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealMonitoring.DB
{
    public class Information
    {
        [Key]
        public int id_info { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public string DataSources { get; set; }
        public string TypeDeDonnee { get; set; }
        public int ID_User { get; set; }

        [ForeignKey("ID_User")]
        public Utilisateur Utilisateur { get; set; }
    }
}
