using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Dtos.Archived.BuildPieceSocket
{
    public class BuildPieceSocketResponse
    {
        public long Id { get; set; }
        public PieceSocket.PieceSocketResponse SocketType { get; set; } = null!;
        public BuildPiece.BuildPieceResponse HoldingBuildPiece { get; set; } = null!;
    }
}
