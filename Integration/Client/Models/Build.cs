using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Client
{
    public class Build
    {
        public long Id { get; set; }
        public BuildPiece FirstPiece { get; set; } = null!;

        public Dictionary<long, BuildPiece> BuildPieces { get; } = [];
        public Dictionary<long, Piece> PieceTypes { get; } = [];
        public Dictionary<long, BuildPieceSocket> BuildPieceSockets { get; } = [];
        public Dictionary<long, PieceSocket> SocketTypes { get; } = [];

        public Build() { }

        public Build(BuildPiece firstPiece, IList<BuildPiece>? buildPieces = null)
        {

        }

        public static Build FromDto(Dtos.Build.Response dto)
        {
            Build res = new();
            res.BuildPieces.Add(dto.Id, BuildPiece.FromDto(dto.FirstPiece, res));

            foreach (Dtos.BuildPiece.Response pieceDto in dto.Pieces)
            {
                res.BuildPieces.Add(pieceDto.Id, BuildPiece.FromDto(pieceDto, res));
            }

            return res;
        }
    }
}
