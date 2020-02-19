using SIS.HTTP.Enums;
using System;

namespace SIS.MvcFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url)
            : base(url)
        {
        }

        public override HttpMethodType Type => HttpMethodType.Get;
    }
}
