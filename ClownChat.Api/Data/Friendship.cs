using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public record Friendship
{
    [Required]
    public required User XUser;
    [Required]
    public required User YUser;
    [Required]
    public required DateTime Date;
    [Required]
    public required ChatRoom Chat;
}