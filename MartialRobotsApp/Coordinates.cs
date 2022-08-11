using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartialRobotsApp
{
    public class Coordinates
    {
        public int LongitudeX { get; set; }
        public int LatitudeY { get; set; }

        public Coordinates(int longitudeX, int latitudeY)
        {
            LongitudeX = longitudeX;
            LatitudeY = latitudeY;
        }
    }
}
