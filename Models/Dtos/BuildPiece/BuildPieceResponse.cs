using Models.Dtos.BuildPieceSocket;
using Models.Dtos.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.BuildPiece
{
    public class BuildPieceResponse
    {
        public long Id { get; set; }
        public PieceResponse Piece { get; set; } = null!;
        public IList<BuildPieceSocketResponse> Sockets { get; set; } = null!;
    }
}
