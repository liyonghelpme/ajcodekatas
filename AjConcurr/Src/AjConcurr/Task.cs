namespace AjConcurr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Linq.Expressions;

    public class Task<T> : ITask
    {
        private Action<T> action;
        private T parameter;

        public Task(Action<T> action, T parameter)
        {
            this.action = action;
            this.parameter = parameter;
        }

        public void Run()
        {
            this.action(this.parameter);
        }
    }

    public class Task<T1, T2, T3> : ITask
    {
        private Action<T1, T2, T3> action;
        private T1 parameter1;
        private T2 parameter2;
        private T3 parameter3;

        public Task(Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            this.action = action;
            this.parameter1 = parameter1;
            this.parameter2 = parameter2;
            this.parameter3 = parameter3;
        }

        public void Run()
        {
            this.action(this.parameter1, this.parameter2, this.parameter3);
        }
    }

    public class Task<T1, T2> : ITask
    {
        private Action<T1, T2> action;
        private T1 parameter1;
        private T2 parameter2;

        public Task(Action<T1, T2> action, T1 parameter1, T2 parameter2)
        {
            this.action = action;
            this.parameter1 = parameter1;
            this.parameter2 = parameter2;
        }

        public void Run()
        {
            this.action(this.parameter1, this.parameter2);
        }
    }
}
