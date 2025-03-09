using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public class ChatRoom
{
    [Key]
    public int Id;

    [Required]
    public required string Name;

    [Required]
    public required string About;
}