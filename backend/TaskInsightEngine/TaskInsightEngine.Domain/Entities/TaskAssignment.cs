namespace TaskInsightEngine.Domain.Entities
{
    public class TaskAssignment
    {
        public int TaskAssignmentId { get; set; }
        public int TaskItemId { get; set; }

        public int AssigneeId { get; set; }

        public int AssignerId { get; set; }

        public int ManagerId { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public TaskItem TaskItem { get; set; }

        public ProjectMember AssigneeMember { get; set; }
        public ProjectMember AssignerMember { get; set; }
        public ProjectMember Manager { get; set; }

       public ICollection<TaskImpediment> TaskImpediments { get; set; }
    }
}
