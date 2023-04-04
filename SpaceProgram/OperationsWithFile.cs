using CsvHelper;
using SpaceProgram.Exceptions;
using SpaceProgram.Models;
using System.Globalization;

namespace SpaceProgram
{
    internal class OperationsWithFile
    {
        public List<DataInputModel> ReadFile(string filePath)
        {
            List<DataInputModel> records = new List<DataInputModel>();
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<DataInputModelMap>();
                        records = csvReader.GetRecords<DataInputModel>().ToList();
                    }

                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return records;
        }
        public void WriteFile(List<DataOutputModel> reportRecords)
        {
            try
            {
                var csvPath = Path.Combine(Environment.CurrentDirectory, $"WeatherReport.csv");
                using (var streamWriter = new StreamWriter(csvPath))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(reportRecords);

                    }
                }
            }
            catch (IOException e)
            {
                throw new CannotCreateFileException(e.Message);
            }
        }
        public void WriteFile(List<DataOutputModelDE> reportRecords)
        {
            try
            {
                var csvPath = Path.Combine(Environment.CurrentDirectory, $"Wetterbericht.csv");
                using (var streamWriter = new StreamWriter(csvPath))
                {
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        csvWriter.WriteRecords(reportRecords);

                    }
                }
            }
            catch (IOException e)
            {
                throw new CannotCreateFileException(e.Message);
            }
        }
        public void DisplayAllRecords(List<DataInputModel> records)
        {
            foreach (var item in records)
            {
                Console.WriteLine($"Day: {item.Day}, Temperature: {item.Temperature} C, Wind Speed: {item.Wind} m/s, Humidity: {item.Humidity}%, Precipitation: {item.Precipitation}%, Lightning: {(item.Lightning)}, Clouds: {item.Clouds}");
            }
        }
    }
}
