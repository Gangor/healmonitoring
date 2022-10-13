using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github.Models
{
    public class Commit
    {
        const int AdditionRate = 1;
        const int RemovalRate = 2;

        public Owner author { get; set; }
        public Owner committer { get; set; }
        public CommitInfo commit { get; set; }
        public List<File> files { get; set; }
        public Tree tree { get; set; }
        public string sha { get; set; }

        public int GetScore()
        {
            var addition = files.Sum(u => u.additions);
            var deletion = files.Sum(u => u.deletions);

            if (addition == 0 && deletion == 0)
                return files.Count;

            return addition * AdditionRate + deletion + RemovalRate;
        }
    }
}
