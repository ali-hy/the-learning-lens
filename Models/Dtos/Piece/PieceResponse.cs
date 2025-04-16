using Models.Dtos.PieceSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.Piece
{
    public class PieceResponse
    {
        public long Id { get; set; }
        public IList<PieceSocketResponse> Sockets { get; set; } = null!;
        // TODO: add field for identifying / fetching the piece on the client
    }
}
