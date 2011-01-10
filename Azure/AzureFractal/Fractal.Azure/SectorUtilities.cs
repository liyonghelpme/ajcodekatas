namespace Fractal.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure.StorageClient;

    public class SectorUtilities
    {
        public CloudQueueMessage FromSectorInfoToMessage(SectorInfo sectorInfo)
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

        public SectorInfo FromMessageToSectorInfo(CloudQueueMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}
