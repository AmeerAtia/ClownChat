using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Database;

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
    public DateTime Date;

    [Required]
    [StringLength(10000)]
    public required string Content;
}
