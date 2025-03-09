using System.Runtime.CompilerServices;

namespace ClownChat.Api.Data;

public record ChatMember
{
    public required ChatRoom ChatRoomId;
    public required User UserId;
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