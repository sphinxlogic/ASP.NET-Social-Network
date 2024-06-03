namespace Fisharoo.FisharooWeb.Profiles.Interface
{
    public interface IUploadAvatar
    {
        void ShowMessage(string Message);
        void ShowCropPanel();
        void ShowApprovePanel();
        void ShowUploadPanel();
    }
}