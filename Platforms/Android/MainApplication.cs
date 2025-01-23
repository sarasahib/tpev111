using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.App;

namespace MauiApp1ATM.Platforms.Android
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override void OnCreate()
        {
            base.OnCreate();

            // Création du canal de notification pour Android 8.0 et versions ultérieures
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                CreateNotificationChannel();
            }
        }

        private void CreateNotificationChannel()
        {
            // Configuration du canal de notification
            var channel = new NotificationChannel(
                "channel_id", // ID du canal
                "Channel Name", // Nom du canal
                NotificationImportance.Default) // Importance de la notification
            {
                Description = "Description of the channel" // Description
            };

            // Gestionnaire de notifications
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager?.CreateNotificationChannel(channel);
        }
    }
}
