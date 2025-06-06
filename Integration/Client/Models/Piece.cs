using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Client
{
    public class Piece(long id, string name, IList<PieceSocket> sockets)
    {
        public long Id { get; set; } = id;
        public string Name { get; set; } = name;
        public IList<PieceSocket> Sockets { get; set; } = sockets;

        public Piece(long id, string name) : this(id, name, []) { }
        public Piece(long id) : this(id, string.Empty) { }

        public static Piece FromDto(Dtos.Piece.Response dto, Build build)
        {
            Piece res = new(dto.Id, dto.Name);

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
