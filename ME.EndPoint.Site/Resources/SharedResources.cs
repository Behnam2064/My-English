using ME.Application.Interfaces;
using ME.EndPoint.Site.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ME.Application.Resources
{
    public class SharedResources : IStringLocalizerResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResources(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);

        }

        public LocalizedString this[string Key] 
        { 
            get => _localizer[Key];
        }

        public LocalizedString Get(string key)
        {
            return this[key];
        }

    }
}
