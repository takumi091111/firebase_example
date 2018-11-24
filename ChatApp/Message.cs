namespace ChatApp
{
    public class Message
    {
        public string Content { get; set; }

        public Message(string text)
        {
            Content = text;
        }
    }
}
