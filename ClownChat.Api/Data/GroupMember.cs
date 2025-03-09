using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ClownChat.Api.Data;

public record GroupMember
{
    public required int UserId;
    public required int ChatId;
    public required DateTime JoinDate;
}