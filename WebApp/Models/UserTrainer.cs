namespace WebApp.Models
{
    public class UserTrainer
    {
        public long TrainerId { get; set; }
        public long TraineeId { get; set; }

        public UserAccount Trainee { get; set; } = null!;
        public UserAccount Trainer { get; set; } = null!;
    }
}
