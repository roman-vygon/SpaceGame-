namespace SpaceGame
{
    public struct Coordinates
    {
        public double coordX;
        public double coordY;
        public double coordZ;
        static double moveCoordinate(double cur, double dest, double speed)
        {
            if (dest > cur)
            {
                cur += speed;
                if (cur > dest)
                    cur = dest;
            }
            else
            {
                cur -= speed;
                if (cur < dest)
                    cur = dest;
            }
            return cur;
        }
        public bool moveTo(Coordinates destination, Coordinates speed)
        {
            this.coordX = moveCoordinate(this.coordX, destination.coordX, speed.coordX);
            this.coordY = moveCoordinate(this.coordY, destination.coordY, speed.coordY);
            this.coordZ = moveCoordinate(this.coordZ, destination.coordZ, speed.coordZ);
            return (this.coordX == destination.coordX &&
                    this.coordY == destination.coordY &&
                    this.coordZ == destination.coordZ);
        }
        public Coordinates(double x, double y, double z)
        {
            this.coordX = x;
            this.coordY = y;
            this.coordZ = z;            
        }        
    }
}