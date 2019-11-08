using System;
using System.Collections.Generic;

namespace SpaceGame
{
    public class Planet
    {
        private string name;
        private List<Colony> colonies = new List<Colony>();
        private Coordinates positionInSpace;
        private Resources fertility;
        public Planet(string planetName, Coordinates pos, Resources planetFertility)
        {
            this.name = planetName;
            this.positionInSpace = pos;
            this.fertility = planetFertility;
        }

        public string getName()
        {
            return this.name;
        }

        public void setColony(string name)
        {
            Colony newColony = new Colony(name, this.fertility);
            this.colonies.Add(newColony);
        }

        public Coordinates getCoordinates()
        {
            return positionInSpace;
        }

        public Resources getFertility()
        {
            return fertility;
        }

        public void destroyColony(string name)
        {
            foreach (Colony colony in colonies)
            {
                if (colony.getName() == name)
                {
                    this.colonies.Remove(colony);
                    Console.WriteLine("Sucessfully deleted colony " + name);
                    return;
                } 
            }
            Console.WriteLine("Colony with name " + name + " on planet " + this.name + " doesnt exist");
        } 

        public List<Colony> getColonies()
        {
            return this.colonies;
        }
    }
}