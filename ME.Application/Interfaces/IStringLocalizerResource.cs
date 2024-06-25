using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ME.Application.Interfaces
{
    public interface IStringLocalizerResource
    {
        LocalizedString this[string Key] { get;}
        LocalizedString Get(string key);
    }
}
