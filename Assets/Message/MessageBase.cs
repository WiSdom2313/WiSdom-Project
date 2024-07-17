public abstract class MessageBase
{
    public int Priority { get; set; } = 0; // Độ ưu tiên của tin nhắn (mặc định là 0)

    // Constructor
    public MessageBase() { }
}
public class Message : MessageBase
{
    public Message() : base() { }
}
