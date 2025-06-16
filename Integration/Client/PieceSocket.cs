using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Client
{
    public class PieceSocket
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public PieceSocket(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public static PieceSocket FromDto(Dtos.PieceSocket.Response dto)
        {
            return new PieceSocket(dto.Id, dto.Name);
        }
    }
}
