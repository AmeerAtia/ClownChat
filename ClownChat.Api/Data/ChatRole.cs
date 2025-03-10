namespace ClownChat.Api.Data;

public class ChatRole
{
    [Required]
    public required string Name;
    [Required]
    public required bool SendMessages;
}

enum ReadMessagesPermission
{
    AllMessages,
    LimitedByJoinDate,
    Didabled
}

enum SendMessagesPermission
{
    SendFreely,
    SlowMode,
    Didabled
}

enum CorrectMessagesPermission
{
    Always,
    Once,
    Disabled
}

enum DeleteMessagesPermission
{
    AnyMessage,
    HisMessages,
    Disabled
}