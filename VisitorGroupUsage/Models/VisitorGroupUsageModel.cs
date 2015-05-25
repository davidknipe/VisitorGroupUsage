namespace VisitorGroupUsage.Models
{
    public class VisitorGroupUsageModel
    {
        public VisitorGroupUsageModel()
        {
            UsedOnThisContent = new VisitorGroupSet();
            UsedOnReferencedContent = new VisitorGroupSet();
        }

        public VisitorGroupSet UsedOnThisContent;
        public VisitorGroupSet UsedOnReferencedContent;
    }
}
