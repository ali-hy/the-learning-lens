using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public static Piece FromDto(Dtos.Piece.Response dto, Build build)
        {
            var res = new Piece(dto.Id, dto.Name);

            foreach (Dtos.PieceSocket.Response socketTypeDto in dto.Sockets)
            {
                build.SocketTypes.TryGetValue(socketTypeDto.Id, out PieceSocket? socketType);
                socketType ??= PieceSocket.FromDto(socketTypeDto);
                res.Sockets.Add(socketType);
            }

            return res;
        }
    }
}
