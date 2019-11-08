using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    abstract public class Building
    {
        protected BuildingType type;
        public string name;
        public int level { get; private set; } = 1;     
        public void upgrade(Status status)
        {
            if (level != type.maxLevel)
                if (status.payResources(new Resources(10 * level, 10 * level, 10 * level)))
                    this.level++;
        }
    }
}
