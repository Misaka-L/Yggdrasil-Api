namespace Yggdrasil.Models.Options {
    public class JwtOption {
        public string Secret { get; set; }
        public int ExpiresHour { get; set; }
    }
}
