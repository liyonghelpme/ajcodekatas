﻿namespace AjConcurr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    public class Channel
    {
        private AutoResetEvent sethandle = new AutoResetEvent(false);
        private AutoResetEvent gethandle = new AutoResetEvent(false);
        private object value;

        public void Send(object value)
        {
            lock (sethandle)
            {
                this.gethandle.WaitOne();
                this.value = value;
                this.sethandle.Set();
            }
        }

        public object Receive()
        {
            lock (gethandle)
            {
                this.gethandle.Set();
                this.sethandle.WaitOne();

                object result = this.value;
                return result;
            }
        }
    }
}
