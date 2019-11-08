using System;
using System.Collections.Generic;

namespace SpaceGame
{

    public class Status
    {
        public Planet currentPlanet { get; private set; } = null;
        public Colony currentColony { get; private set; } = null;
        private Planet destination = null;
        private Coordinates placeInSpace = new Coordinates(0,0,0);
        private Resources amountOfResources = new Resources(100, 100, 100);
        private Coordinates speed = new Coordinates(100, 100, 100);
        public void addResources(Resources res)
        {
            this.amountOfResources.iron += res.iron;
            this.amountOfResources.gold += res.gold;
            this.amountOfResources.spice += res.spice;
        }
        public bool payResources(Resources res)
        {
            if (this.amountOfResources.iron < res.iron ||
                this.amountOfResources.gold < res.gold ||
                this.amountOfResources.spice < res.spice)
            {
                Console.WriteLine("Insufficent funds");
                return false;
            }
            this.amountOfResources -= res;
            return true;
        }
        public bool setDestination(string name, List<Planet> planets)
        {
            foreach (Planet planet in planets)
            {
                if (planet.getName() == name)
                {
                    destination = planet;
                    currentPlanet = null;
                    return true;
                }
            }
            return false;
        }
        public void Move()
        {
            if (destination != null)            
                if (placeInSpace.moveTo(destination.getCoordinates(), speed))
                {
                    currentPlanet = destination;
                    destination = null;
                }            
        }
        public void moveToColony(string name)
        {
            if (currentPlanet == null)
            {
                Console.WriteLine("you must be on planet");
                return;
            }

            foreach (Colony colony in currentPlanet.getColonies())
            {
                if (colony.getName() == name)
                {
                    currentColony = colony;
                    return;
                }
            }
            Console.WriteLine("colony " + name + " doesnt exist");
        }

        public void getCurrentInfo(List<Planet> planets, List<MineType> mineTypes, List<StatueType> statues)
        {
            Console.WriteLine("Place in space: " + placeInSpace.coordX.ToString() + " " + placeInSpace.coordY.ToString() + " " + placeInSpace.coordZ.ToString());
            if (destination != null)
                Console.WriteLine("Destination: " +destination.getName());
            Console.WriteLine("Current resources:");
            Console.WriteLine("iron: " + amountOfResources.iron);
            Console.WriteLine("gold: " + amountOfResources.gold);
            Console.WriteLine("spice: " + amountOfResources.spice + "\n");

            if (currentPlanet == null)
            {
                Console.WriteLine("Planets: ");
                foreach (Planet planet in planets)
                {
                    Coordinates coord = planet.getCoordinates();
                    Console.WriteLine(planet.getName() + " (" + coord.coordX + " " + coord.coordY + " " + coord.coordZ+ ")");
                }
            } else
            {
                if (currentColony == null)
                {
                    Console.WriteLine("Current planet: " + currentPlanet.getName());

                    Coordinates coord = currentPlanet.getCoordinates();
                    Console.WriteLine("x: " + coord.coordX + " y: " + coord.coordY + " z: " + coord.coordZ);

                    Resources res = currentPlanet.getFertility();
                    Console.WriteLine("iron: " + res.iron + " gold: " + res.gold + " spice: " + res.spice);

                    Console.WriteLine("Colonies: ");
                    foreach (Colony colony in currentPlanet.getColonies())
                    {
                        Console.WriteLine(colony.getName());
                    }
                } else
                {
                    Console.WriteLine("Current planet: " + currentPlanet.getName());
                    Console.WriteLine("Current colony: " + currentColony.getName());
                    Console.WriteLine("Mines: ");
                    foreach (MineType t in mineTypes)
                        if (currentColony.buildings.ContainsKey(t))
                            foreach (MineBuilding b in currentColony.buildings[t])
                                Console.WriteLine(b.ToString());
                    foreach (StatueType t in statues)
                        if (currentColony.buildings.ContainsKey(t))
                            foreach (Statue b in currentColony.buildings[t])
                                Console.WriteLine(b.ToString());
                }
            }
        }

        public void goBack()
        {
            if (currentColony != null)
            {
                currentColony = null;
                return;
            }

            if (currentPlanet != null)
            {
                currentPlanet = null;
                return;
            }
        }
    }
}