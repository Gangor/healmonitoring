using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealMonitoring.DB
{
    public class HealMonitoringContext : DbContext
    {
        public HealMonitoringContext()
        {

        }

        public HealMonitoringContext(DbContextOptions<HealMonitoringContext> options)
            : base(options)
        {

        }

        public DbSet<Connexion> Connexion { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
    }
}
