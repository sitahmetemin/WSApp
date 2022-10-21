using WSApp.Src.Application.DTOs;

namespace WSApp.Models.ResponseModels
{
    public class ProductListModel
    {
        //public IList<BrandDTO> Brands { get; set; }
        //public IList<ProcessNameDTO> ProcessNames { get; set; }
        //public IList<ProcessTypeDTO> ProcessTypes { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}