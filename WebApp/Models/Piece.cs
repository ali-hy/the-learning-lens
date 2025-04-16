namespace WebApp.Models
{
    public class Piece
    {
        public long Id { get; set; }

        public long PrefabId { get; set; }
        public Prefab Prefab { get; set; } = null!;

        public IList<PieceSocket> Sockets { get; set; } = null!;
    }
}
