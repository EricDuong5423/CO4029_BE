using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;

namespace CO4029_BE.API.Contracts.Mappers;

public static class HistoryMapper
{
    public static HistoryReponse ToReponse(this History history) => new HistoryReponse
    {
        Header = history.header,
        Create_date = history.create_date,
    };
}