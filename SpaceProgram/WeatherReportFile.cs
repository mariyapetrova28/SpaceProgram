using SpaceProgram.Exceptions;
using SpaceProgram.Language;
using SpaceProgram.Models;

namespace SpaceProgram
{
    internal class WeatherReportFile
    {
        
        int launchDayRecordIndex = 0;
        double avgTemp = 0;
        double avgWind = 0;
        double avgHumidity = 0;
        double avgPrecipitation = 0;
        double maxTemp, minTemp, maxWind, minWind, maxHumidity, minHumidity, maxPrecipitation, minPrecipitation;
        double medianTemp = 0, medianWind = 0, medianHumidity = 0, medianPrecipitation = 0;
        public List<DataOutputModel> CreateWeatherReportRecords(List<DataInputModel> records)
        {
            List<DataOutputModel> reportRecords = new List<DataOutputModel>();
            try
            {
                //Getting the aggregate data
                maxTemp = minTemp = records[0].Temperature;
                maxWind = minWind = records[0].Wind;
                maxHumidity = minHumidity = records[0].Humidity;
                maxPrecipitation = minPrecipitation = records[0].Precipitation;
                List<double> temp = new List<double>();
                List<double> wind = new List<double>();
                List<double> humidity = new List<double>();
                List<double> precipitation = new List<double>();
                for (int i = 0; i < records.Count; i++)
                {
                    temp.Add(records[i].Temperature);
                    wind.Add(records[i].Wind);
                    humidity.Add(records[i].Humidity);
                    precipitation.Add(records[i].Precipitation);
                    if (records[i].Temperature > maxTemp)
                    {
                        maxTemp = records[i].Temperature;
                    }
                    if (records[i].Temperature < minTemp)
                    {
                        minTemp = records[i].Temperature;
                    }
                    if (records[i].Wind > maxWind)
                    {
                        maxWind = records[i].Wind;
                    }
                    if (records[i].Wind < minWind)
                    {
                        minWind = records[i].Wind;
                    }
                    if (records[i].Humidity > maxHumidity)
                    {
                        maxHumidity = records[i].Humidity;
                    }
                    if (records[i].Humidity < minHumidity)
                    {
                        minHumidity = records[i].Humidity;
                    }
                    if (records[i].Precipitation > maxPrecipitation)
                    {
                        maxPrecipitation = records[i].Precipitation;
                    }
                    if (records[i].Precipitation < minPrecipitation)
                    {
                        minPrecipitation = records[i].Precipitation;
                    }
                    avgTemp += records[i].Temperature;
                    avgWind += records[i].Wind;
                    avgHumidity += records[i].Humidity;
                    avgPrecipitation += records[i].Precipitation;

                }
                avgTemp /= records.Count;
                avgWind /= records.Count;
                avgHumidity /= records.Count;
                avgPrecipitation /= records.Count;

                temp.Sort();
                wind.Sort();
                humidity.Sort();
                precipitation.Sort();
                if (records.Count % 2 != 0)
                {
                    medianTemp = temp[records.Count / 2];
                    medianWind = wind[records.Count / 2];
                    medianHumidity = humidity[records.Count / 2];
                    medianPrecipitation = precipitation[records.Count / 2];
                }
                else
                {
                    medianTemp = (temp[records.Count / 2] + temp[records.Count / 2 - 1]) / 2;
                    medianWind = (wind[records.Count / 2] + wind[records.Count / 2 - 1]) / 2;
                    medianHumidity = (humidity[records.Count / 2] + humidity[records.Count / 2 - 1]) / 2;
                    medianPrecipitation = (precipitation[records.Count / 2] + precipitation[records.Count / 2 - 1]) / 2;
                }
                //Removing all the records that we don't need and getting the best launch day
                for (int i = 0; i < records.Count; i++)
                {
                    if (records[i].Temperature < 2 || records[i].Temperature > 31)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Wind > 10)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Humidity > 60)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Precipitation != 0)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Lightning.Equals("Yes"))
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Clouds.Equals("Cumulus") || records[i].Clouds.Equals("Nimbus"))
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                }
                if (records.Count > 1)
                {
                    for (int i = 1; i < records.Count; i++)
                    {
                        if ((records[i].Wind < records[launchDayRecordIndex].Wind && records[i].Humidity <= records[launchDayRecordIndex].Humidity) || (records[i].Wind <= records[launchDayRecordIndex].Wind && records[i].Humidity < records[launchDayRecordIndex].Humidity))
                        {
                            launchDayRecordIndex = i;
                        }
                    }
                }
                //Filling the WeatherReport data


                DataOutputModel tempData = new DataOutputModel($"{LanguageHelper.GetString("Temp")} (C)", Math.Round(avgTemp, 2).ToString(), maxTemp.ToString(), minTemp.ToString(), medianTemp.ToString(), records[launchDayRecordIndex].Temperature.ToString());
                DataOutputModel windData = new DataOutputModel($"{LanguageHelper.GetString("Wind")} (m/s)", Math.Round(avgWind, 2).ToString(), maxWind.ToString(), minWind.ToString(), medianWind.ToString(), records[launchDayRecordIndex].Wind.ToString());
                DataOutputModel humidityData = new DataOutputModel($"{LanguageHelper.GetString("Humidity")} (%)", Math.Round(avgHumidity, 2).ToString(), maxHumidity.ToString(), minHumidity.ToString(), medianHumidity.ToString(), records[launchDayRecordIndex].Humidity.ToString());
                DataOutputModel precipitationData = new DataOutputModel($"{LanguageHelper.GetString("Precipitation")} (%)", Math.Round(avgPrecipitation, 2).ToString(), maxPrecipitation.ToString(), minPrecipitation.ToString(), medianPrecipitation.ToString(), records[launchDayRecordIndex].Precipitation.ToString());
                DataOutputModel lightningData = new DataOutputModel($"{LanguageHelper.GetString("Lightning")}", null, null, null, null, $"{LanguageHelper.GetString("No")}");
                DataOutputModel cloudsData = new DataOutputModel($"{LanguageHelper.GetString("Clouds")}", null, null, null, null, records[launchDayRecordIndex].Clouds.ToString());
                reportRecords.Add(tempData);
                reportRecords.Add(windData);
                reportRecords.Add(humidityData);
                reportRecords.Add(precipitationData);
                reportRecords.Add(lightningData);
                reportRecords.Add(cloudsData);




            }
            catch (NoRecordsInListException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"{LanguageHelper.GetString("recordsList")}");
            }
            return reportRecords;
        }
        public List<DataOutputModelDE> CreateWeatherReportRecordsDE(List<DataInputModel> records)
        {
            List<DataOutputModelDE> reportRecords = new List<DataOutputModelDE>();
            try
            {
                //Getting the aggregate data
                maxTemp = minTemp = records[0].Temperature;
                maxWind = minWind = records[0].Wind;
                maxHumidity = minHumidity = records[0].Humidity;
                maxPrecipitation = minPrecipitation = records[0].Precipitation;
                List<double> temp = new List<double>();
                List<double> wind = new List<double>();
                List<double> humidity = new List<double>();
                List<double> precipitation = new List<double>();
                for (int i = 0; i < records.Count; i++)
                {
                    temp.Add(records[i].Temperature);
                    wind.Add(records[i].Wind);
                    humidity.Add(records[i].Humidity);
                    precipitation.Add(records[i].Precipitation);
                    if (records[i].Temperature > maxTemp)
                    {
                        maxTemp = records[i].Temperature;
                    }
                    if (records[i].Temperature < minTemp)
                    {
                        minTemp = records[i].Temperature;
                    }
                    if (records[i].Wind > maxWind)
                    {
                        maxWind = records[i].Wind;
                    }
                    if (records[i].Wind < minWind)
                    {
                        minWind = records[i].Wind;
                    }
                    if (records[i].Humidity > maxHumidity)
                    {
                        maxHumidity = records[i].Humidity;
                    }
                    if (records[i].Humidity < minHumidity)
                    {
                        minHumidity = records[i].Humidity;
                    }
                    if (records[i].Precipitation > maxPrecipitation)
                    {
                        maxPrecipitation = records[i].Precipitation;
                    }
                    if (records[i].Precipitation < minPrecipitation)
                    {
                        minPrecipitation = records[i].Precipitation;
                    }
                    avgTemp += records[i].Temperature;
                    avgWind += records[i].Wind;
                    avgHumidity += records[i].Humidity;
                    avgPrecipitation += records[i].Precipitation;

                }
                avgTemp /= records.Count;
                avgWind /= records.Count;
                avgHumidity /= records.Count;
                avgPrecipitation /= records.Count;

                temp.Sort();
                wind.Sort();
                humidity.Sort();
                precipitation.Sort();
                if (records.Count % 2 != 0)
                {
                    medianTemp = temp[records.Count / 2];
                    medianWind = wind[records.Count / 2];
                    medianHumidity = humidity[records.Count / 2];
                    medianPrecipitation = precipitation[records.Count / 2];
                }
                else
                {
                    medianTemp = (temp[records.Count / 2] + temp[records.Count / 2 - 1]) / 2;
                    medianWind = (wind[records.Count / 2] + wind[records.Count / 2 - 1]) / 2;
                    medianHumidity = (humidity[records.Count / 2] + humidity[records.Count / 2 - 1]) / 2;
                    medianPrecipitation = (precipitation[records.Count / 2] + precipitation[records.Count / 2 - 1]) / 2;
                }
                //Removing all the records that we don't need and getting the best launch day
                for (int i = 0; i < records.Count; i++)
                {
                    if (records[i].Temperature < 2 || records[i].Temperature > 31)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Wind > 10)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Humidity > 60)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Precipitation != 0)
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Lightning.Equals("Yes"))
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                    else if (records[i].Clouds.Equals("Cumulus") || records[i].Clouds.Equals("Nimbus"))
                    {
                        records.RemoveAt(i);
                        i--;
                    }
                }
                if (records.Count > 1)
                {
                    for (int i = 1; i < records.Count; i++)
                    {
                        if ((records[i].Wind < records[launchDayRecordIndex].Wind && records[i].Humidity <= records[launchDayRecordIndex].Humidity) || (records[i].Wind <= records[launchDayRecordIndex].Wind && records[i].Humidity < records[launchDayRecordIndex].Humidity))
                        {
                            launchDayRecordIndex = i;
                        }
                    }
                }
                //Filling the WeatherReport data

                DataOutputModelDE tempData = new DataOutputModelDE($"{LanguageHelper.GetString("Temp")} (C)", Math.Round(avgTemp, 2).ToString(), maxTemp.ToString(), minTemp.ToString(), medianTemp.ToString(), records[launchDayRecordIndex].Temperature.ToString());
                DataOutputModelDE windData = new DataOutputModelDE($"{LanguageHelper.GetString("Wind")} (m/s)", Math.Round(avgWind, 2).ToString(), maxWind.ToString(), minWind.ToString(), medianWind.ToString(), records[launchDayRecordIndex].Wind.ToString());
                DataOutputModelDE humidityData = new DataOutputModelDE($"{LanguageHelper.GetString("Humidity")} (%)", Math.Round(avgHumidity, 2).ToString(), maxHumidity.ToString(), minHumidity.ToString(), medianHumidity.ToString(), records[launchDayRecordIndex].Humidity.ToString());
                DataOutputModelDE precipitationData = new DataOutputModelDE($"{LanguageHelper.GetString("Precipitation")} (%)", Math.Round(avgPrecipitation, 2).ToString(), maxPrecipitation.ToString(), minPrecipitation.ToString(), medianPrecipitation.ToString(), records[launchDayRecordIndex].Precipitation.ToString());
                DataOutputModelDE lightningData = new DataOutputModelDE($"{LanguageHelper.GetString("Lightning")}", null, null, null, null, $"{LanguageHelper.GetString("No")}");
                DataOutputModelDE cloudsData = new DataOutputModelDE($"{LanguageHelper.GetString("Clouds")}", null, null, null, null, records[launchDayRecordIndex].Clouds.ToString());
                reportRecords.Add(tempData);
                reportRecords.Add(windData);
                reportRecords.Add(humidityData);
                reportRecords.Add(precipitationData);
                reportRecords.Add(lightningData);
                reportRecords.Add(cloudsData);


            }
            catch (NoRecordsInListException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"{LanguageHelper.GetString("recordsList")}");
            }
            return reportRecords;
        }
    }
}
