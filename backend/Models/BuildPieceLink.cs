namespace the_learning_lens.Models
{
    public class BuildPieceLink
    {
        public long Id { get; set; }

        public long PieceId1 { get; set; }
        public Piece Piece1 { get; set; } = null!;
        public string LinkName1 { get; set; } = string.Empty;

        public long PieceId2 { get; set; }
        public Piece Piece2 { get; set; } = null!;
        public string LinkName2 { get; set; } = string.Empty;
    }
}
