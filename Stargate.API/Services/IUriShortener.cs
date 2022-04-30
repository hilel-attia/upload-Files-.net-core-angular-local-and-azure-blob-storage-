namespace Stargate.API.Services
{
    public interface IUriShortener
    {
        string GetExternalUri(int id);
    }
}