using Integration.Dtos.BuildPiece;
using Integration.Dtos.PieceSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Dtos.BuildPieceSocket
{
    public class Response
    {
        public long Id { get; set; }
        public PieceSocket.Response SocketType { get; set; } = null!;
        public BuildPiece.Response HoldingBuildPiece { get; set; } = null!;
    }
}
