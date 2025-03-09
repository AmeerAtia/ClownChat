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