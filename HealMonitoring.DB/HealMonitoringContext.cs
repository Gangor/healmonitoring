using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
