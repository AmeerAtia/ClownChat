using Microsoft.EntityFrameworkCore;

namespace ClownChat.Api.Data;

public class ApiDbContext : DbContext
{
    public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    public DbSet<GroupMember> ChatMembers => Set<GroupMember>();
    public DbSet<Relation> Friendships => Set<Relation>();
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
    public void AddChatMember(GroupMember member) => ChatMembers.Add(member);
    public void AddFriendship(Relation friendship) => Friendships.Add(friendship);
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
        modelBuilder.Entity<GroupMember>(entity =>
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
        modelBuilder.Entity<Relation>(entity =>
        {
            entity.Property<int>("User1");
            entity.Property<int>("User2");
            entity.Property<int>("ChatId");
            
            entity.HasKey("User1", "User2");
            
            entity.HasOne(f => f.User1)
                .WithMany()
                .HasForeignKey("User")
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(f => f.User2)
                .WithMany()
                .HasForeignKey("User")
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(f => f.ChatId)
                .WithMany()
                .HasForeignKey("ChatId");
        });

        // Configure Message
        modelBuilder.Entity<Message>(entity =>
        {
            entity.Property<int>("Id")
                .HasField("Id")
                .ValueGeneratedOnAdd();
            entity.HasKey("Id");
            
            entity.Property<int>("UserId")
                .HasField("UserId");

            entity.Property<int>("ChatRoomId")
                .HasField("ChatRoomId");

            entity.Property<DateTime>("Date")
                .HasField("Date");

            entity.Property<string>("Content")
                .HasField("Content");
            
            entity.HasOne(m => m.UserId)
                .WithMany()
                .HasForeignKey("UserId");
            
            entity.HasOne(m => m.ChatRoomId)
                .WithMany()
                .HasForeignKey("ChatRoomId");
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
            
            entity.Property<string>("PasswordHash")
                .HasField("Password")
                .HasMaxLength(100);
            
            entity.Property<DateTime>("CreatedDate")
                .HasField("CreatedDate");
            
            entity.Property<DateTime>("LastLoginDate")
                .HasField("LastLoginDate");
        });
    }
}