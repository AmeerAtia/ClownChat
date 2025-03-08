using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

namespace ClownChat.Api.Data;

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
