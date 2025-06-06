using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Client
{
    public class BuildPiece(long id, Piece pieceType, Dictionary<long, BuildPieceSocket> buildPieces)
    {
        public long Id { get; set; } = id;
        public Piece PieceType { get; set; } = pieceType;
        public Dictionary<long, BuildPieceSocket> Sockets { get; set; } = buildPieces;

        public BuildPiece(long id, Piece pieceType) : this(id, pieceType, []) { }

        public static BuildPiece FromDto(Dtos.BuildPiece.Response dto, Build build)
        {
            // Get or make piece type
            build.PieceTypes.TryGetValue(dto.PieceType.Id, out Piece? pieceType);
            pieceType ??= Piece.FromDto(dto.PieceType, build);

            // Create and define
            BuildPiece res = new(dto.Id, pieceType);
            build.BuildPieces.Add(res.Id, res);

            // Make sockets
            foreach (Dtos.BuildPieceSocket.Response buildPieceSocketDto in dto.Sockets)
            {
                var buildPieceSocket = BuildPieceSocket.FromDto(buildPieceSocketDto, res, build);
                res.Sockets.Add(buildPieceSocket.Id, buildPieceSocket);
                build.BuildPieceSockets.Add(buildPieceSocket.Id, buildPieceSocket);
            }

            return res;
        }
    }
}
