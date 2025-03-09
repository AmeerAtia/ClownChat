using System.Runtime.CompilerServices;

namespace ClownChat.Api.Data;

public record ChatMember
{
    public required int UserId;
    public required int ChatRoomId;
    public required ChatPermission Role;
    public required DateTime JoinDate;
}

public enum ChatPermission
{
    Owner,
    Admin,
    Member,
    Disabled
}