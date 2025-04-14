namespace WebApp.Models
{
    public class BuildPieceLink
    {
        public long Id { get; set; }

        public long OutPieceId { get; set; }
        public BuildPiece OutPiece { get; set; } = null!;
        public string OutLinkName { get; set; } = string.Empty;

        public long InPieceId { get; set; }
        public BuildPiece InPiece { get; set; } = null!;
        public string InLinkName { get; set; } = string.Empty;
    }
}
