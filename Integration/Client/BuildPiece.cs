using System.Collections.Generic;

namespace Integration.Client
{
    public class BuildPiece
    {
        public long Id { get; set; }
        public Piece PieceType { get; set; }
        public Dictionary<long, BuildPieceSocket> Sockets { get; set; }

        public BuildPiece(long id, Piece pieceType, Dictionary<long, BuildPieceSocket> buildPieceSockets)
        {
            Id = id;
            PieceType = pieceType;
            Sockets = buildPieceSockets;
        }

        public BuildPiece(long id, Piece pieceType) : this(id, pieceType, new Dictionary<long, BuildPieceSocket>()) { }

        public static BuildPiece FromDto(Dtos.BuildPiece.Response dto, Build build)
        {
            // Get or make piece type
            build.PieceTypes.TryGetValue(dto.PieceType.Id, out Piece? pieceType);
            pieceType ??= Piece.FromDto(dto.PieceType, build);

            // Create and define
            BuildPiece res = new BuildPiece(dto.Id, pieceType);
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
