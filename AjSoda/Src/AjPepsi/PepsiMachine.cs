namespace AjPepsi
{
    using System;
    using System.Collections.Generic;

    using AjSoda;

    public class PepsiMachine : BaseObject
    {
        private BaseClass baseClass;

        private Dictionary<string, object> globals = new Dictionary<string, object>();

        public PepsiMachine()
        {
            this.baseClass = new BaseClass();
            BaseClass machineClass = (BaseClass) this.baseClass.CreateDelegated();
            this.Behavior = machineClass;
            machineClass.Machine = this;

            this.CreateClass("Object");
        }

        public IClass CreateClass(string className)
        {
            BaseClass cls = (BaseClass) this.baseClass.CreateDelegated();
            cls.Machine = this;
            this.globals[className] = cls;
            return cls;
        }

        public IClass CreateClass(string clsname, IBehavior superclass)
        {
            IClass cls = (IClass) superclass.CreateDelegated();
            this.globals[clsname] = cls;
            return cls;
        }

        public object GetGlobalObject(string objname)
        {
            if (this.globals.ContainsKey(objname))
            {
                return this.globals[objname];
            }

            return null;
        }

        public void SetGlobalObject(string objname, object value)
        {
            this.globals[objname] = value;
        }
    }
}
