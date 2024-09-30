using System;
using System.Collections.Generic;

public class BinTreeNode<T>
{
    public T Value { get; set; }
    public BinTreeNode<T> Left { get; set; }
    public BinTreeNode<T> Right { get; set; }

    public BinTreeNode(T value, BinTreeNode<T> left = null, BinTreeNode<T> right = null)
    {
        Value = value;
        Left = left;
        Right = right;
    }
    public static void Print<T>(BinTreeNode<T> p, int level = 0)
    {
        if (p == null) return;
        Print(p.Right, level + 1);
        Console.WriteLine("".PadLeft(level, '.') + p.Value);
        Print(p.Left, level + 1);
    }
    public static void TraversePreOrder<T>(BinTreeNode<T> tree, Action<T> action)
    {
        if (tree == null) return;
        action(tree.Value);
        TraversePreOrder(tree.Left, action);
        TraversePreOrder(tree.Right, action);
    }


    //1
    public static BinTreeNode<char> CreateTreeOfChars()
    {
        BinTreeNode<char> root = new BinTreeNode<char>('A');
        root.Left = new BinTreeNode<char>('B');
        root.Left.Left = new BinTreeNode<char>('D');
        root.Left.Right = new BinTreeNode<char>('E');

        root.Right = new BinTreeNode<char>('C');
        root.Right.Right = new BinTreeNode<char>('G');
        root.Right.Left = new BinTreeNode<char>('F');
        root.Right.Left.Left = new BinTreeNode<char>('H');
        root.Right.Left.Right = new BinTreeNode<char>('I');
        
        return root;
    }

    //2
    public static void PrintTree<T>(BinTreeNode<T> p, int level = 0)
    {
        if (p == null) return;
        Console.WriteLine("".PadLeft(level, '.') + p.Value);
        PrintTree(p.Left, level + 1);
        PrintTree(p.Right, level + 1);
    }

    //3
    public static int NoOfNodes<T>(BinTreeNode<T> tree)
    {
        int counter = 0;
        if(tree == null)
            return 0;
        counter++;

        counter += NoOfNodes(tree.Left);
        counter += NoOfNodes(tree.Right);

        return counter;
    }

    //4
    public static int Depth<T>(BinTreeNode<T> tree)
    {
        int counter = 0;
        if(tree == null)
            return 0;
        counter++;

        counter += Math.Max(Depth(tree.Left), Depth(tree.Right));
        return counter;
    }

    //5
    public static void DoMirrorOfTree<T>(BinTreeNode<T> tree)
    {
        if (tree == null) return;
        DoMirrorOfTree(tree.Left);
        DoMirrorOfTree(tree.Right);
        BinTreeNode<T> temp = tree.Left;
        tree.Left = tree.Right;
        tree.Right = temp;
    }

    //6
    public static List<T> GetTraversePreorder<T>(BinTreeNode<T> tree)
    {
        List<T> list = new List<T>();
        TraversePreOrder(tree, value => list.Add(value));
        return list;
    }

    //7
    public static IEnumerable<T> InOrder<T>(BinTreeNode<T> head)
    {
        if (head != null)
        {
            foreach(T value in InOrder(head.Left))
                yield return value;

            yield return head.Value;

            foreach(T value in InOrder(head.Right))
                yield return value;
        }
    }

