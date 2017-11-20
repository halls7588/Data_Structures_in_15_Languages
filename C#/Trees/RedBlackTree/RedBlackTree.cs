/*******************************************************
 *  RedBlackTree.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Rec Black Tree implementation in C#
 ********************************************************/
using System;
using System.Collections.Generic;

namespace DataStructures.Trees.RedBlackTree
{
    /// <summary>
    /// RedBlackTree class
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class RedBlackTree<T> where T : IComparable
    {
        /// <summary>
        /// Node colors
        /// </summary>
        private static int BLACK = 0;
        private static int RED = 1;

        /// <summary>
        /// Node Class for redblacktree
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Public properties of the node class
            /// </summary>
            public T Key { set; get; }
            public Node Parent { set; get; }
            public Node Left { set; get; }
            public Node Right { set; get; }
            public int NumLeft { set; get; }
            public int NumRight { set; get; }
            public int Color { set; get; }

            /// <summary>
            /// Node Constructor
            /// </summary>
            public Node()
            {
                Color = BLACK;
                NumLeft = 0;
                NumRight = 0;
                Parent = null;
                Left = null;
                Right = null;
            }

            /// <summary>
            /// Constructor which sets key to the argument.
            /// </summary>
            /// <param name="key">key to set</param>
            public Node(T key)
            {
                Color = BLACK;
                NumLeft = 0;
                NumRight = 0;
                Parent = null;
                Left = null;
                Right = null;
                Key = key;
            }
        }

        /// <summary>
        /// private member of the redblack tree.
        /// </summary>
        private readonly Node _nil;
        private Node _root;

        /// <summary>
        /// RedBalckTree Constructor
        /// </summary>
        public RedBlackTree()
        {
            _nil = new Node();
            _root = _nil;
            _root.Left = _nil;
            _root.Right = _nil;
            _root.Parent = _nil;
        }

        /// <summary>
        /// Rotates a node left
        /// </summary>
        /// <param name="x">The node which the lefRotate is to be performed on.</param>
        private void RotateLeft(Node x)
        {
            // Call rotateLeftFixup() which updates the numLeft
            // and numRight values.
            RotateLeftFixup(x);
            // Perform the left rotate as described in the algorithm
            // in the course text.
            Node y = x.Right;
            x.Right = y.Left;
            // Check for existence of y.left and make pointer changes
            if (!IsNil(y.Left))
                y.Left.Parent = x;
            y.Parent = x.Parent;
            // x's parent is null
            if (IsNil(x.Parent))
                _root = y;
            // x is the left child of it's parent
            else if (x.Parent.Left == x)
                x.Parent.Left = y;
            // x is the right child of it's parent.
            else
                x.Parent.Right = y;
            // Finish of the leftRotate
            y.Left = x;
            x.Parent = y;
        }

        /// <summary>
        /// Updates the numLeft & numRight values affected by leftRotate.
        /// </summary>
        /// <param name="x">The node which the leftRotate is to be performed on.</param>
        private void RotateLeftFixup(Node x)
        {
            // Case 1: Only x, x.right and x.right.right always are not nil.
            if (IsNil(x.Left) && IsNil(x.Right.Left))
            {
                x.NumLeft = 0;
                x.NumRight = 0;
                x.Right.NumLeft = 1;
            }
            // Case 2: x.right.left also exists in addition to Case 1
            else if (IsNil(x.Left) && !IsNil(x.Right.Left))
            {
                x.NumLeft = 0;
                x.NumRight = 1 + x.Right.Left.NumLeft + x.Right.Left.NumRight;
                x.Right.NumLeft = 2 + x.Right.Left.NumLeft + x.Right.Left.NumRight;
            }
            // Case 3: x.left also exists in addition to Case 1
            else if (!IsNil(x.Left) && IsNil(x.Right.Left))
            {
                x.NumRight = 0;
                x.Right.NumLeft = 2 + x.Left.NumLeft + x.Left.NumRight;
            }
            // Case 4: x.left and x.right.left both exist in addtion to Case 1
            else
            {
                x.NumRight = 1 + x.Right.Left.NumLeft + x.Right.Left.NumRight;
                x.Right.NumLeft = 3 + x.Left.NumLeft + x.Left.NumRight + x.Right.Left.NumLeft + x.Right.Left.NumRight;
            }
        }

