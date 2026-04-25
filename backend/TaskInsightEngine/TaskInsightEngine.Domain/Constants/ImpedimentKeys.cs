namespace TaskInsightEngine.Domain.Constants
{
    public static class ImpedimentKeys
    {
        public static string Group(int Id) => $"imp-{Id}";

        public static string GroupKey(int ImpedimentId) => $"impediment-{ImpedimentId}";

    }
}
