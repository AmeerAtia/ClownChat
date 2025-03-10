namespace ClownChat.Api.Data;

public class User
{
    [Key]
    public int Id;
    
    [Required]
    [StringLength(15)]
    [RegularExpression(@"^[a-zA-Z0-9_\. ]+$")]
    public required string Name;

    [Required]
    [StringLength(40)]
    public required string Email;

    [Required]
    [StringLength(150)]
    public required string PasswordHash;

    [Required]
    public required DateTime CreatedDate;

    [Required]
    public required DateTime LastLoginDate;
}