        /// <summary>
        /// Updates the numLeft and numRight values affected by the Rotate.
        /// </summary>
        /// <param name="y"> The node which the rightRotate is to be performed</param>
        private void RotateRight(Node y)
        {
            // Call rightRotateFixup to adjust numRight and numLeft values
            RotateRightFixup(y);
            // Perform the rotate as described in the course text.
            Node x = y.Left;
            y.Left = x.Right;
            // Check for existence of x.right
            if (!IsNil(x.Right))
                x.Right.Parent = y;
            x.Parent = y.Parent;
            // y.parent is nil
            if (IsNil(y.Parent))
                _root = x;
            // y is a right child of it's parent.
            else if (y.Parent.Right == y)
                y.Parent.Right = x;
            // y is a left child of it's parent.
            else
                y.Parent.Left = x;

            x.Right = y;
            y.Parent = x;
        }

        /// <summary>
        /// Updates the numLeft and numRight values affected by the rotate
        /// </summary>
        /// <param name="y">the node around which the righRotate is to be performed.</param>
        private void RotateRightFixup(Node y)
        {
            // Case 1: Only y, y.left and y.left.left exists.
            if (IsNil(y.Right) && IsNil(y.Left.Right))
            {
                y.NumRight = 0;
                y.NumLeft = 0;
                y.Left.NumRight = 1;
            }
            // Case 2: y.left.right also exists in addition to Case 1
            else if (IsNil(y.Right) && !IsNil(y.Left.Right))
            {
                y.NumRight = 0;
                y.NumLeft = 1 + y.Left.Right.NumRight + y.Left.Right.NumLeft;
                y.Left.NumRight = 2 + y.Left.Right.NumRight + y.Left.Right.NumLeft;
            }
            // Case 3: y.right also exists in addition to Case 1
            else if (!IsNil(y.Right) && IsNil(y.Left.Right))
            {
                y.NumLeft = 0;
                y.Left.NumRight = 2 + y.Right.NumRight + y.Right.NumLeft;
            }
            // Case 4: y.right & y.left.right exist in addition to Case 1
            else
            {
                y.NumLeft = 1 + y.Left.Right.NumRight + y.Left.Right.NumLeft;
                y.Left.NumRight = 3 + y.Right.NumRight + y.Right.NumLeft + y.Left.Right.NumRight + y.Left.Right.NumLeft;
            }
        }

        /// <summary>
        /// Inserts new key into the tree
        /// </summary>
        /// <param name="key">key to insert</param>
        public void Insert(T key) => Insert(new Node(key));

        /// <summary>
        /// Inserts z into the appropriate position in the RedBlackTree while updating numLeft and numRight values.
        /// </summary>
        /// <param name="z">the node to be inserted into the Tree rooted at root</param>
        private void Insert(Node z)
        {
            // Create a reference to root & initialize a node to nil
            Node y = _nil;
            Node x = _root;
            // While we haven't reached a the end of the tree keep
            // trying to figure out where z should go
            while (!IsNil(x))
            {
                y = x;
                // if z.key is < than the current key, go left
                if (z.Key.CompareTo(x.Key) < 0)
                {
                    // Update x.numLeft as z is < than x
                    x.NumLeft++;
                    x = x.Left;
                }
                // else z.key >= x.key so go right.
                else
                {
                    // Update x.numGreater as z is => x
                    x.NumRight++;
                    x = x.Right;
                }
            }
            // y will hold z's parent
            z.Parent = y;
            // Depending on the value of y.key, put z as the left or
            // right child of y
            if (IsNil(y))
                _root = z;
            else if (z.Key.CompareTo(y.Key) < 0)
                y.Left = z;
            else
                y.Right = z;
            // Initialize z's children to nil and z's color to red
            z.Left = _nil;
            z.Right = _nil;
            z.Color = RED;
            // Call insertFixup(z)
            InsertFixup(z);
        }

