namespace LocalNotifications
{
    public partial class MainPage : ContentPage
    {
      
        public MainPage()
        {
            InitializeComponent();
        }

        private   void Button_Clicked(object sender, EventArgs e)
        {
#if ANDROID
           if(Platform.CurrentActivity is MainActivity activity)
            {
              
                activity.SendNotification();
            }
#endif
        }
    }

}
