namespace Flashcard.Data.Card
{
    public class UserAnswerModel
    {
        public string UserId { get; set; }
        public int BundleId { get; set; }
        public int CardId { get; set; }
        public bool Answered { get; set; }  
    }
}
