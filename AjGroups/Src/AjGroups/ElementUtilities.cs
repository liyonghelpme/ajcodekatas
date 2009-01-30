namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ElementUtilities
    {
        internal static List<Element> ElementsClosure(ICollection<Element> initialElements)
        {
            List<Element> elements = new List<Element>(initialElements);
            int lastprocessed = 0;

            while (lastprocessed < elements.Count)
            {
                List<Element> newelements = new List<Element>();

                for (int k = 0; k < elements.Count; k++)
                {
                    for (int j = lastprocessed; j < elements.Count; j++)
                    {
                        Element newelement = elements[k].Multiply(elements[j]);

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

        internal static List<Element> ElementsUnion(ICollection<Element> elements1, ICollection<Element> elements2) 
        {
            List<Element> elements = new List<Element>(elements1);

            foreach (Element element in elements2)
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