        /// <summary>
        /// Fixes up the violation of the RedBlackTree properties that may have been caused during insert(z)
        /// </summary>
        /// <param name="z">the node which was inserted and may have caused a violation of the RedBlackTree properties</param>
        private void InsertFixup(Node z)
        {
            Node y = _nil;
            // While there is a violation of the RedBlackTree properties..
            while (z.Parent.Color == RED)
            {
                // If z's parent is the the left child of it's parent.
                if (z.Parent == z.Parent.Parent.Left)
                {
                    // Initialize y to z 's cousin
                    y = z.Parent.Parent.Right;
                    // Case 1: if y is red...recolors
                    if (y.Color == RED)
                    {
                        z.Parent.Color = BLACK;
                        y.Color = BLACK;
                        z.Parent.Parent.Color = RED;
                        z = z.Parent.Parent;
                    }
                    // Case 2: if y is black & z is a right child
                    else if (z == z.Parent.Right)
                    {
                        // leftRotaet around z's parent
                        z = z.Parent;
                        RotateLeft(z);
                    }
                    // Case 3: else y is black & z is a left child
                    else
                    {
                        // Recolors and rotate round z's grandpa
                        z.Parent.Color = BLACK;
                        z.Parent.Parent.Color = RED;
                        RotateRight(z.Parent.Parent);
                    }
                }
                // If z's parent is the right child of it's parent.
                else
                {
                    // Initialize y to z's cousin
                    y = z.Parent.Parent.Left;
                    // Case 1: if y is red...recolors
                    if (y.Color == RED)
                    {
                        z.Parent.Color = BLACK;
                        y.Color = BLACK;
                        z.Parent.Parent.Color = RED;
                        z = z.Parent.Parent;
                    }
                    // Case 2: if y is black and z is a left child
                    else if (z == z.Parent.Left)
                    {
                        // rightRotate around z's parent
                        z = z.Parent;
                        RotateRight(z);
                    }
                    // Case 3: if y  is black and z is a right child
                    else
                    {
                        // Recolors and rotate around z's grandpa
                        z.Parent.Color = BLACK;
                        z.Parent.Parent.Color = RED;
                        RotateLeft(z.Parent.Parent);
                    }
                }
            }
            // Color root black at all times
            _root.Color = BLACK;
        }

        /// <summary>
        /// gets the smallest node in the tree
        /// </summary>
        /// <param name="node">Start node to find smallest of</param>
        /// <returns>the node with the smallest key rooted at node</returns>
        public Node TreeMinimum(Node node)
        {
            // while there is a smaller key, keep going left
            while (!IsNil(node.Left))
                node = node.Left;
            return node;
        }

        /// <summary>
        /// Returns the next largest key from the given node
        /// </summary>
        /// <param name="x">Node whose successor we must find</param>
        /// <returns>node the with the next largest key from x.key</returns>
        public Node TreeSuccessor(Node x)
        {
            // if x.left is not nil, call treeMinimum(x.right) and
            // return it's value
            if (!IsNil(x.Left))
                return TreeMinimum(x.Right);
            Node y = x.Parent;
            // while x is it's parent's right child...
            while (!IsNil(y) && x == y.Right)
            {
                // Keep moving up in the tree
                x = y;
                y = y.Parent;
            }
            // Return successor
            return y;
        }

        /// <summary>
        /// Removes a Node from the tree
        /// </summary>
        /// <param name="node">Node which is to be removed from the the tree</param>
        public void Remove(Node node)
        {
            Node z = Search(node.Key);
            Node x = _nil;
            Node y = _nil;
            // if either one of z's children is nil, then we must remove z
            if (IsNil(z.Left) || IsNil(z.Right))
                y = z;
            // else we must remove the successor of z
            else y = TreeSuccessor(z);
            // Let x be the left or right child of y (y can only have one child)
            if (!IsNil(y.Left))
                x = y.Left;
            else
                x = y.Right;
            // link x's parent to y's parent
            x.Parent = y.Parent;
            // If y's parent is nil, then x is the root
            if (IsNil(y.Parent))
                _root = x;
            // else if y is a left child, set x to be y's left sibling
            else if (!IsNil(y.Parent.Left) && y.Parent.Left == y)
                y.Parent.Left = x;
            // else if y is a right child, set x to be y's right sibling
            else if (!IsNil(y.Parent.Right) && y.Parent.Right == y)
                y.Parent.Right = x;
            // if y != z, transfer y's satellite data into z.
            if (y != z)
            {
                z.Key = y.Key;
            }
            // Update the numLeft and numRight numbers which might need
            // updating due to the deletion of z.key.
            FixNodeData(x, y);
            // If y's color is black, it is a violation 
            if (y.Color == BLACK)
                RemoveFixup(x);
        }

