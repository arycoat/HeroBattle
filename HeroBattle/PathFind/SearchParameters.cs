using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle.PathFind
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SearchParameters
    {
        public Point StartLocation { get; set; }

        public Point EndLocation { get; set; }

        private Map map;

        public SearchParameters(Point startLocation, Point endLocation, Map map)
        {
            this.StartLocation = startLocation;
            this.EndLocation = endLocation;
            this.map = map;
        }

        public Map GetMap() { return map; }
    }
}
