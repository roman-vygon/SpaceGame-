using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class Statue : Building
    {
        public Statue(string _name, BuildingType t)
        {
            this.name = _name;
            this.type = t;
        }
        private void beBeautiful()
        {
            //do nothing
        }
    }
}
