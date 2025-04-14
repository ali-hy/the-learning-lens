using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class PieceLink
    {
        public long Id { get; set; }

        public long? InPieceId { get; set; }
        public Piece? InPiece { get; set; }

        public long? OutPieceId { get; set; }
        public Piece? OutPiece { get; set; }

        public string Name { get; set; } = string.Empty;

        // Optional helper property (not mapped)
        [NotMapped]
        public bool IsInLink => InPieceId.HasValue;
    }
}
