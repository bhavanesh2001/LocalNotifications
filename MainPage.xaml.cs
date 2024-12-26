#if ANDROID
using LocalNotifications.Platforms.Android;
#endif

namespace LocalNotifications
{
    public partial class MainPage : ContentPage
    {
      
        public MainPage()
        {
            InitializeComponent();
#if ANDROID
            type.ItemsSource =  Enum.GetValues(typeof(NotificationType)).Cast<NotificationType>().ToList();
#endif
        }

        private   void Button_Clicked(object sender, EventArgs e)
        {
#if ANDROID
            var notifcationInfo = new NotificationModel() { 
                Title = title.Text, 
                Description = description.Text,
                DisplayType = (NotificationType)type.SelectedItem};
            if (Platform.CurrentActivity is MainActivity activity)
            {
              
                activity.SendNotification(notifcationInfo);
            }
#endif
        }
    }

}
