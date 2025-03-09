using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public record Message
{
    public Message(User s, string t)
    {
        Sender = s;
        Content = t;
        Date = DateTime.Now;
    }

    [Key]
    public int Id;
    
    [Required]
    public required User Sender;
    
    [Required]
    public required ChatRoom ChatRoomId;
    
    [Required]
    public required DateTime Date;

    [Required]
    [StringLength(10000)]
    public required string Content;
}
