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
            this.Object = new BaseObject(2);
            this.Object.Behavior = behavior;
            behavior.Parent = this.Object;
            this.Behavior = behavior;
        }

        public IObject Behavior { get; private set;  }

        public IObject Object { get; private set; }
    }
}
