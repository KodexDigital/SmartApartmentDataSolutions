namespace Application.Interfaces
{
    public interface ITokenization
    {
        string GetAccessToken(string userEmail);
        string GetToken(string email);
    }
}
