using CO4029_BE.API.Contracts.Responses;
using CO4029_BE.Domain.Entities;

namespace CO4029_BE.API.Contracts.Mappers;

public static class ChatboxMapper
{
    public static ChatboxReponse ToReponse(Chatbox chatbox) => new ChatboxReponse
    {
        Id = chatbox.id,
        Content = chatbox.content,
        Contact_person = chatbox.contact_person,
        Contact_time = chatbox.contact_time,
    };
}