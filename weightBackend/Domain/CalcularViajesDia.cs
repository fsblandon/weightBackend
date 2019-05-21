using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace weightBackend.Domain
{
    public class CalcularViajesDia
    {

        private int viajes = 0;
        private List<int> objectsPerDay = new List<int>();

        public CalcularViajesDia()
        {

        }

        public static string CalcularDias(IFormFile file)
        {
            Stream fs = file.OpenReadStream();
            var streamReader = new StreamReader(fs);

            // open file
            var fileOpened = streamReader.ReadToEnd();
            fileOpened = fileOpened.Replace("\n", "");
            var list = fileOpened.Split('\r').ToList();
            list.Remove("");

            List<int> listElements = list.Select(x => Convert.ToInt32(x)).ToList();

            string resultOutput = "";
            int dayPerLine = 0;

            listElements.ForEach(d =>
            {
                dayPerLine++;
                var numObjects = d;
                int index = listElements.IndexOf(d);

                for (int i = index + 1; i <= index + numObjects; i++)
                {
                    objectsPerDay.Add(listElements[i]);
                }
                string resultLine = "Case #" + dayPerLine + ": " + viajes;
                resultOutput = string.Concat(resultOutput, resultLine, Environment.NewLine);

            });

            return resultOutput;
        }

        public static int CalcularViajesDia()
        {
            var maxObject = objectsPerDay.Max();
            objectsPerDay.Remove(maxObject);

            var weight = 0;
            var i = 1;

            while (weight < 50 && maxObject < 50)
            {
                if (objectsPerDay.Count == 0)
                    return 0;

                var minObject = objectsPerDay.Min();
                objectsPerDay.Remove(minObject);
                i++;
                weight = maxObject * i;
            }

            viajes++;

            if (objectsPerDay.Count > 0)
            {
                viajes += CalcularViajesDia();
            }

            return viajes;

        }
    }
}
