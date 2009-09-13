namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ElementUtilities
    {
        internal static List<IElement> ElementsClosure(ICollection<IElement> initialElements)
        {
            List<IElement> elements = new List<IElement>(initialElements);
            int lastprocessed = 0;

            while (lastprocessed < elements.Count)
            {
                List<IElement> newelements = new List<IElement>();

                for (int k = 0; k < elements.Count; k++)
                {
                    for (int j = lastprocessed; j < elements.Count; j++)
                    {
                        IElement newelement = elements[k].Multiply(elements[j]);

                        if (!newelements.Contains(newelement) && !elements.Contains(newelement))
                        {
                            newelements.Add(newelement);
                        }

                        newelement = elements[j].Multiply(elements[k]);

                        if (!newelements.Contains(newelement) && !elements.Contains(newelement))
                        {
                            newelements.Add(newelement);
                        }
                    }
                }

                lastprocessed = elements.Count;

                elements.AddRange(newelements);
            }

            return elements;
        }

        internal static List<IElement> ElementsUnion(ICollection<IElement> elements1, ICollection<IElement> elements2) 
        {
            List<IElement> elements = new List<IElement>(elements1);

            foreach (IElement element in elements2)
            {
                if (!elements.Contains(element))
                {
                    elements.Add(element);
                }
            }

            return elements;
        }
    }
}
