using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace weightBackend.Domain
{
    public class CalcularViajesDia
    {
        private static List<int> objectsPerDay = new List<int>();

        public CalcularViajesDia()
        {

        }

        public static string CalcularDias(IFormFile file)
        {
            Stream fs = file.OpenReadStream();
            var streamReader = new StreamReader(fs);

            // open file
            //var fileOpened = streamReader.ReadToEnd();
            //fileOpened = fileOpened.Replace("\n", "");
            //List<string> list = fileOpened.Split('\r').ToList();
            //list.Remove("");

            //string str = streamReader.ReadToEnd();
            List<int> dataLines = new List<int>();

            string line;

            while((line = streamReader.ReadLine()) != null)
            {
                dataLines.Add(int.Parse(line));
            }

            //List<int> listElements = new List<int>();
            //listElements = dataLines.Select(int.Parse).ToList();

            string resultOutput = "";
            int dayPerLine = 0;

            dataLines.ForEach(d =>
            {
                dayPerLine++;
                var numObjects = d;
                int index = dataLines.IndexOf(d);

                for (int i = index + 1; i <= index + numObjects; i++)
                {
                    objectsPerDay.Add(dataLines[i]);
                }
                string resultLine = "Case #" + dayPerLine + ": " + CalcularViajes();
                resultOutput = string.Concat(resultOutput, resultLine, Environment.NewLine);
                index = index - 1;
            });

            return resultOutput;
        }

        public static int CalcularViajes()
        {
            var maxObject = objectsPerDay.Max();
            objectsPerDay.Remove(maxObject);

            var weight = 0;
            var i = 1;
            int viajes = 0;

            while (weight < 50 && maxObject < 50)
            {
                if (objectsPerDay.Count == 0)
                {
                    return 0;
                }

                var minObject = objectsPerDay.Min();
                objectsPerDay.Remove(minObject);
                i++;
                weight = maxObject * i;
            }

            if (objectsPerDay.Count > 0)
            {
                viajes += CalcularViajes();
            }

            viajes++;

            return viajes;

        }
    }
}
