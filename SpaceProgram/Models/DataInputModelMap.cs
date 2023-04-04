using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Models
{
    internal class DataInputModelMap : ClassMap<DataInputModel>
    {
        public DataInputModelMap() 
        {
            Map(m => m.Day).Name("Day");
            Map(m => m.Temperature).Name("Temperature (C)");
            Map(m => m.Wind).Name("Wind (m/s)");
            Map(m => m.Humidity).Name("Humidity (%)");
            Map(m => m.Precipitation).Name("Precipitation (%)");
            Map(m => m.Lightning).Name("Lightning");
            Map(m => m.Clouds).Name("Clouds");
        }
    }
}
