namespace Infrastructure.Payment
{
    public class StripeOptions
    {
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
        public string Domain { get; set; }
        public string WebHook { get; set; }
    }
}