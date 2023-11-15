using BusinessLogic.Controllers;

namespace Frontend.Data
{
    public class FeedController
    {
        public FeedManager Feed { get; set; }
        public FeedController(FeedManager feed) {
            Feed = feed;
        } 
    }
}
