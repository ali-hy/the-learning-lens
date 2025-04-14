namespace WebApp.Models
{
    public class BuildPiece
    {
        public long Id { get; set; }

        public long PieceId { get; set; }
        public Piece Piece { get; set; } = null!;
        
        public long BuildId { get; set; }
        public Build Build { get; set; } = null!;

        public List<BuildPieceLink> OutLinks { get; set; } = null!;
        public List<BuildPieceLink> InLinks { get; set; } = null!;
    }
}
