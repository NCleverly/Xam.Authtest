using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.CommonCore
{
    public interface IContextMenuService
    {
        void ShowContextMenu(View viewRoot, Dictionary<string, Action> menuItems);
    }
}
