namespace Fractal.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure.StorageClient;

    public static class SectorUtilities
    {
        public static CloudQueueMessage FromSectorInfoToMessage(SectorInfo sectorInfo)
        {
            string value = string.Format(
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
                FromX = Int32.Parse(parameters[2]),
                FromY = Int32.Parse(parameters[3]),
                Width = Int32.Parse(parameters[4]),
                Height = Int32.Parse(parameters[5]),
                RealMinimum = Double.Parse(parameters[6]),
                ImgMinimum = Double.Parse(parameters[7]),
                Delta = Double.Parse(parameters[8]),
                MaxIterations = Int32.Parse(parameters[9]),
                MaxValue = Int32.Parse(parameters[10])
            };

            return value;
        }
    }
}
