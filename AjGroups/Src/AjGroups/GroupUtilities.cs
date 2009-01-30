namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class GroupUtilities
    {
        internal static List<IGroup> GroupsClosure(ICollection<IGroup> initialGroups)
        {
            List<IGroup> groups = new List<IGroup>(initialGroups);
            int lastprocessed = 0;

            while (lastprocessed < groups.Count)
            {
                List<IGroup> newgroups = new List<IGroup>();

                for (int k = 0; k < groups.Count; k++)
                {
                    for (int j = lastprocessed; j < groups.Count; j++)
                    {
                        IGroup newgroup = GroupUtilities.Multiply(groups[k],groups[j]);

                        if (!newgroups.Contains(newgroup) && !groups.Contains(newgroup))
                        {
                            newgroups.Add(newgroup);
                        }

                        newgroup = GroupUtilities.Multiply(groups[j], groups[k]);

                        if (!newgroups.Contains(newgroup) && !groups.Contains(newgroup))
                        {
                            newgroups.Add(newgroup);
                        }
                    }
                }

                lastprocessed = groups.Count;

                groups.AddRange(newgroups);
            }

            return groups;
        }

        public static IGroup Multiply(IGroup group1, IGroup group2)
        {
            return new CompleteGroup(ElementUtilities.ElementsClosure(ElementUtilities.ElementsUnion(group1.Elements, group2.Elements)));
        }

        public static List<IGroup> GetSubgroups(IGroup group)
        {
            List<IGroup> cyclicGroups = new List<IGroup>();

            foreach (Element element in group.Elements) 
            {
                IGroup newgroup = new GeneratedGroup(element);

                if (!cyclicGroups.Contains(newgroup))
                    cyclicGroups.Add(newgroup);
            }

            return GroupsClosure(cyclicGroups);
        }

        public static bool AreEquals(IGroup group1, IGroup group2)
        {
            if (group1.Order != group2.Order)
            {
                return false;
            }

            for (int k = 0; k < group1.Order; k++)
            {
                if (!group1.Elements[k].Equals(group2.Elements[k]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
