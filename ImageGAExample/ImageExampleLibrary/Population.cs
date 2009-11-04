using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageExampleLibrary
{
    public class Population
    {
        private PolygonalImage[] images;

        public Population(PolygonalImage[] images)
        {
            this.images = images;
        }

        public void Evaluate(Evaluator evaluator)
        {
            foreach (PolygonalImage image in this.images)
                if (image.Distance == 0)
                    evaluator.Evaluate(image);
        }

        public PolygonalImage GetBestImage()
        {
            long best = 0;
            PolygonalImage selected = null;

            foreach (PolygonalImage image in this.images)
            {
                if (selected == null || best > image.Distance)
                {
                    best = image.Distance;
                    selected = image;
                }
            }

            return selected;
        }

        public Population Mutate(Mutator mutator, Evaluator evaluator)
        {
            PolygonalImage[] newimages = new PolygonalImage[this.images.Length];

            for (int k = 0; k < this.images.Length; k++)
            {
                PolygonalImage newimage = mutator.Mutate(this.images[k]);

                if (this.images[k].Distance == 0)
                    evaluator.Evaluate(this.images[k]);

                if (this.images[k].Distance < evaluator.Evaluate(newimage))
                    newimages[k] = this.images[k];
                else
                    newimages[k] = newimage;
            }

            newimages = newimages.OrderBy(i => i.Distance).ToArray();

            //PolygonalImage[] newimages = this.images.OrderBy(i => i.Distance).ToArray();

            for (int k = 0; k < newimages.Length / 2; k++)
            {
                // newimages[k] = mutator.Mutate(newimages[k]);
                newimages[k + newimages.Length / 2] = mutator.Mutate(newimages[k]);
            }

            return new Population(newimages);
        }
    }
}
