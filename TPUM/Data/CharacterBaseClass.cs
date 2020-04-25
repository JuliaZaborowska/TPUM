using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPUM.Data
{
    abstract class CharacterBaseClass
    {
        public IList<CharacterBaseClass> MultiClasses { get; set; }
    }
}
