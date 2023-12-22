using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaijaQuickFix.Hubs
{
    public class Hello : Hub
    {
        public void HelloSignalR ()
        {
            this.Clients.All.helloClient(this.Clients.CallerState.prop1 as string);
        }
    }
}