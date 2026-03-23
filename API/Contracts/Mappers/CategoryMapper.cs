using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;

namespace CO4029_BE.API.Contracts.Mappers;

public static class CategoryMapper
{
    public static CategoryReponse ToReponse(this Category category) => new CategoryReponse
    {
        Id = category.ID,
        Name = category.Name,
    };
}