        /// <summary>
        /// Updates the numLeft and numRight numbers which might need updating due to the deletion
        /// </summary>
        /// <param name="x">the RedBlackNode which was actually deleted from the tree</param>
        /// <param name="y">the value of the key that used to be in y</param>
        private void FixNodeData(Node x, Node y)
        {
            // Initialize two variables which will help us traverse the tree
            Node current = _nil;
            Node track = _nil;
            // if x is nil, then we will start updating at y.parent
            // Set track to y, y.parent's child
            if (IsNil(x))
            {
                current = y.Parent;
                track = y;
            }
            // if x is not nil, then we start updating at x.parent
            // Set track to x, x.parent's child
            else
            {
                current = x.Parent;
                track = x;
            }
            // while we haven't reached the root
            while (!IsNil(current))
            {
                // if the node we deleted has a different key than
                // the current node
                if (!y.Key.Equals(current.Key))
                {
                    // if the node we deleted is greater than
                    // current.key then decrement current.numRight
                    if (y.Key.CompareTo(current.Key) > 0)
                        current.NumRight--;
                    // if the node we deleted is less than
                    // current.key then decrement current.numLeft
                    if (y.Key.CompareTo(current.Key) < 0)
                        current.NumLeft--;
                }
                // if the node we deleted has the same key as the
                // current node we are checking
                else
                {
                    // the cases where the current node has any nil
                    // children and update appropriately
                    if (IsNil(current.Left))
                        current.NumLeft--;
                    else if (IsNil(current.Right))
                        current.NumRight--;
                    // the cases where current has two children and
                    // we must determine whether track is it's left
                    // or right child and update appropriately
                    else if (track == current.Right)
                        current.NumRight--;
                    else if (track == current.Left)
                        current.NumLeft--;
                }
                // update track and current
                track = current;
                current = current.Parent;
            }
        }

        /// <summary>
        /// Restores the Red Black properties that may have been violated during removal
        /// </summary>
        /// <param name="x">the child of the deleted node from remove(RedBlackNode v)</param>
        private void RemoveFixup(Node x)
        {
            Node w;

            // While we haven't fixed the tree completely...
            while (x != _root && x.Color == BLACK)
            {
                // if x is it's parent's left child
                if (x == x.Parent.Left)
                {
                    // set w = x's sibling
                    w = x.Parent.Right;
                    // Case 1, w's color is red.
                    if (w.Color == RED)
                    {
                        w.Color = BLACK;
                        x.Parent.Color = RED;
                        RotateLeft(x.Parent);
                        w = x.Parent.Right;
                    }
                    // Case 2, both of w's children are black
                    if (w.Left.Color == BLACK &&
                            w.Right.Color == BLACK)
                    {
                        w.Color = RED;
                        x = x.Parent;
                    }
                    // Case 3 / Case 4
                    else
                    {
                        // Case 3, w's right child is black
                        if (w.Right.Color == BLACK)
                        {
                            w.Left.Color = BLACK;
                            w.Color = RED;
                            RotateRight(w);
                            w = x.Parent.Right;
                        }
                        // Case 4, w = black, w.right = red
                        w.Color = x.Parent.Color;
                        x.Parent.Color = BLACK;
                        w.Right.Color = BLACK;
                        RotateRight(x.Parent);
                        x = _root;
                    }
                }
                // if x is it's parent's right child
                else
                {
                    // set w to x's sibling
                    w = x.Parent.Left;
                    // Case 1, w's color is red
                    if (w.Color == RED)
                    {
                        w.Color = BLACK;
                        x.Parent.Color = RED;
                        RotateRight(x.Parent);
                        w = x.Parent.Left;
                    }
                    // Case 2, both of w's children are black
                    if (w.Right.Color == BLACK && w.Left.Color == BLACK)
                    {
                        w.Color = RED;
                        x = x.Parent;
                    }
                    // Case 3 / Case 4
                    else
                    {
                        // Case 3, w's left child is black
                        if (w.Left.Color == BLACK)
                        {
                            w.Right.Color = BLACK;
                            w.Color = RED;
                            RotateRight(w);
                            w = x.Parent.Left;
                        }
                        // Case 4, w = black, and w.left = red
                        w.Color = x.Parent.Color;
                        x.Parent.Color = BLACK;
                        w.Left.Color = BLACK;
                        RotateRight(x.Parent);
                        x = _root;
                    }
                }
            }
            // set x to black to ensure there is no violation
            x.Color = BLACK;
        }

