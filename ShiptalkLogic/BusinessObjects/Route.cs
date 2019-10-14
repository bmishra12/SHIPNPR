namespace ShiptalkLogic.BusinessObjects
{
    public sealed class Route
    {
        public bool AdminRequired { get; set; }
        public string RouteName { get; set; }
        public short ScopeId { get; set; }
        public int UserId { get; set; }
    }
}