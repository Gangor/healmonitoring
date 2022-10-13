using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github.Models
{
    public class Repository
    {
        public bool fork { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
        public string url { get; set; }

        public List<Commit> Commits = new List<Commit>();

        public int GetScore()
        {
            return Commits.Sum(u => u.GetScore());
        }
    }
}
