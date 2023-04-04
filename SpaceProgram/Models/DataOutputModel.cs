using LINQtoCSV;

namespace SpaceProgram.Models
{
    internal class DataOutputModel
    {
        public string Parameter { get; set; }
        public string Average { get; set; }
        public string Maximum { get; set; }
        public string Minimum { get; set; }
        public string Median { get; set; }

        public string LaunchDay { get; set; }

        public DataOutputModel(string parameter, string average, string maximum, string minimum, string median, string launchday)
        {
            this.Parameter = parameter;
            this.Average = average;
            this.Maximum = maximum;
            this.Minimum = minimum;
            this.Median = median;
            this.LaunchDay = launchday;
        }
    }
}
