namespace TreeAndJournal.Application.Tree.GetTree
{
    public class NodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NodeDto> Children { get; set; } = new List<NodeDto>();
    }
}
