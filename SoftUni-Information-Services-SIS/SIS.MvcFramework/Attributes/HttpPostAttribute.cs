using SIS.HTTP.Enums;

namespace SIS.MvcFramework.Attributes
{
    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute()
        {
        }

        public HttpPostAttribute(string url)
            : base(url)
        {
        }

        public override HttpMethodType Type => HttpMethodType.Post;
    }
}
