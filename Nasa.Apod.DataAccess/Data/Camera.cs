using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nasa.Apod.DataAccess.Data
{
    public class Camera
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoverId { get; set; }
        public string FullName { get; set; }
    }
}
