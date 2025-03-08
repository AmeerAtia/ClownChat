using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public class ChatRoom
{
    [Key]
    public int Id;
    public required ICollection<ChatMember> Members = new List<ChatMember>();
    public required ICollection<Message> Messages = new List<Message>();
}