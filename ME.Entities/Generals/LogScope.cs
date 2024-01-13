using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME.Entities.Generals
{
    [Flags]
    public enum LogScope
    {
        Developer = 1,
        Client = 2,
        All = Developer | Client,


    }
}
