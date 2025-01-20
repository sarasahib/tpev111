using Android.App;
using Android.Content;
using Android.OS;

namespace AutoStartApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnStop()
    {
        base.OnStop();

        var context = ApplicationContext;
        var intent = new Intent(context, typeof(MainActivity));
        intent.AddFlags(ActivityFlags.NewTask);
        context.StartActivity(intent);
    }
}
