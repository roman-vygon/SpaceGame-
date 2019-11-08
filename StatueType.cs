using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class StatueType : BuildingType
    {
        public StatueType(string _name, Resources _cost, int _maxLevel)
        {            
            this.Name = _name;
            this.cost = _cost;
            this.maxLevel = _maxLevel;
        }
    }
}
