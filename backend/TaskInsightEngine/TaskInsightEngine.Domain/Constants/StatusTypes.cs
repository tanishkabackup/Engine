using TaskInsightEngine.Domain.Entities;

namespace TaskInsightEngine.Domain.Enums
{
    public static class StatusTypes
    {
        public const string InProgress = "InProgress";
        public const string OnHold = "OnHold";
        public const string Completed = "Completed";
        public const string New = "New";
        public const string InReview = "InReview";
        public const string Blocked = "Blocked";

        public static int MapStatus(string status)
        {
            return status switch
            {
                New => 1,
                Completed => 2,
                InProgress => 3,
                OnHold => 4,
                InReview => 5,
                Blocked => 6,
                _=>0
            };
               
        }

        public static string MapStatusTypes (int id )
        {
            return id switch
            {
               1=> New,
               2=> Completed,
               3=> InProgress,
               4=> OnHold,
               5=> InReview,
               6=> Blocked,
                _=>"none"
            };
        }

    }
}
