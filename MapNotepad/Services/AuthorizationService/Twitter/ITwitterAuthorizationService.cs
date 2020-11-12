namespace MapNotepad.Services.AuthorizationService.Twitter
{
    public interface ITwitterAuthorizationService
    {
        void Login();
        void RegisterAuthDelegate(IAuthDelegate authDelegate);
    }
}
