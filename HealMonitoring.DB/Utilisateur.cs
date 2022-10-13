using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealMonitoring.DB
{
    public class Utilisateur
    {
        [Key]
        public int ID_User { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Entreprise { get; set; }

        public List<Information> Informations { get; set; }
    }
}
