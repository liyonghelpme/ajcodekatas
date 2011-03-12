namespace Fractal.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure.StorageClient;
    using System.Globalization;

    public static class SectorUtilities
    {
        public static CloudQueueMessage FromSectorInfoToMessage(SectorInfo sectorInfo)
        {
            string value = string.Format(CultureInfo.InvariantCulture,
                "SectorInfo {0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                sectorInfo.Id,
                sectorInfo.FromX,
                sectorInfo.FromY,
                sectorInfo.Width,
                sectorInfo.Height,
                sectorInfo.RealMinimum,
                sectorInfo.ImgMinimum,
                sectorInfo.Delta,
                sectorInfo.MaxIterations,
                sectorInfo.MaxValue
                );

            return new CloudQueueMessage(value);
        }

        public static SectorInfo FromMessageToSectorInfo(CloudQueueMessage msg)
        {
            string[] parameters = msg.AsString.Split(' ');
            SectorInfo value = new SectorInfo()
            {
                Id = new Guid(parameters[1]),
                FromX = Int32.Parse(parameters[2], CultureInfo.InvariantCulture),
                FromY = Int32.Parse(parameters[3], CultureInfo.InvariantCulture),
                Width = Int32.Parse(parameters[4], CultureInfo.InvariantCulture),
                Height = Int32.Parse(parameters[5], CultureInfo.InvariantCulture),
                RealMinimum = Double.Parse(parameters[6], CultureInfo.InvariantCulture),
                ImgMinimum = Double.Parse(parameters[7], CultureInfo.InvariantCulture),
                Delta = Double.Parse(parameters[8], CultureInfo.InvariantCulture),
                MaxIterations = Int32.Parse(parameters[9], CultureInfo.InvariantCulture),
                MaxValue = Int32.Parse(parameters[10], CultureInfo.InvariantCulture)
            };

            return value;
        }
    }
}
