namespace Pquyquy.JWT.Settings;

public class JWTSetting
{
    public string SecretKey { get; set; }
    public string AudienceToken { get; set; }
    public string IssuerToken { get; set; }
    public int ExpireMinutes { get; set; }
}
