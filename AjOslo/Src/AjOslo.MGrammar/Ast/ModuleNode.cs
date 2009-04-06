namespace AjOslo.MGrammar.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ModuleNode
    {
        private List<LanguageNode> languages = new List<LanguageNode>();

        public ModuleNode(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }

        public ICollection<LanguageNode> Languages
        {
            get
            {
                return this.languages;
            }
        }

        public void AddLanguage(LanguageNode language)
        {
            languages.Add(language);
        }
    }
}
