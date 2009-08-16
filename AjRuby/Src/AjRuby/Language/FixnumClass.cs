﻿namespace AjRuby.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FixnumClass : BaseClass
    {
        private static FixnumClass instance = new FixnumClass();

        public static FixnumClass Instance { get { return instance; } }

        private FixnumClass()
        {
        }
    }
}