using WSApp.Src.Application.DTOs.Base;
using static WSApp.Src.Application.Utils.Enums;

namespace WSApp.Src.Application.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public string ModelNo { get; set; }
        public OSName OS { get; set; }
        public string ProcessorBrand { get; set; }
        public string ProcessorType { get; set; }
        public string ProcessorVersion { get; set; }
        public int Ram { get; set; }
        public int StorageSize { get; set; }
        public StorageType StorageType { get; set; }
        public double DisplaySize { get; set; }
        public double Score { get; set; }
        public double Prices { get; set; }
        public string ImageUrl { get; set; }
        public string Site { get; set; }
    }
}