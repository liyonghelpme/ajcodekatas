namespace AjOslo.MGrammar.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LanguageNode
    {
        private List<LanguageElement> elements = new List<LanguageElement>();

        public LanguageNode(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