        /// <summary>
        /// Searches for a node with key k and returns the first such node
        /// </summary>
        /// <param name="key">the key whose node we want to search for</param>
        /// <returns>node with the key if found or null</returns>
        public Node Search(T key)
        {
            // Initialize a pointer to the root to traverse the tree
            Node current = _root;
            // While we haven't reached the end of the tree
            while (!IsNil(current))
            {
                // If we have found a node with a key equal to key
                if (current.Key.Equals(key))
                    // return that node and exit search(int)
                    return current;
                // go left or right based on value of current and key
                else if (current.Key.CompareTo(key) < 0)
                    current = current.Right;
                // go left or right based on value of current and key
                else
                    current = current.Left;
            }
            // we have not found a node whose key is "key"
            return null;
        }

        /// <summary>
        /// Returns the number of nodes in the tree that are greater then the given key
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>the number of elements greater than key</returns>
        public int NumGreater(T key) => FindNumGreater(_root, key);

        /// <summary>
        /// Returns the number of nodes in the tree that are smaller then the given key
        /// </summary>
        /// <param name="key">Key to test</param>
        /// <returns>the number of elements smaller than key</returns>
        public int NumSmaller(T key) => FindNumSmaller(_root, key);

        /// <summary>
        /// Gets the number of nodes greater then the key of the given tree
        /// </summary>
        /// <param name="node">Root or subtree root node to test on</param>
        /// <param name="key">key to compare to</param>
        /// <returns>number of nodes greater then the key</returns>
        public int FindNumGreater(Node node, T key)
        {
            // Base Case: if node is nil, return 0
            if (IsNil(node))
                return 0;
            // If key is less than node.key
            if (key.CompareTo(node.Key) < 0)
                return 1 + node.NumRight + FindNumGreater(node.Left, key);
            // If key is greater than node.key
            return FindNumGreater(node.Right, key);
        }

        /// <summary>
        /// Returns sorted list of keys greater than key. Size of list will not exceed maxReturned
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <param name="maxReturned">Maximum number of results to return</param>
        /// <returns>ist of keys greater than key. List may not exceed maxReturned</returns>
        public List<T> GetGreaterThan(T key, Int32 maxReturned)
        {
            List<T> list = new List<T>();
            GetGreaterThan(_root, key, list);
            return list.GetRange(0, Math.Min(maxReturned, list.Count));
        }

        /// <summary>
        /// Gets the nodes greater then the key
        /// </summary>
        /// <param name="node">Node to test on key</param>
        /// <param name="key">Key to test</param>
        /// <param name="list">List of nodes greater then the key</param>
        private void GetGreaterThan(Node node, T key, List<T> list)
        {
            if (!IsNil(node))
            {
                if (node.Key.CompareTo(key) > 0)
                {
                    GetGreaterThan(node.Left, key, list);
                    list.Add(node.Key);
                    GetGreaterThan(node.Right, key, list);
                }
                else
                    GetGreaterThan(node.Right, key, list);
            }
        }

        /// <summary>
        /// Takes root or subtree root node and finds number of elements smaller then the given key
        /// </summary>
        /// <param name="node">Root of the tree to start comparison</param>
        /// <param name="key">key to compare</param>
        /// <returns>number of nodes smaller than key.</returns>
        public int FindNumSmaller(Node node, T key)
        {
            // Base Case: if node is nil, return 0
            if (IsNil(node))
                return 0;
            // If key is less than node.key, look to the left as all
            if (key.CompareTo(node.Key) <= 0)
                return FindNumSmaller(node.Left, key);
            // If key is larger than node.key, all elements to the left of
            return node.NumLeft + FindNumSmaller(node.Right, key) + 1;

        }

        /// <summary>
        /// tests if a node is the nil node
        /// </summary>
        /// <param name="node">node to test</param>
        /// <returns>true|false</returns>
        private bool IsNil(Node node) => node == _nil;

        /// <summary>
        /// returns the size of the tree
        /// </summary>
        /// <returns>size of the tree</returns>
        public int Size() => _root.NumLeft + _root.NumRight + 1;
    }
}
