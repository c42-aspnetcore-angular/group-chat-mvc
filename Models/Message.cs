    using System;

    namespace GroupChat.Models
    {
        public class Message
        {
            public int ID { get; set; }
            public string AddedBy { get; set;  }
            public string Text { get; set;  }
            public int GroupId { get; set;  }
        }
    }