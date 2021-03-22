using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Interfuze
{
    public class CsvParser
    {
        public CsvParser()
        {

        }

        public List<Data> ParseDataCSV(string fileName)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Resource\", fileName);

            List<Data> list = new List<Data>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                bool firstLine = true;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (firstLine)
                    {
                        firstLine = false;

                        continue;
                    }

                    string dateDefault = "2021-01-01 " + fields[1].Substring(10);
                    Data data = new Data(fields[0],
                            DateTime.ParseExact(dateDefault, "yyyy-MM-dd H:mm", CultureInfo.InvariantCulture),
                            Convert.ToInt32(fields[2]));
                    list.Add(data);
                }
            }

            return list;
        }

        public List<Device> ParseDeviceCSV(string fileName)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, @"Resource\", fileName);

            List<Device> list = new List<Device>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                bool firstLine = true;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (firstLine)
                    {
                        firstLine = false;

                        continue;
                    }

                    Device device = new Device(fields[0], fields[1], fields[2]);
                    list.Add(device);
                }
            }

            return list;
        }
    }
}
