using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NN_Less3.NN
{
    public class Pair<T>
    {
        private T F;
        private T S;

        public T first { get { return F; } set { F = value; } }
        public T second { get { return S; } set { S = value; } }

        public Pair() { F = default(T);  S = default(T); }


        public Pair(T f, T s)
        {
            F = f;
            S = s;
        }
    }
}
