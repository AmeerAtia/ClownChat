using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public record Relation
{
    [Required]
    public required RelationType[] Type;
    [Required]
    public required User User1Id;
    [Required]
    public required User User2Id;
    [Required]
    public required DateTime Date;
    [Required]
    public required ChatRoom ChatId;
}

public enum RelationType: byte
{
    Brother,
    Friend,
    Contact,
    Blocked
}