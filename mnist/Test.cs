using mnist.VectorMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnist
{
    internal struct Test
    {
        public Test(Vector input, Vector expected)
        {
            Input = input;
            Expected = expected;
        }
        public Vector Input;
        public Vector Expected;
    }
}
