namespace CodeValue.ScrumBoard.Client.Models
{
    public interface ITokenStore
    {
        string Token { get; }

        void StoreToken(string token);
    }

    public class TokenStore : ITokenStore
    {
        public string Token { get; private set; }

        public void StoreToken(string token)
        {
            // TODO : should be cached in disk and loaded later.
            Token = token;
        }
    }
}
