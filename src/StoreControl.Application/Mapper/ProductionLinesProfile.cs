using AutoMapper;
using StoreControl.Application.Features.ProductionLinesFeatures;
using StoreControl.Application.Features.ProductionLinesFeatures.Commands.CreateProductionLine;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class ProductionLinesProfile : Profile
    {
        public ProductionLinesProfile()
        {
            CreateMap<ProductionLine, ProductionLineDto>();

            CreateMap<CreateProductionLineCommand, ProductionLine>();
        }
    }
}
