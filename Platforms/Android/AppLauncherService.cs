using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Java.Lang;
using NetMauiStartOnBootPoC.Services;
using AndroidProvider = Android.Provider;
using AndroidNet = Android.Net;

namespace NetMauiStartOnBootPoC.Platforms.Android
{
    internal class AppLauncherService : IAppLauncherService
    {
        public void AskManageOverlayPermission()
        {
            try
            {
                var context = Platform.AppContext;

                if (Build.VERSION.SdkInt >= BuildVersionCodes.M && !AndroidProvider.Settings.CanDrawOverlays(context))
                {
                    var intent = new Intent(AndroidProvider.Settings.ActionManageOverlayPermission,
                        AndroidNet.Uri.Parse($"package:{context.PackageName}"));
                    intent.AddFlags(ActivityFlags.NewTask);
                    context.StartActivity(intent);
                }
                else
                {
                    Log.Debug("AppLauncherService", "Permission d'affichage par-dessus déjà accordée.");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error("AppLauncherService", $"Erreur lors de la demande de permission : {ex}");
            }
        }

        public void RestartApplication()
        {
            try
            {
                var context = Platform.AppContext;
                PackageManager packageManager = context.PackageManager!;
                Intent? intent = packageManager.GetLaunchIntentForPackage(context.PackageName!);

                if (intent == null)
                {
                    Log.Error("AppLauncherService", "Impossible de trouver l'intention pour redémarrer l'application.");
                    return;
                }

                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                context.StartActivity(intent);
                Runtime.GetRuntime()?.Exit(0);
            }
            catch (System.Exception ex)
            {
                Log.Error("AppLauncherService", $"Erreur lors du redémarrage de l'application : {ex}");
            }
        }
    }
}
