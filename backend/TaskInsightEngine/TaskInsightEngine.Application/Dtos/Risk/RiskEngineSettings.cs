namespace TaskInsightEngine.Application.Dtos.Risk
{

    public class RiskEngineSettings
    {
        public Thresholds Thresholds { get; set; }
        public Weights Weights { get; set; }
        public MovementRules MovementRules { get; set; }

        public const string Section = "RiskEngine";
    }

    public class Thresholds
    {
        public int Critical { get; set; }
        public int Attention { get; set; }
        public int Monitor { get; set; }
    }

    public class Weights
    {
        public int OverDue { get; set; }
        public int BlockerPresent { get; set; }
        public int BlockerNoActivity { get; set; }
    }

    public class MovementRules
    {
        public int EscalationLimit { get; set; }
        public int ImprovementLimit { get; set; }
        public int StabilityTolerance { get; set; }
    }
}

