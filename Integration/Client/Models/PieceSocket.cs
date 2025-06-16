using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Client
{
    public class PieceSocket(long id, string name)
    {
        public long Id { get; set; } = id;
        public string Name { get; set; } = name;

        public static PieceSocket FromDto(Dtos.PieceSocket.Response dto)
        {
            return new(dto.Id, dto.Name);
        }
    }
}
