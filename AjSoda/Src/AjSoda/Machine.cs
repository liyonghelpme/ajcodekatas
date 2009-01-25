namespace AjSoda
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Machine
    {
        public Machine()
        {
            BaseBehavior behavior = new BaseBehavior();
            IBehavior objectBehavior = behavior.CreateDelegated();
            
            objectBehavior.Parent = null;
            behavior.Parent = objectBehavior;
            
            this.Object = new BaseObject();
            this.Object.Behavior = objectBehavior;

            objectBehavior.Send("methodAt:put:", "vtable", new BaseBehaviorMethod());
            objectBehavior.Send("methodAt:put:", "delegated", new BaseObjectDelegateMethod());

            this.Behavior = behavior;
        }

        public IBehavior Behavior { get; private set;  }

        public IObject Object { get; private set; }
    }
}
