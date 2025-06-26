using System.Collections.Generic;

namespace Integration.Client
{
    public class Build
    {
        public long Id { get; set; }
        public BuildPiece FirstPiece { get; set; } = null!;

        public Dictionary<long, BuildPiece> BuildPieces { get; } = new Dictionary<long, BuildPiece>();
        public Dictionary<long, Piece> PieceTypes { get; } = new Dictionary<long, Piece>();
        public Dictionary<long, BuildPieceSocket> BuildPieceSockets { get; } = new Dictionary<long, BuildPieceSocket>();
        public Dictionary<long, PieceSocket> SocketTypes { get; } = new Dictionary<long, PieceSocket>();

        public Build() { }

        public Build(BuildPiece firstPiece, IList<BuildPiece>? buildPieces = null)
        {

        }

        public static Build FromDto(Dtos.Archived.Build.Response dto)
        {
            Build res = new Build();
            res.BuildPieces.Add(dto.Id, BuildPiece.FromDto(dto.FirstPiece, res));

            foreach (Dtos.Archived.BuildPiece.BuildPieceResponse pieceDto in dto.Pieces)
            {
                res.BuildPieces.Add(pieceDto.Id, BuildPiece.FromDto(pieceDto, res));
            }

            return res;
        }
    }
}
