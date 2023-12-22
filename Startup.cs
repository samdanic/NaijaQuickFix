using Owin;
using Microsoft.Owin; 
[assembly:OwinStartup(typeof(NaijaQuickFix.Startup))]

namespace NaijaQuickFix
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}