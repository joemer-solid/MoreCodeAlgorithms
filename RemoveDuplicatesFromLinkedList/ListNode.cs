namespace RemoveDuplicatesFromLinkedList
{
    public abstract class ListNode
    {
        public ListNode(int nodeValue)
        {
            NodeValue = nodeValue;
        }

        protected void SetNextNode(ListNode listNode)
            => NextNode = listNode;

        public int NodeValue { get; private set; }

        public ListNode NextNode { get; private set; }

        public abstract string Print();

        protected string GetNodeValueOrDefault(ListNode listNode) =>
            listNode != null ? listNode.NodeValue.ToString() : "undefined";
    }
}
