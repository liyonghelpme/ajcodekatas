﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MPI;

namespace Fractal.MPIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MPI.Environment environment = new MPI.Environment(ref args))
            {
                if (args[4].Equals("*"))
                    args[4] = "0";

                SectorInfo sectorinfo = new SectorInfo()
                {
                    RealMinimum = Convert.ToDouble(args[0]),
                    ImgMinimum = Convert.ToDouble(args[1]),
                    Delta = Convert.ToDouble(args[2]),
                    FromX = Convert.ToInt32(args[3]),
                    FromY = Convert.ToInt32(args[4]),
                    Width = Convert.ToInt32(args[5]),
                    Height = Convert.ToInt32(args[6]),
                    MaxIterations = Convert.ToInt32(args[7]),
                    MaxValue = Convert.ToInt32(args[8])
                };

                Intracommunicator comm = Communicator.world;

                if (comm.Rank == 0)
                {
                    SectorInfo[] sectors = new SectorInfo[comm.Size];

                    for (int k = 0; k < sectors.Length; k++)
                    {
                        SectorInfo newsector = new SectorInfo()
                        {
                            RealMinimum = sectorinfo.RealMinimum,
                            ImgMinimum = sectorinfo.ImgMinimum,
                            Delta = sectorinfo.Delta,
                            FromX = sectorinfo.FromX,
                            FromY = sectorinfo.FromY + k * (sectorinfo.Height / comm.Size),
                            Width = sectorinfo.Width,
                            Height = sectorinfo.Height / comm.Size,
                            MaxIterations = sectorinfo.MaxIterations,
                            MaxValue = sectorinfo.MaxValue
                        };

                        sectors[k] = newsector;
                    }

                    SectorInfo si = comm.Scatter(sectors);

                    CalculateSector(args, si);
                }
                else
                {
                    SectorInfo si = comm.Scatter<SectorInfo>(0);
                    CalculateSector(args, si);
                }
            }
        }

        private static void CalculateSector(string[] args, SectorInfo sectorinfo)
        {
            Calculator calculator = new Calculator();

            Sector sector = calculator.CalculateSector(sectorinfo);

            SectorSerializer serializer = new SectorSerializer();

            string filename = string.Format("{0}-{1}-{2}-{3}-{4}.bin", args[9], sectorinfo.FromX, sectorinfo.FromY, sectorinfo.Width, sectorinfo.Height);

            serializer.Serialize(sector, filename);
        }
    }
}
