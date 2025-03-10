namespace ClownChat.Api.Data;

public class Room
{
    [Key]
    public int Id;

    [Required]
    public required string Name;

    [Required]
    public required RoomType Type;
    
    public required ChatRole? ChatRole;
    // public required MeetRole? MeetRole;

    [Required]
    public required string About;
}

public enum RoomType
{
    ChatOnly,
    MeetOnly,
    ChatMeet,
    Private,
}