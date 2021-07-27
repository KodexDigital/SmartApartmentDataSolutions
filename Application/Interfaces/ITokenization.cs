using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITokenization
    {
        string GetAccessToken(string Email);
        string GetToken(Dictionary<string, object> payload);
    }
}
