using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Dtos.BuildPiece
{
    public class Response
    {
        public long Id { get; set; }
        public Piece.Response PieceType { get; set; } = null!;
        public IList<BuildPieceSocket.Response> Sockets { get; set; } = null!;
    }
}
