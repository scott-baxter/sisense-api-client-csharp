namespace SisenseApiClient.Services.Application.Models
{
    public class ApplicationStatus
    {
        public class LicenseInfo
        {
            public bool IsMobileEnabled { get; set; }
            public bool IsExpired { get; set; }
        }

        public string Version { get; set; }
        public LicenseInfo License { get; set; }
    }
}
