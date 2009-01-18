namespace AjGa.Tsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Position
    {
        private int x;
        private int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X 
        { 
            get
            { 
                return this.x; 
            } 
        }

        public int Y 
        { 
            get 
            { 
                return this.y;
            }
        }
    }
}