    //8
    public static void RemoveNode<T>(BinTreeNode<T> root, T value)
    {
        if (root == null) return;
        if (BinTreeNode<T>.FindNodeWithParent(root, value, out BinTreeNode<T> foundNode, out BinTreeNode<T> parentNode))
        {
            if (foundNode.Left == null && foundNode.Right == null)
            {
                if (BinTreeNode<T>.CompareNodesWithValue(root, value)) //Delete root with no children
                {
                    root = null;
                    return;
                }
                else
                {
                    //Console.WriteLine($"P:{parentNode.Value} L:{parentNode.Left.Value} R:{parentNode.Right.Value} V:{value}"); //////////////////////////
                    if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Left, value)) //Leaf left
                    {
                        parentNode.Left = null;
                        return;
                    }
                    if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Right, value)) //Leaf right
                    {
                        parentNode.Right = null;
                        return;
                    }
                }
            }
            else if (foundNode.Left == null && foundNode.Right != null) //One child right
            {
                if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Left, value))
                    parentNode.Left = foundNode.Right;
                if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Right, value))
                    parentNode.Right = foundNode.Right;

                return;
            }
            else if (foundNode.Left != null && foundNode.Right == null) //One child left
            {
                if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Left, value))
                    parentNode.Left = foundNode.Left;
                if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Right, value))
                    parentNode.Right = foundNode.Left;

                return;
            }
            else if (foundNode.Left != null && foundNode.Right != null) //Two children
            {
                BinTreeNode<T>.FindDeepestRightmostNodeWithParent(root, out BinTreeNode<T> deepFoundNode, out BinTreeNode<T> deepParentNode);
                if (BinTreeNode<T>.CompareNodesWithValue(deepParentNode.Left, deepFoundNode.Value))
                    deepParentNode.Left = null;
                if (BinTreeNode<T>.CompareNodesWithValue(deepParentNode.Right, deepFoundNode.Value))
                    deepParentNode.Right = null;

                if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Left, value))
                {
                    deepFoundNode.Left = foundNode.Left;
                    deepFoundNode.Right = foundNode.Right;
                    parentNode.Left = deepFoundNode;
                }
                if (BinTreeNode<T>.CompareNodesWithValue(parentNode.Left, value))
                {
                    deepFoundNode.Left = foundNode.Left;
                    deepParentNode.Right = foundNode.Right;
                    parentNode.Right = deepFoundNode;
                }
            }

        }
        else
            return; //Node not found, do nothing
    }
    public static bool CompareNodesWithValue<T>(BinTreeNode<T> node, T value)
    {
        return EqualityComparer<T>.Default.Equals(node.Value, value);
    }

    public static bool FindNodeWithParent<T>(BinTreeNode<T> root, T value, out BinTreeNode<T> foundNode, out BinTreeNode<T> parent)
    {
        parent = null;
        foundNode = null;

        if (root == null)
            return false;

        return FindNodeWithValueAndParentHelper(root, null, value, ref parent, ref foundNode);
    }

    private static bool FindNodeWithValueAndParentHelper<T>(BinTreeNode<T> node, BinTreeNode<T> parentNode, T value, ref BinTreeNode<T> parent, ref BinTreeNode<T> foundNode)
    {
        if (node == null)
            return false;

        if (node.Value.Equals(value))
        {
            parent = parentNode;
            foundNode = node;
            return true;
        }

        if (FindNodeWithValueAndParentHelper(node.Left, node, value, ref parent, ref foundNode))
            return true;

        return FindNodeWithValueAndParentHelper(node.Right, node, value, ref parent, ref foundNode);
    }
    public static void FindDeepestRightmostNodeWithParent<T>(BinTreeNode<T> root, out BinTreeNode<T> deepFoundNode, out BinTreeNode<T> deepParentNode)
    {
        deepParentNode = null;
        deepFoundNode = null;

        if (root == null)
            return;

        var result = FindDeepestRightmostNodeWithParentHelper(root, null, 0, new Result<BinTreeNode<T>>());
        deepParentNode = result.Parent;
        deepFoundNode = result.Node;
    }

    private class Result<TNode>
    {
        public TNode Node { get; set; }
        public TNode Parent { get; set; }
        public int Depth { get; set; }
    }

    private static Result<BinTreeNode<T>> FindDeepestRightmostNodeWithParentHelper<T>(BinTreeNode<T> node, BinTreeNode<T> parent, int depth, Result<BinTreeNode<T>> currentMax)
    {
        if (node == null)
            return currentMax;

        if (depth >= currentMax.Depth && IsRightmost(node, depth, currentMax))
        {
            currentMax.Node = node;
            currentMax.Parent = parent;
            currentMax.Depth = depth;
        }

        currentMax = FindDeepestRightmostNodeWithParentHelper(node.Right, node, depth + 1, currentMax);
        currentMax = FindDeepestRightmostNodeWithParentHelper(node.Left, node, depth + 1, currentMax);

        return currentMax;
    }

    private static bool IsRightmost<T>(BinTreeNode<T> node, int depth, Result<BinTreeNode<T>> currentMax)
    {
        return depth >= currentMax.Depth && (depth > currentMax.Depth || node == currentMax.Node);
    }
}
class Program
{
    static void Main()
    {
        var t = new BinTreeNode<string>("A",
            new BinTreeNode<string>("B",
                new BinTreeNode<string>("D"),
                new BinTreeNode<string>("E")),
            new BinTreeNode<string>("C", 
                null,
                new BinTreeNode<string>("F",
                    new BinTreeNode<string>("H"),
                    new BinTreeNode<string>("I"))));
        BinTreeNode<string>.Print(t);
        Console.WriteLine();
        BinTreeNode<string>.RemoveNode(t, "B"); //usunięcie węzła z 2 dziećmi
        BinTreeNode<string>.Print(t);
    }

}