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

public class FriendshipService
{
    public static Friendship create(User xUser, User yUser)
    {
        var friendship = new Friendship() {
            XUser = xUser,
            YUser = yUser,
            Date = DateTime.Now,
            Chat = new ChatRoom() {
                
            }
        };
        xUser.Friendships.Add(friendship);
        yUser.Friendships.Add(friendship);
        return friendship;
    }
}