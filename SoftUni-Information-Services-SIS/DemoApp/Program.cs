using SIS.MvcFramework;
using System.Threading.Tasks;

namespace DemoApp
{
    public static class Program
    {
        public static async Task Main()
        {
            await WebHost.StartAsync(new StartUp());
        }

    }
}