using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalNotifications.Platforms.Android
{
    public class AndroidConstants
    {
        public const string STATUS_BAR_CHANNEL_ID = "1";
        public const string STATUS_BAR_CHANNEL_NAME = "Status Bar Notifications";
        public const string STATUS_BAR_CHANNEL_DESCRIPTION = @"This Notification first appears as an icon in the status bar. 
                                                              User can open notification drawer, where they can view more details of notification";    
    }
}
