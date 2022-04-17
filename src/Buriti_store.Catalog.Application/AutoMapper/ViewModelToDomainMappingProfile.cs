using AutoMapper;
using Buriti_store.Catalog.Application.ViewModels;
using Buriti_store.Catalog.Domain;

namespace Buriti_store.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(p =>
                    new Product(p.Name, p.Description, p.Active,
                        p.Value, p.CategoryId, p.DateRegister,
                        p.Image, new Dimensions(p.Height, p.Width, p.Depth)));

            CreateMap<CategoryViewModel, Category>()
                .ConstructUsing(c => new Category(c.Name, c.Code));
        }
    }
}