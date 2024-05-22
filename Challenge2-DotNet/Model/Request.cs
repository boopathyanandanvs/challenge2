using System.ComponentModel.DataAnnotations;

namespace Challenge2_DotNet.Model
{
    public class Request
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientSecret { get; set; }
        [Required]
        public string TenantId { get; set; }
        [Required]
        public string SubscriptionId { get; set; }
        [Required]
        public string ResourceGroup { get; set; }
        [Required]
        public string ResourceProvider { get; set; }
        [Required]
        public string ApiVersion { get; set; }
        [Required]
        public string ResourceType { get; set; }
        [Required]
        public string ResourceName { get; set; }
    }
}
