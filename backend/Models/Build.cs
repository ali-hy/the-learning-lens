namespace the_learning_lens.Models
{
    public class Build
    {
        long Id { get; set; }
        Piece FirstPiece { get; set; } = null!;
    }
}
