using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.Archive
{
    public class PieceSocket
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Id of the type of piece that this socket is expected to hold
        /// </summary>
        public long PieceId { get; set; }

        /// <summary>
        /// The type of piece that this socket is expected to hold
        /// </summary>
        public Piece Piece { get; set; } = null!;


        /// <summary>
        /// Will hold JSON properties of socket (if necessary)
        /// </summary>
        public string? JsonDescription { get; set; }
    }
}
