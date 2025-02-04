

namespace LocalNotifications
{
    public partial class MainPage : ContentPage
    {

        INotificationManagerService? notificationManager { get; }
        public MainPage()
        {
            InitializeComponent();
            notificationManager = IPlatformApplication.Current?.Services.GetService<INotificationManagerService>();
            type.ItemsSource = Enum.GetNames(typeof(NotificationType));

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(await Permissions.CheckStatusAsync<Permissions.PostNotifications>() != PermissionStatus.Granted)
               await Permissions.RequestAsync<Permissions.PostNotifications>();
            notificationManager?.SendNotification(title.Text, description.Text,(NotificationType)type.SelectedIndex);
        }
    }

}
