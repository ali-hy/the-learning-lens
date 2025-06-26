using System.Collections.Generic;

namespace Integration.Dtos.Archived.Build
{
    public class Response
    {
        public long Id { get; set; }
        public BuildPiece.BuildPieceResponse FirstPiece { get; set; } = null!;
        public IList<BuildPiece.BuildPieceResponse> Pieces { get; set; } = null!;
    }
}
