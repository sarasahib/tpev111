using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace AutoStartApp.Platforms.Android
{
    [BroadcastReceiver(
        Name = "com.companyname.netmauistartonbootpoc.BootReceiver",
        Enabled = true,
        Exported = true,
        DirectBootAware = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted }, Categories = new[] { Intent.CategoryDefault })]
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
