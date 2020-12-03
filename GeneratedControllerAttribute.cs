using System;

namespace Shinetech.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GeneratedControllerAttribute : Attribute
    {
        public GeneratedControllerAttribute(string route = "", string description = "")
        {
            Route = route;
            Description = description;
        }
        public GeneratedControllerAttribute()
        {

        }

        public string Route { get; set; }
        public string Description { get; set; }
    }
}
