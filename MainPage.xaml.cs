

namespace LocalNotifications
{
    public partial class MainPage : ContentPage
    {

        INotificationManagerService? notificationManager { get; }
        public MainPage()
        {
            InitializeComponent();
            notificationManager = IPlatformApplication.Current?.Services.GetService<INotificationManagerService>();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
            notificationManager?.SendNotification(title.Text, description.Text);
        }
    }

}
