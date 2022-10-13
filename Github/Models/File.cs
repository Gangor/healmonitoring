using System;
using System.Collections.Generic;
using System.Text;

namespace Github.Models
{
    public class File
    {
        public string sha { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
        public int changes { get; set; }
    }
}
