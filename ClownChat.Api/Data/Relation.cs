namespace ClownChat.Api.Data;

public class Relation
{
    [Key]
    public int Id;

    [Required]
    public required RelationType Type;

    [Required]
    public required DateTime Date;

    [Required]
    public required User User1Id;

    [Required]
    public required User User2Id;

    [Required]
    public required Room ChatId;
}

public enum RelationType: byte
{
    Brother,
    Friend,
    Contact,
    Blocked
}