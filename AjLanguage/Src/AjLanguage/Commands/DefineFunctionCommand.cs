﻿namespace AjLanguage.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AjLanguage.Expressions;
    using AjLanguage.Language;

    [Serializable]
    public class DefineFunctionCommand : ICommand
    {
        private string name;
        private string[] parameterNames;
        private ICommand body;
        private bool isdefault;
        private bool hasvariableparameters;

        public DefineFunctionCommand(string name, string[] parameterNames, ICommand body, bool isdefault)
            : this(name, parameterNames, body, isdefault, false)
        {
        }

        public DefineFunctionCommand(string name, string[] parameterNames, ICommand body, bool isdefault, bool hasvariableparameters)
        {
            this.name = name;
            this.parameterNames = parameterNames;
            this.body = body;
            this.isdefault = isdefault;
            this.hasvariableparameters = hasvariableparameters;
        }

        public string FunctionName { get { return this.name; } }

        public string[] ParameterNames { get { return this.parameterNames; } }

        public ICommand Body { get { return this.body; } }

        public bool IsDefault { get { return this.isdefault; } }

        public bool HasVariableParameters { get { return this.hasvariableparameters; } }

        public void Execute(IBindingEnvironment environment)
        {
            Machine.Current.Environment.SetValue(this.name, new Function(this.parameterNames, this.body, null, this.isdefault, this.hasvariableparameters));
        }
    }
}
