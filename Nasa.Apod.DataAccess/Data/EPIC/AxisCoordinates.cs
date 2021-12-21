using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.Apod.DataAccess.Data.EPIC
{
    public abstract class AxisCoordinates
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
