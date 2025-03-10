namespace ClownChat.Api.Data;

public class GroupMember
{
    public required int UserId;
    public required int GroupId;
    public required string NickName;
    public required DateTime JoinDate;
}