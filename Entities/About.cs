using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class About : BaseRegulations
    {
        public About() : base(2) { }
        public About(string title, string description, string photoPath, string filePath, DateTime lastUpdated) 
            : base(2, title, description, photoPath, filePath, lastUpdated) { }
    }
}