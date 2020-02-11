using SIS.HTTP.Enums;

namespace SIS.MvcFramework.Attributes
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute()
        {
        }

        public HttpGetAttribute(string url)
            :base(url)
        {
        }

        public override HttpMethodType Type => HttpMethodType.Get;
    }
}
