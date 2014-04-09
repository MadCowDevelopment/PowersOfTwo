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
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        #endregion Public Methods
    }
}