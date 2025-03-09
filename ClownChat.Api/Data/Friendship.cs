using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public record Friendship
{
    [Required]
    public required int User1;
    [Required]
    public required int User2;
    [Required]
    public required DateTime Date;
    [Required]
    public required int ChatId;
}