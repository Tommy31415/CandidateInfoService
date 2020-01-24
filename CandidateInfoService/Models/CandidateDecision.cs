namespace CandidateInfoService.Models
{
    public class CandidateDecision
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsGoingToHaveOffer { get; set; }
    }
}
