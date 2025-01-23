namespace NetMauiStartOnBootPoC.Services
{
    public interface IAppLauncherService
    {
        void AskManageOverlayPermission();
        void RestartApplication();
    }
}
