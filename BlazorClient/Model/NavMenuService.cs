using BlazorClient.Shared;
using System;
using System.Collections.Generic;

namespace BlazorClient
{
    public class NavMenuService
    {
        private List<NavMenu> AdditionalMenuItems { get; set; }
        public event EventHandler<EventArgs> OnChanged;
        public event EventHandler<EventArgs> OnExit;
        public void NotifyChanged()
        {
            OnChanged.Invoke(this, EventArgs.Empty);
        }

        public void StopTimer()
        {
            OnExit.Invoke(this, EventArgs.Empty);
        }
    }
}