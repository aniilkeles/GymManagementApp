namespace GymManagementApp.Models
{
    internal class MemberWorkoutAssignment
    {
        public int AssignmentId { get; init; }

        public int MemberId { get; init; }

        public int WorkoutPlanId { get; init; }

        public DateTime AssignedDate { get; init; }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public MemberWorkoutAssignment(int assignmentId, int memberId, int planId)
        {
            AssignmentId = assignmentId;
            MemberId = memberId;
            WorkoutPlanId = planId;
            AssignedDate = DateTime.Now;
            isActive = true;
        }
    }
}
