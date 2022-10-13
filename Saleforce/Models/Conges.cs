using System;

namespace Saleforce.Models
{
    public class Conges
    {
        const int CongesRate = 2;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string User { get; set; }

        internal int GetScore()
        {
            return (EndDate - StartDate).Days * CongesRate;
        }
    }
}
