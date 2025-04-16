namespace WebApp.Models
{
    public class Build
    {
        public long Id { get; set; }

        public long FirstPieceId { get; set; }
        public BuildPiece FirstPiece { get; set; } = null!;

        public IList<BuildPiece> Pieces { get; set; } = null!;
    }
}
