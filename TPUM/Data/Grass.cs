using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    class Grass : IMap
    {
        public string Name { get { return "Grass"; } }
        public bool CanWalk { get { return true; } }
        public FieldType Type { get { return FieldType.GRASS; } }
    }
}
