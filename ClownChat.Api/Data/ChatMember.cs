using System.Runtime.CompilerServices;

namespace ClownChat.Api.Data;

public record ChatMember
{
    public required ChatRoom ChatRoom;
    public required User User;
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

public class ChatMemberService
{
    public ChatMember create(User x, ChatRoom C)
    {
        var chatRoomMember = new ChatMember() {
            User = x,
            ChatRoom = C,
            Role = ChatPermission.Member,
            JoinDate = DateTime.Now
        };
        return chatRoomMember;
    }
}