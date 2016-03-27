namespace ELibrary.WebAPI.Services
{
    public interface IELibraryIdentityService
    {
        string CurrentUser { get; }
    }
}