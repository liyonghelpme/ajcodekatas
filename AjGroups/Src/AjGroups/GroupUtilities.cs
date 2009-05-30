﻿namespace AjGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class GroupUtilities
    {
        public static IGroup Multiply(IGroup group1, IGroup group2)
        {
            return new CompleteGroup(ElementUtilities.ElementsClosure(ElementUtilities.ElementsUnion(group1.Elements, group2.Elements)));
        }

        public static List<IGroup> GetCyclicSubgroups(IGroup group)
        {
            List<IGroup> cyclicGroups = new List<IGroup>();

            foreach (Element element in group.Elements)
            {
                IGroup newgroup = new GeneratedGroup(element);

                if (!cyclicGroups.Contains(newgroup))
                    cyclicGroups.Add(newgroup);
            }

            return cyclicGroups;
        }

        public static IEnumerable<IGroup> GetSubgroups(IGroup group)
        {
            return GroupsClosure(GetCyclicSubgroups(group));
        }

        public static IEnumerable<IGroup> GetNormalSubgroups(IGroup group)
        {
            return GetSubgroups(group).Where(sg => IsNormalSubgroup(sg, group));
        }

        public static bool AreEquals(IGroup group1, IGroup group2)
        {
            if (group1.Order != group2.Order)
                return false;

            for (int k = 0; k < group1.Order; k++)
                if (!group1.Elements[k].Equals(group2.Elements[k]))
                    return false;

            return true;
        }

        public static bool IsNormalSubgroup(IGroup subgroup, IGroup group)
        {
            foreach (Element element1 in subgroup.Elements)
                foreach (Element element2 in group.Elements)
                {
                    if (!subgroup.Elements.Contains(element1.Multiply(element2)))
                        return false;
                    if (!subgroup.Elements.Contains(element2.Multiply(element1)))
                        return false;
                }

            return true;
        }

        public static bool AreIsomorphic(IGroup group1, IGroup group2)
        {
            if (group1.Order != group2.Order)
                return false;

            if (group1.Equals(group2))
                return true;

            for (int k = 0; k < group1.Order; k++)
                if (group1.Elements[k].Order != group2.Elements[k].Order)
                    return false;

            List<IGroup> cyclics1 = GetCyclicSubgroups(group1);
            List<IGroup> cyclics2 = GetCyclicSubgroups(group2);

            if (cyclics1.Count == 2 && cyclics2.Count == 2)
                return true;

            return false;
        }

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
                        IGroup newgroup = GroupUtilities.Multiply(groups[k], groups[j]);

                        if (!newgroups.Contains(newgroup) && !groups.Contains(newgroup))
                            newgroups.Add(newgroup);

                        newgroup = GroupUtilities.Multiply(groups[j], groups[k]);

                        if (!newgroups.Contains(newgroup) && !groups.Contains(newgroup))
                            newgroups.Add(newgroup);
                    }
                }

                lastprocessed = groups.Count;

                groups.AddRange(newgroups);
            }

            return groups;
        }
    }
}