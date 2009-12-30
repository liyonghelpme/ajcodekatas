namespace AjConcurr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public static class GoRoutines
    {
        public static void Go(Action action)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(GoRoutines.RunAction));
            thread.IsBackground = true;
            thread.Start(action);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(RunAction), action);
        }

        public static void Go(ITask task)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(GoRoutines.RunTask));
            thread.IsBackground = true;
            thread.Start(task);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(RunTask), task);
        }

        public static void Go<T>(Action<T> action, T parameter)
        {
            Go(new Task<T>(action, parameter));
        }

        public static void Go<T1, T2>(Action<T1, T2> action, T1 parameter1, T2 parameter2)
        {
            Go(new Task<T1, T2>(action, parameter1, parameter2));
        }

        public static void Go<T1, T2, T3>(Action<T1, T2, T3> action, T1 parameter1, T2 parameter2, T3 parameter3)
        {
            Go(new Task<T1, T2, T3>(action, parameter1, parameter2, parameter3));
        }

        private static void RunAction(object action)
        {
            ((Action)action)();
        }

        private static void RunTask(object task)
        {
            ((ITask)task).Run();
        }
    }
}
