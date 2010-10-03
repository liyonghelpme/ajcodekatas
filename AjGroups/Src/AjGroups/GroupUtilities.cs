namespace AjGroups
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

            foreach (IElement element in group.Elements)
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

        public static bool AreEqual(IGroup group1, IGroup group2)
        {
            if (group1.Order != group2.Order)
                return false;

            for (int k = 0; k < group1.Order; k++) {
                if (!group2.Elements.Contains(group1.Elements[k]))
                    return false;
                if (!group1.Elements.Contains(group2.Elements[k]))
                    return false;
            }

            return true;
        }

        public static bool IsNormalSubgroup(IGroup subgroup, IGroup group)
        {
            foreach (Element element1 in group.Elements)
            {
                IList<IElement> left = new List<IElement>();
                IList<IElement> right = new List<IElement>();

                foreach (Element element2 in subgroup.Elements)
                {
                    left.Add(element1.Multiply(element2));
                    right.Add(element2.Multiply(element1));
                }

                foreach (Element e in left)
                    if (!right.Contains(e))
                        return false;

                foreach (Element e in right)
                    if (!left.Contains(e))
                        return false;
            }

            return true;
        }

        // TODO Complete algorithm
        public static bool AreIsomorphic(IGroup group1, IGroup group2)
        {
            if (group1.Order != group2.Order)
                return false;

            if (group1.Equals(group2))
                return true;

            // Assumes order
            //for (int k = 0; k < group1.Order; k++)
            //    if (group1.Elements[k].Order != group2.Elements[k].Order)
            //        return false;

            if (IsCyclic(group1) && IsCyclic(group2))
                return true;

            Dictionary<IElement, IElement> map = new Dictionary<IElement, IElement>();

            if (TryMap(group1, group2, map))
                return true;

            return false;
        }

        public static Boolean IsCyclic(IGroup group)
        {
            foreach (IElement element in group.Elements)
                if (element.Order == group.Order)
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

        private static bool TryMap(IGroup group1, IGroup group2, IDictionary<IElement, IElement> map)
        {
            if (map.Keys.Count == group1.Order)
                return true;

            foreach (IElement element in group1.Elements)
            {
                if (map.Keys.Contains(element))
                    continue;

                if (TryMapTo(group1, group2, element, map))
                    return true;
            }

            return false;
        }

        private static bool TryMapTo(IGroup group1, IGroup group2, IElement element, IDictionary<IElement, IElement> map)
        {
            // It suppossed was tested at TryMap()
            //if (map.Keys.Contains(element))
            //    return false;

            foreach (IElement element2 in group2.Elements)
            {
                IDictionary<IElement, IElement> resultmap = CompatibleMap(group1, group2, map, element, element2);

                if (resultmap != null)
                    if (TryMap(group1, group2, resultmap))
                        return true;
            }

            return false;
        }

        private static IDictionary<IElement, IElement> CompatibleMap(IGroup group1, IGroup group2, IDictionary<IElement, IElement> map, IElement element1, IElement element2)
        {
            if (element1.Order != element2.Order)
                return null;

            if (map.Values.Contains(element2))
                return null;

            Dictionary<IElement, IElement> newmaps = new Dictionary<IElement, IElement>();

            // TODO test isomorphism consistency
            foreach (IElement el1 in map.Keys)
            {
                IElement el2 = map[el1];

                IElement el1element1 = el1.Multiply(element1);
                IElement el2element2 = el2.Multiply(element2);

                if (map.Keys.Contains(el1element1))
                {
                    if (!map[el1element1].Equals(el2element2))
                        return null;
                }
                else
                    newmaps[el1element1] = el2element2;

                IElement element1el1 = element1.Multiply(el1);
                IElement element2el2 = element2.Multiply(el2);

                if (map.Keys.Contains(element1el1))
                {
                    if (!map[element1el1].Equals(element2el2))
                        return null;
                }
                else
                    newmaps[element1el1] = element2el2;
            }

            IDictionary<IElement, IElement> resultmap = new Dictionary<IElement, IElement>(map);
            resultmap[element1] = element2;

            foreach (IElement key in newmaps.Keys)
            {
                resultmap = CompatibleMap(group1, group2, resultmap, key, newmaps[key]);
                if (resultmap == null)
                    return null;
            }

            return resultmap;
        }
    }
}
