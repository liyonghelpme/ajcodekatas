namespace Fractal.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public class SectorService
    {
        private CloudStorageAccount account;

        public SectorService(CloudStorageAccount account)
        {
            this.account = account;
        }

        public void PostSectorInfo(SectorInfo sectorInfo)
        {
            CloudQueueClient queueStorage = this.account.CreateCloudQueueClient();
            CloudQueue queue = queueStorage.GetQueueReference("fractaltoprocess");
            queue.AddMessage(this.SectorInfoToMessage(sectorInfo));
        }

        private CloudQueueMessage SectorInfoToMessage(SectorInfo sectorInfo)
        {
            string value = string.Format(
                "SectorInfo {0} {1} {2} {3} {4} {5} {6} {7} {8}",
                sectorInfo.RealMinimum,
                sectorInfo.ImgMinimum,
                sectorInfo.Delta,
                sectorInfo.FromX,
                sectorInfo.FromY,
                sectorInfo.Width,
                sectorInfo.Height,
                sectorInfo.MaxIterations,
                sectorInfo.MaxValue
                );

            return new CloudQueueMessage(value);
        }
    }
}
