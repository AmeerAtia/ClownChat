using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public record Message
{
    [Key]
    public int Id;
    
    [Required]
    public required int Sender;
    
    [Required]
    public required int ChatRoomId;
    
    [Required]
    public required DateTime Date;

    [Required]
    [StringLength(10000)]
    public required string Content;
}
