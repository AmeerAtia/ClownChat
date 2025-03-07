using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Database;

public class ChatRoom
{
    [Key]
    public int Id { get; set; }
    public ICollection<ChatMember> Members = new List<ChatMember>();
    public ICollection<Message> Messages = new List<Message>();
}