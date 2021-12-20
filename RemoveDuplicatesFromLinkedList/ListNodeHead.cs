using System.Collections.Generic;
using System.Linq;

namespace RemoveDuplicatesFromLinkedList
{
    public interface IChildNodeVisitor
    {
        void AcceptSetNextNode(ListNodeChild listNodeChild);

        void AcceptParentNodeReset(ListNode listNode);
    }

    public sealed class ListNodeHead : ListNode
    {
        private readonly HashSet<ListNodeChild> _childNodes = new HashSet<ListNodeChild>();

        public ListNodeHead(int nodeValue) : base(nodeValue) { }

        public HashSet<ListNodeChild> ChildNodes { get { return _childNodes; } }

        public void AppendChild(int childNodeValue)
        {
            if (_childNodes.Count == 0)
            {
                ListNodeChild listNodeChild = new ListNodeChild(childNodeValue, this);

                AddChildNode(listNodeChild);

                SetNextNode(listNodeChild);
            }
            else if (_childNodes.Count > 0)
            {
                var lastAppendedChild = ChildNodes
                    .Where(childNode => childNode.NextNode == null)
                    .FirstOrDefault();

                ListNodeChild newChild = new ListNodeChild(childNodeValue, lastAppendedChild);

                ((IChildNodeVisitor)lastAppendedChild).AcceptSetNextNode(newChild);

                AddChildNode(newChild);
            }
        }

        public IEnumerable<ListNodeChild> GetChildrenWithValue(int nodeValue)
            => EmptyIfNull(ChildNodes.Where(listNodeChild => listNodeChild.NodeValue == nodeValue));

        public void RemoveDuplicateChildNodeWithValue(int nodeValue)
        {
            IEnumerable<ListNodeChild> childrenWithValue = GetChildrenWithValue(nodeValue);
            if (childrenWithValue.Count() > 1)
            {
                IEnumerable<ListNodeChild> duplicatedNodes = childrenWithValue
                    .ToList()
                    .Skip(1);

                duplicatedNodes.ToList()
                    .ForEach(toBeRemoved =>
                    {
                        ChildNodes.Remove(toBeRemoved);
                    });

                ResetChildNodeReferences();
            }
        }

        private static IEnumerable<T> EmptyIfNull<T>(IEnumerable<T> values)
            => values ?? Enumerable.Empty<T>();

        private void AddChildNode(ListNodeChild listNodeChild)
            => ChildNodes.Add(listNodeChild);

        private void ResetChildNodeReferences()
        {
            // get the immediate child node           
            ListNodeChild immediateChild = ChildNodes.SingleOrDefault(childNode => childNode.Parent is ListNodeHead);

            if (immediateChild == null)
            {
                ((IChildNodeVisitor)ChildNodes.ElementAt(0)).AcceptParentNodeReset(this);  
            }           

            foreach (ListNodeChild listNodeChild in ChildNodes)
            {
                ListNodeChild currentChildNextNode = (ListNodeChild)listNodeChild.NextNode;

                if(currentChildNextNode != null 
                    && listNodeChild.NodeValue != currentChildNextNode.Parent.NodeValue)
                {
                   ((IChildNodeVisitor)(ListNodeChild)listNodeChild).AcceptParentNodeReset(listNodeChild);
                }
            }

            // reset the next node value of the new last node
            ((IChildNodeVisitor)ChildNodes.ElementAt(ChildNodes.Count() - 1)).AcceptSetNextNode(null);
        }

        public override string Print()
            => $"AggregateHead: NodeValue: {NodeValue} NextNodeValue: {GetNodeValueOrDefault(NextNode)}";
    }
}
