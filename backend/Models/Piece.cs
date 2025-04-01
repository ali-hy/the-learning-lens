namespace the_learning_lens.Models
{
    public class Piece
    {
        public long Id { get; set; }

        public long TaskId { get; set; }

        public long PrefabId { get; set; }
        public Prefab Prefab { get; set; } = null!;

        public IList<PieceLink> Links { get; set; } = null!;
    }
}
