using WebApp.Models;

namespace WebApp.Validators
{
    public class PieceLinkValidator
    {
        public static void Validate(PieceLink link)
        {
            bool hasIn = link.InPieceId.HasValue;
            bool hasOut = link.OutPieceId.HasValue;

            if (hasIn == hasOut)
            {
                throw new InvalidOperationException("A PieceLink must have exactly one of InPieceId or OutPieceId set.");
            }
        }
    }
}
