using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Provider;
using Android.Net; // Nécessaire pour Android.Net.Uri
using System.Threading.Tasks;

namespace NetMauiStartOnBootPoC.Platforms.Android
{
    [Activity(
        Label = "NetMauiStartOnBootPoC",
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true)]
    public class MainActivity : MauiAppCompatActivity
    {
        private const int RequestCodeOverlayPermission = 101;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Log.Debug("MainActivity", "OnCreate démarré.");
            base.OnCreate(savedInstanceState);

            Task.Run(async () =>
            {
                await Task.Delay(2000); // Attendez 2 secondes
                CheckOverlayPermission();
            });
        }

        private void CheckOverlayPermission()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M && !Settings.CanDrawOverlays(this))
            {
                ShowOverlayPermissionDialog();
            }
            else
            {
                EnableAutoLaunch();
            }
        }

        private void ShowOverlayPermissionDialog()
        {
            RunOnUiThread(() =>
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Autorisation requise");
                builder.SetMessage("Veuillez activer l'option pour permettre à l'application de fonctionner correctement.");
                builder.SetPositiveButton("Ouvrir les paramètres", (sender, args) =>
                {
                    RequestOverlayPermission();
                });
                builder.SetNegativeButton("Annuler", (sender, args) =>
                {
                    Log.Debug("MainActivity", "L'utilisateur a refusé d'accorder l'autorisation.");
                });
                builder.Show();
            });
        }

        private void RequestOverlayPermission()
        {
            try
            {
                var intent = new Intent(Settings.ActionManageOverlayPermission);
                StartActivityForResult(intent, RequestCodeOverlayPermission);
            }
            catch (System.Exception ex)
            {
                Log.Error("MainActivity", "Erreur lors de l'ouverture des paramètres : " + ex);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == RequestCodeOverlayPermission)
            {
                if (Settings.CanDrawOverlays(this))
                {
                    Log.Debug("MainActivity", "Permission d'affichage par-dessus accordée.");
                    EnableAutoLaunch();
                }
                else
                {
                    Log.Warn("MainActivity", "Permission d'affichage par-dessus refusée.");
                }
            }
        }

        private void EnableAutoLaunch()
        {
            try
            {
                var preferences = GetSharedPreferences("AppPreferences", FileCreationMode.Private);
                var editor = preferences.Edit();
                editor.PutBoolean("AutoLaunchEnabled", true);
                editor.Apply();
                Log.Debug("MainActivity", "Lancement automatique activé.");
            }
            catch (System.Exception ex)
            {
                Log.Error("MainActivity", $"Erreur dans EnableAutoLaunch : {ex}");
            }
        }
    }
}
