using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public record Message
{
    [Key]
    public int Id;
    
    [Required]
    public required User UserId;
    
    [Required]
    public required ChatRoom ChatRoomId;
    
    [Required]
    public required Message? RepliedTo;
    
    [Required]
    public required DateTime Date;

    [Required]
    [StringLength(10000)]
    public required string Content;
}
