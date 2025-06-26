using Integration.Dtos.Archived.Piece;
using Integration.Dtos.Archived.PieceSocket;
using System.Collections.Generic;

namespace Integration.Client
{
    public class Piece
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<PieceSocket> Sockets { get; set; }

        public Piece(long id, string name, IList<PieceSocket> sockets)
        {
            Id = id;
            Name = name;
            Sockets = sockets;
        }

        public Piece(long id, string name) : this(id, name, new List<PieceSocket>()) { }

        public Piece(long id) : this(id, string.Empty) { }

        public static Piece FromDto(Dtos.Archived.Piece.PieceResponse dto, Build build)
        {
            var res = new Piece(dto.Id, dto.Name);

            foreach (PieceSocketResponse socketTypeDto in dto.Sockets)
            {
                build.SocketTypes.TryGetValue(socketTypeDto.Id, out PieceSocket? socketType);
                socketType ??= PieceSocket.FromDto(socketTypeDto);
                res.Sockets.Add(socketType);
            }

            return res;
        }
    }
}
