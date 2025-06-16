using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Client
{
    public class BuildPieceSocket
    {
        public long Id { get; set; }
        public PieceSocket SocketType { get; set; }
        public BuildPiece OnBuildPiece { get; set; }
        public BuildPiece? HoldingBuildPiece { get; set; }

        public BuildPieceSocket(long id, PieceSocket socketType, BuildPiece isOn, BuildPiece? isHolding)
        {
            Id = id;
            SocketType = socketType;
            OnBuildPiece = isOn;
            HoldingBuildPiece = isHolding;
        }

        public BuildPieceSocket(long id, PieceSocket socketType, BuildPiece isOn)
            : this(id, socketType, isOn, null)
        {
        }

        public static BuildPieceSocket FromDto(Dtos.BuildPieceSocket.Response dto, BuildPiece buildPiece, Build build)
        {
            // Get or make SocketType
            build.SocketTypes.TryGetValue(dto.Id, out PieceSocket? socketType);
            socketType ??= PieceSocket.FromDto(dto.SocketType);

            var res = new BuildPieceSocket(
                dto.Id,
                socketType,
                isOn: buildPiece
            );

            build.BuildPieceSockets.Add(res.Id, res);

            // Get or make isHolding
            build.BuildPieces.TryGetValue(dto.HoldingBuildPiece.Id, out BuildPiece? isHolding);
            isHolding ??= BuildPiece.FromDto(dto.HoldingBuildPiece, build);

            return res;
        }
    }
}
