using Integration.Dtos.Archived.PieceSocket;
using System.Collections.Generic;

namespace Integration.Dtos.Archived.Piece
{
    public class PieceResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<PieceSocketResponse> Sockets { get; set; } = null!;
    }
}
