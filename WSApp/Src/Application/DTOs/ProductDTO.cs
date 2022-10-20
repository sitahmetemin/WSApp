using WSApp.Src.Application.DTOs.Base;
using static WSApp.Src.Application.Utils.Enums;

namespace WSApp.Src.Application.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public string ModelNo { get; set; }
        public string OS { get; set; }
        public string ProcessorBrand { get; set; }
        public string ProcessorType { get; set; }
        public string ProcessorVersion { get; set; }
        public string Ram { get; set; }
        public string StorageSize { get; set; }
        public string StorageType { get; set; }
        public string DisplaySize { get; set; }
        public string Score { get; set; }
        public string Prices { get; set; }
        public string ImageUrl { get; set; }
        public string Site { get; set; }
    }
}