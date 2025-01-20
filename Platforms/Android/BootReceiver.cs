using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace AutoStartApp.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Action == Intent.ActionBootCompleted)
            {
                var startIntent = new Intent(context, typeof(MainActivity));
                startIntent.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(startIntent);
            }
        }
    }
}
