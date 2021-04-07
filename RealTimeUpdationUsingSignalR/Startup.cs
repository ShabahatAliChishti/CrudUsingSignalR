using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RealTimeUpdationUsingSignalR.Startup))]

namespace RealTimeUpdationUsingSignalR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
