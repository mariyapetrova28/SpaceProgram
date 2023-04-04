using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Models
{
    internal class DataOutputModelDE
    {
        public string Parameter { get; set; }
        public string Durchschnitt { get; set; }
        public string Maximal { get; set; }
        public string Minimum { get; set; }
        public string Median { get; set; }

        public string Starttag { get; set; }

        public DataOutputModelDE(string parameter, string average, string maximum, string minimum, string median, string launchday)
        {
            this.Parameter = parameter;
            this.Durchschnitt = average;
            this.Maximal = maximum;
            this.Minimum = minimum;
            this.Median = median;
            this.Starttag = launchday;
        }
    }
}
