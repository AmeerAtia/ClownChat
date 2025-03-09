using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public record Friendship
{
    [Required]
    public required User User1;
    [Required]
    public required User User2;
    [Required]
    public required DateTime Date;
    [Required]
    public required ChatRoom ChatId;
}