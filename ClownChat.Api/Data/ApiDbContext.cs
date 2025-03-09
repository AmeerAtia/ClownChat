using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public class ApiDbContext : DbContext
{
    public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    public DbSet<ChatMember> ChatMembers => Set<ChatMember>();
    public DbSet<Friendship> Friendships => Set<Friendship>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<User> Users => Set<User>();

    // Helper methods for adding entities
    public User AddUser(string name, string email, string password)
    {
        var user = new User() {
            Name = name,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            CreatedDate = DateTime.UtcNow,
            LastLoginDate = DateTime.UtcNow
        };
        Users.Add(user);
        return user;
    }

    public void AddChatRoom(ChatRoom room) => ChatRooms.Add(room);
    public void AddChatMember(ChatMember member) => ChatMembers.Add(member);
    public void AddFriendship(Friendship friendship) => Friendships.Add(friendship);
    public void AddMessage(Message message) => Messages.Add(message);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Update with your actual connection string
        optionsBuilder.UseSqlCe(@"Data Source=ClownChatDB.sdf");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure ChatRoom
        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.Property<int>("Id").HasField("Id");
            entity.HasKey("Id");
        });

        // Configure ChatMember (composite key)
        modelBuilder.Entity<ChatMember>(entity =>
        {
            // Configure shadow properties for foreign keys
            entity.Property<int>("ChatRoomId");
            entity.Property<int>("UserId");
            
            entity.HasKey("ChatRoomId", "UserId");
            
            entity.HasOne(cm => cm.ChatRoom)
                .WithMany(c => c.Members)
                .HasForeignKey("ChatRoomId");
            
            entity.HasOne(cm => cm.User)
                .WithMany()
                .HasForeignKey("UserId");
        });

        // Configure Friendship (composite key)
        modelBuilder.Entity<Friendship>(entity =>
        {
            entity.Property<int>("XUserId");
            entity.Property<int>("YUserId");
            entity.Property<int>("ChatId");
            
            entity.HasKey("XUserId", "YUserId");
            
            entity.HasOne(f => f.XUser)
                .WithMany(u => u.Friendships)
                .HasForeignKey("XUserId")
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(f => f.YUser)
                .WithMany()
                .HasForeignKey("YUserId")
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(f => f.Chat)
                .WithMany()
                .HasForeignKey("ChatId");
        });

        // Configure Message
        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property<int>("Id").HasField("Id");
            entity.HasKey("Id");
            
            entity.Property<int>("SenderId");
            entity.Property<DateTime>("Date").HasField("Date");
            entity.Property<string>("Content").HasField("Content");
            
            entity.HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey("SenderId");
        });

        // Configure User
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property<int>("Id")
            .HasField("Id")
            .ValueGeneratedOnAdd();
            entity.HasKey("Id");
            
            entity.Property<string>("Name")
                .HasField("Name")
                .HasMaxLength(15);
            
            entity.Property<string>("Email")
                .HasField("Email")
                .HasMaxLength(40);
            
            entity.Property<string>("Password")
                .HasField("Password")
                .HasMaxLength(100);
            
            entity.Property<DateTime>("CreatedDate")
                .HasField("CreatedDate");
            
            entity.Property<DateTime>("LastLoginDate")
                .HasField("LastLoginDate");
        });
    }
}

// Add this to resolve the ChatPermission dependency
/* give me db context of that code in ef core
```
using System.ComponentModel.DataAnnotations;

namespace ClownChat.Api.Data;

public class ChatRoom
{
    [Key]
    public int Id;
    public required ICollection<ChatMember> Members = new List<ChatMember>();
    public required ICollection<Message> Messages = new List<Message>();
}
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

public record Message
{
    public Message(User s, string t)
    {
        Sender = s;
        Content = t;
        Date = DateTime.Now;
    }

    [Key]
    public int Id;
    
    [Required]
    public required User Sender;
    
    [Required]
    public DateTime Date;

    [Required]
    [StringLength(10000)]
    public required string Content;
}

public record User
{
    [Key]
    public int Id;
    
    [Required]
    [StringLength(15)]
    [RegularExpression(@"^[a-zA-Z0-9_\. ]+$")]
    public string Name = string.Empty;

    public ICollection<Friendship> Friendships = new HashSet<Friendship>();
}
```
and add in that dbcontext(ApiDbContext) functions to add message add friendship and add user and chatmember and chat room to the db and use sql ce
*/