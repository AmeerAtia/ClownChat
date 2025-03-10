namespace ClownChat.Api.Data;

public class Group
{
    [Key]
    public int Id;

    [Required]
    public required string Name;

    [Required]
    public required string About;

    [Required]
    public required User OwnerId;
}
