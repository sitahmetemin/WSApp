using WSApp.Src.Application.DTOs.Base;

namespace WSApp.Src.Application.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public string Brand { get; set; }
        public string DisplaySize { get; set; }
        public string ImageUrl { get; set; }
        public string ModelName { get; set; }
        public string ModelNo { get; set; }
        public string OS { get; set; }
        public string ProcessorBrand { get; set; }
        public string ProcessorType { get; set; }
        public string ProcessorVersion { get; set; }
        public string Ram { get; set; }
        public string Score { get; set; }
        public IList<SellSourceDTO> SellSource { get; set; }
        public string StorageSize { get; set; }
        public string StorageType { get; set; }
    }

    public class SellSourceDTO
    {
        public string Price { get; set; }
        public string ProductUrl { get; set; }
        public string Site { get; set; }
    }
}