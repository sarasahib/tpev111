using Android.App;
using Android.Content;
using Android.Util;

namespace NetMauiStartOnBootPoC.Platforms.Android
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
            // Vérifier si l'intention est correcte
            if (intent.Action != Intent.ActionBootCompleted)
            {
                Log.Warn("BootReceiver", "Action inattendue reçue.");
                return;
            }

            Log.Debug("BootReceiver", "Événement BOOT_COMPLETED détecté.");

            // Lancer MainActivity après redémarrage
            try
            {
                var activityIntent = new Intent(context, typeof(MainActivity));
                activityIntent.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(activityIntent);

                Log.Debug("BootReceiver", "MainActivity lancée avec succès.");
            }
            catch (System.Exception ex)
            {
                Log.Error("BootReceiver", $"Erreur lors du lancement de MainActivity : {ex.Message}");
            }
        }
    }
}