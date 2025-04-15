using Models.Dtos.BuildPiece;

namespace Models.Dtos.Build
{
    public class BuildResponse
    {
        public long Id { get; set; }
        public BuildPieceResponse FirstPiece { get; set; } = null!;

        public IList<BuildPieceResponse> Pieces { get; set; } = null!;
    }
}
