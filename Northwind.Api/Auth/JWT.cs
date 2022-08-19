namespace Northwind.Api.Auth
{
    public class JWT
    {
        public string Access_Token { get; set; }
        public string Token_type { get; set; } = "bearer";
        public int Expires_in { get; set; }
        public string Refresh_token { get; set; }        
    }
}
