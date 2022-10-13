using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealMonitoring.DB
{
    public class Connexion
    {
        [Key]
        public int ID_Token { get; set; }
        public string platforme { get; set; }
        public string User { get; set; }
        public string MotdePasse { get; set; }
        public string Token { get; set; }
        public bool TypeDeConnexion { get; set; }
        public int ID_User { get; set; }

        [ForeignKey("ID_User")]
        public Utilisateur Utilisateur { get; set; }
    }
}
