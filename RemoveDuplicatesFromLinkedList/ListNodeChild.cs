namespace RemoveDuplicatesFromLinkedList
{
    public sealed class ListNodeChild : ListNode, IChildNodeVisitor
    {
        public ListNodeChild(int nodeValue, ListNode parent) : base(nodeValue)
          => Parent = parent;

        public ListNode Parent { get; private set; }

        public override string Print()
           => string.Format(
                "AggregatedChild: NodeValue: {0} ParentNodeValue: {1} NextNodeValue: {2}",
                NodeValue.ToString(),
                GetNodeValueOrDefault(Parent),
                GetNodeValueOrDefault(NextNode));       

        void IChildNodeVisitor.AcceptSetNextNode(ListNodeChild listNodeChild)
            => SetNextNode(listNodeChild);           

        void IChildNodeVisitor.AcceptParentNodeReset(ListNode listNode)
             => Parent = listNode;
    }
}
