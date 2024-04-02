using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examDay10Server
{
    public class Singleton<T> where T : class, new()
    {
        static T instance;
        public static T Ins
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }

    }
}
