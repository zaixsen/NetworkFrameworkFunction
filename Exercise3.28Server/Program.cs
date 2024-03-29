using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3._28Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetMgr.Ins.Init();

            LoginMgr.Ins.Init();

            PlayerStateMgr.Ins.Init();

            while (true)
            {

            }
        }
    }
}
