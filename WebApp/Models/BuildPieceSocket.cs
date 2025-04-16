namespace WebApp.Models
{
    public class BuildPieceSocket
    {
        public long Id { get; set; }

        public long SocketTypeId { get; set; }
        public PieceSocket SocketType { get; set; } = null!;

        public long OnBuildPieceId { get; set; }
        public BuildPiece OnBuildPiece { get; set; } = null!;

        public long HoldingBuildPieceId { get; set; }
        public BuildPiece HoldingBuildPiece { get; set; } = null!;
    }
}
