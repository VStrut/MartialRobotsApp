using MartialRobotsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian_Robots
{
    public class Mars
    {
        public Coordinates Surface { get; set; }

        public Mars(Coordinates surface)
        {
            Surface = surface;
        }
    }
}
