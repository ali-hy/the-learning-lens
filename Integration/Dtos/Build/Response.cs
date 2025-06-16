
using System.Collections.Generic;

namespace Integration.Dtos.Build
{
    public class Response
    {
        public long Id { get; set; }
        public BuildPiece.Response FirstPiece { get; set; } = null!;
        public IList<BuildPiece.Response> Pieces { get; set; } = null!;
    }
}
