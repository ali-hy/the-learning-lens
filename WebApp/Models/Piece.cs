namespace WebApp.Models
{
    public class Piece
    {
        public long Id { get; set; }

        public long PrefabId { get; set; }
        public Prefab Prefab { get; set; } = null!;

        public IList<PieceLink> InLinks { get; set; } = null!;
        public IList<PieceLink> OutLinks { get; set; } = null!;
    }
}
