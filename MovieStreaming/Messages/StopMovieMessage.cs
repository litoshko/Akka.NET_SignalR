namespace MovieStreaming.Messages
{
    class StopMovieMessage
    {
        private int userId;

        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; private set; }
    }
}
