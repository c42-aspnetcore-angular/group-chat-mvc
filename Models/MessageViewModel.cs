namespace GroupChat.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string AddedBy { get; set; }
        public string Text { get; set; }
        public int GroupId { get; set; }
        public int SocketId { get; set; }
    }
}