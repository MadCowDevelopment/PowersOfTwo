using Microsoft.AspNet.SignalR;
using Microsoft.Owin;

using Owin;

using WebService;

[assembly: OwinStartup(typeof(Startup))]

namespace WebService
{
    public class Startup
    {
        #region Public Methods

        public void Configuration(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);
        }

        #endregion Public Methods
    }
}