using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class PieceSocket
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public long PieceId { get; set; }
        public Piece Piece { get; set; } = null!;


        /// <summary>
        /// Will hold JSON properties of socket (if necessary)
        /// </summary>
        public string? JsonDescription { get; set; }
    }
}
