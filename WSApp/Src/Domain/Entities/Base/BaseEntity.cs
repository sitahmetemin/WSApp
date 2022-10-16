using WSApp.Src.Domain.Entities.Base.Abstraction;

namespace WSApp.Src.Domain.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }
    }
}
