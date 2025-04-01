namespace the_learning_lens.Models
{
    public class BuildPiece
    {
        public long Id { get; set; }

        public long PieceId { get; set; }
        public Piece Piece { get; set; } = null!;

        public BuildPieceLink
    }
}
