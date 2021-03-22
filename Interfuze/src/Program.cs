using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Interfuze
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvParser csvParser = new CsvParser();
            List<Device> devices = csvParser.ParseDeviceCSV("Devices.csv");

            List<Data> dataList1 = csvParser.ParseDataCSV("Data1.csv");
            List<Data> dataList2 = csvParser.ParseDataCSV("Data2.csv");

            List<Data> dataList = dataList1.Union(dataList2).ToList();

            string currentTimeStr = "2021-01-01 " + DateTime.Now.ToString().Substring(11, 5);
            DateTime currentTime = DateTime.ParseExact(currentTimeStr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            Dictionary<string, List<int>> mapFourHours = new Dictionary<string, List<int>>();

            Dictionary<string, Device> deviceMap = populateDeviceMap(devices);

            foreach (Data data in dataList)
            {
                TimeSpan timeSpan = currentTime - data.Time;
                if (timeSpan.TotalHours <= 4 && timeSpan.TotalHours >= 0)
                {
                    populateClusterMap(mapFourHours, data);
                }
            }

            foreach (var pair in mapFourHours)
            {
                string key = pair.Key;
                List<int> values = pair.Value;
                double value = 0;
                string color = "Red";
                bool isRed = false;

                foreach (int val in values)
                {
                    if (val > 30)
                    {
                        isRed = true;
                        continue;
                    }
                    value += val;
                }
                value = value / values.Count;

                if (!isRed && value >= 10 && value < 15) {
                    color = "Amber";
                } else if (!isRed && value >= 0 && value < 10)
                {
                    color = "Green";
                }

                Device device = deviceMap[key];
                Console.WriteLine(device.ToString() + ": " + color);
            }
        }

        private static void populateClusterMap(Dictionary<string, List<int>> map, Data data)
        {
            if (map.ContainsKey(data.DeviceID))
            {
                List<int> list = map.GetValueOrDefault(data.DeviceID);
                list.Add(data.Rainfall);
                map[data.DeviceID] = list;
            }
            else
            {
                List<int> list = new List<int>();
                list.Add(data.Rainfall);
                map.Add(data.DeviceID, list);
            }
        }

        private static Dictionary<string, Device> populateDeviceMap(List<Device> devices)
        {
            Dictionary<string, Device> deviceMap = new Dictionary<string, Device>();
            foreach (Device device in devices)
            {
                if (!deviceMap.ContainsKey(device.ID))
                    deviceMap.Add(device.ID, device);
            }

            return deviceMap;
        }
    }
}
