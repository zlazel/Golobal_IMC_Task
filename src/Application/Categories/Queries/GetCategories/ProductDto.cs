using AutoMapper;
using Golobal_IMC_Task.Application.Common.Mappings;
using Golobal_IMC_Task.Domain.Entities;

namespace Golobal_IMC_Task.Application.Categorys.Queries.GetCategorys
{
    public class ProductDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category!=null?s.Category.CategoryName: string.Empty));
        }
    }
}
