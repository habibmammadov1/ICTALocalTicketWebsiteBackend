using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Structure : BaseRegulations
    {
        public Structure() : base(3) { }
        public Structure(string title, string description, string photoPath, string filePath, DateTime lastUpdated) 
            : base(3, title, description, photoPath, filePath, lastUpdated) { }
    }
}
