using static WSApp.Src.Application.Utils.Enums;

namespace WSApp.Src.Application.Models
{
    public class SourceSiteModel
    {
        public string SiteName { get; set; }
        public List<SitePropertiesModel> ProductProperties { get; set; }
    }
}