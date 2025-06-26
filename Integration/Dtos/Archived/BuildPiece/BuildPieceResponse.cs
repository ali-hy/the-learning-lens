using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Dtos.Archived.BuildPiece
{
    public class BuildPieceResponse
    {
        public long Id { get; set; }
        public Piece.PieceResponse PieceType { get; set; } = null!;
        public IList<BuildPieceSocket.BuildPieceSocketResponse> Sockets { get; set; } = null!;
    }
}
