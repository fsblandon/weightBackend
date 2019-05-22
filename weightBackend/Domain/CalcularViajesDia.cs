using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace weightBackend.Domain
{
    public class CalcularViajesDia
    {

        public CalcularViajesDia()
        {

        }

        public static string CalcularDias(IFormFile file)
        {
            Stream fs = file.OpenReadStream();
            var streamReader = new StreamReader(fs);

            List<int> dataLines = new List<int>();

            string line;

            while((line = streamReader.ReadLine()) != null)
            {
                dataLines.Add(int.Parse(line));
            }

            string resultOutput = "";
            int dayPerLine = 0;
            List<int> objectsPerDay = new List<int>();

            for(int index = 1; index < dataLines.Count; index ++)
            {
                dayPerLine++;
                var numObjects = dataLines[index];
                int count;

                for (count = index + 1; count <= (index + numObjects); count++)
                {
                    objectsPerDay.Add(dataLines[count]);
                }
                string resultLine = "Case #" + dayPerLine + ": " + CalcularViajes(objectsPerDay);
                resultOutput = string.Concat(resultOutput, resultLine, Environment.NewLine);
                index = count - 1;
            }

            return resultOutput;
        }

        public static int CalcularViajes(List<int> objectsPerDay)
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

            viajes++;

            if (objectsPerDay.Count > 0)
            {
                viajes += CalcularViajes(objectsPerDay);
            }

            return viajes;

        }
    }
}
