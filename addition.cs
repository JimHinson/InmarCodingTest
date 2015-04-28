using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InmarCodingTest
{
    public class addition
    {
        private int x;
        private int y;

        public int getX()
        {
            return x;
        }

        public void setX(int value)
        {
            x = value;
        }

        public int getY()
        {
            return y;
        }

        public void setY(int value)
        {
            y = value;
        }

        public int addXandY()
        {
            return (x + y);
        }
    }
}
