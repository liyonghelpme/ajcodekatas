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

            IObject obj = this.CreatePrototype("Object");

            ((IBehavior) obj.Behavior).Parent = null;
            this.baseClass.Parent = obj.Behavior;
            machineClass.Parent = obj.Behavior;

            obj.Behavior.Send("methodAt:put:", "vtable", new BaseBehaviorMethod());
            obj.Behavior.Send("methodAt:put:", "delegated", new BaseObjectDelegateMethod());
        }

        public IObject CreatePrototype(string prototypeName)
        {
            BaseClass cls = (BaseClass) this.baseClass.CreateDelegated();
            cls.Machine = this;
            IObject obj = cls.CreateInstance();
            this.globals[prototypeName] = obj;
            return obj;
        }

        public IClass CreateClass()
        {
            BaseClass cls = (BaseClass)this.baseClass.CreateDelegated();
            cls.Machine = this;
            return cls;
        }

        public IObject CreatePrototype(string prototypeName, string superName, List<string> variableNames)
        {
            IObject super = (IObject) this.globals[superName];
            IBehavior superclass = (IBehavior) super.Behavior;
            IClass cls = (IClass) superclass.CreateDelegated();

            if (variableNames != null)
            {
                foreach (string name in variableNames)
                {
                    cls.AddVariable(name);
                }
            }

            IObject obj = cls.CreateInstance();
            this.globals[prototypeName] = obj;
            return obj;
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
