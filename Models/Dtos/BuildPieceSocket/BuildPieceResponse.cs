using Models.Dtos.PieceSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.BuildPieceSocket
{
    public class BuildPieceSocketResponse
    {
        public long Id { get; set; }
        public PieceSocketResponse PieceSocketResponse { get; set; } = null!;
    }
}
