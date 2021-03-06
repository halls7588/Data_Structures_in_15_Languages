/*******************************************************
 *  RedBlackTree.java
 *  Created by Stephen Hall on 11/06/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Rec Black Tree implementation in Java
 ********************************************************/
package Trees.RedBlackTree;

import java.util.ArrayList;
import java.util.List;

/**
 * RedBlackTree class
 * @param <T>: Generic type
 */
public class RedBlackTree<T extends Comparable<T>> {

    /**
     *  Node Colors
     */
    private static final int BLACK = 0;
    private static final int RED = 1;

    /**
     * RedBlackNode Class
     */
    public class Node {
        private T key;
        private Node parent;
        private Node left;
        private Node right;
        private int numLeft = 0;
        private int numRight = 0;
        private int color;

        /**
         * Node Constructor
         */
        public Node(){
            color = BLACK;
            numLeft = 0;
            numRight = 0;
            parent = null;
            left = null;
            right = null;
        }

        /**
         * Constructor which sets key to the argument.
         * @param key: key to set
         */
        public Node(T key){
            this();
            this.key = key;
        }
    }
    // Root initialized to nil.
    private Node nil = new Node();
    private Node root = nil;

    /**
     * RedBalckTree Constructor
     */
    public RedBlackTree() {
        root.left = nil;
        root.right = nil;
        root.parent = nil;
    }

    /**
     * Rotates a node left
     * @param x: The node which the lefRotate is to be performed on.
     */
    private void rotateLeft(Node x){
        // Call rotateLeftFixup() which updates the numLeft
        // and numRight values.
        rotateLeftFixup(x);
        // Perform the left rotate as described in the algorithm
        // in the course text.
        Node y = x.right;
        x.right = y.left;
        // Check for existence of y.left and make pointer changes
        if (!isNil(y.left))
            y.left.parent = x;
        y.parent = x.parent;
        // x's parent is null
        if (isNil(x.parent))
            root = y;
            // x is the left child of it's parent
        else if (x.parent.left == x)
            x.parent.left = y;
            // x is the right child of it's parent.
        else
            x.parent.right = y;
        // Finish of the leftRotate
        y.left = x;
        x.parent = y;
    }

    /**
     * Updates the numLeft & numRight values affected by leftRotate.
     * @param x: The node which the leftRotate is to be performed on.
     */
    private void rotateLeftFixup(Node x){
        // Case 1: Only x, x.right and x.right.right always are not nil.
        if (isNil(x.left) && isNil(x.right.left)){
            x.numLeft = 0;
            x.numRight = 0;
            x.right.numLeft = 1;
        }
        // Case 2: x.right.left also exists in addition to Case 1
        else if (isNil(x.left) && !isNil(x.right.left)){
            x.numLeft = 0;
            x.numRight = 1 + x.right.left.numLeft + x.right.left.numRight;
            x.right.numLeft = 2 + x.right.left.numLeft + x.right.left.numRight;
        }
        // Case 3: x.left also exists in addition to Case 1
        else if (!isNil(x.left) && isNil(x.right.left)){
            x.numRight = 0;
            x.right.numLeft = 2 + x.left.numLeft + x.left.numRight;
        }
        // Case 4: x.left and x.right.left both exist in addtion to Case 1
        else{
            x.numRight = 1 + x.right.left.numLeft + x.right.left.numRight;
            x.right.numLeft = 3 + x.left.numLeft + x.left.numRight + x.right.left.numLeft + x.right.left.numRight;
        }
    }
    
    /**
     * Updates the numLeft and numRight values affected by the Rotate.
     * @param y: The node which the rightRotate is to be performed
     */
    private void rotateRight(Node y){
        // Call rightRotateFixup to adjust numRight and numLeft values
        rotateRightFixup(y);
        // Perform the rotate as described in the course text.
        Node x = y.left;
        y.left = x.right;
        // Check for existence of x.right
        if (!isNil(x.right))
            x.right.parent = y;
        x.parent = y.parent;
        // y.parent is nil
        if (isNil(y.parent))
            root = x;
            // y is a right child of it's parent.
        else if (y.parent.right == y)
            y.parent.right = x;
            // y is a left child of it's parent.
        else
            y.parent.left = x;
        
        x.right = y;
        y.parent = x;
    }

    /**
     * Updates the numLeft and numRight values affected by the rotate
     * @param y: the node around which the righRotate is to be performed.
     */
    private void rotateRightFixup(Node y){
        // Case 1: Only y, y.left and y.left.left exists.
        if (isNil(y.right) && isNil(y.left.right)){
            y.numRight = 0;
            y.numLeft = 0;
            y.left.numRight = 1;
        }
        // Case 2: y.left.right also exists in addition to Case 1
        else if (isNil(y.right) && !isNil(y.left.right)){
            y.numRight = 0;
            y.numLeft = 1 + y.left.right.numRight + y.left.right.numLeft;
            y.left.numRight = 2 + y.left.right.numRight + y.left.right.numLeft;
        }
        // Case 3: y.right also exists in addition to Case 1
        else if (!isNil(y.right) && isNil(y.left.right)){
            y.numLeft = 0;
            y.left.numRight = 2 + y.right.numRight +y.right.numLeft;
        }
        // Case 4: y.right & y.left.right exist in addition to Case 1
        else{
            y.numLeft = 1 + y.left.right.numRight + y.left.right.numLeft;
            y.left.numRight = 3 + y.right.numRight + y.right.numLeft + y.left.right.numRight + y.left.right.numLeft;
        }
    }

    /**
     * Inserts new key into the tree
     * @param key: key to insert
     */
    public void insert(T key) {
        insert(new Node(key));
    }

    /**
     * Inserts z into the appropriate position in the RedBlackTree while updating numLeft and numRight values.
     * @param z: the node to be inserted into the Tree rooted at root
     */
    private void insert(Node z) {
        // Create a reference to root & initialize a node to nil
        Node y = nil;
        Node x = root;
        // While we haven't reached a the end of the tree keep
        // trying to figure out where z should go
        while (!isNil(x)){
            y = x;
            // if z.key is < than the current key, go left
            if (z.key.compareTo(x.key) < 0){
                // Update x.numLeft as z is < than x
                x.numLeft++;
                x = x.left;
            }
            // else z.key >= x.key so go right.
            else{
                // Update x.numGreater as z is => x
                x.numRight++;
                x = x.right;
            }
        }
        // y will hold z's parent
        z.parent = y;
        // Depending on the value of y.key, put z as the left or
        // right child of y
        if (isNil(y))
            root = z;
        else if (z.key.compareTo(y.key) < 0)
            y.left = z;
        else
            y.right = z;
        // Initialize z's children to nil and z's color to red
        z.left = nil;
        z.right = nil;
        z.color = RED;
        // Call insertFixup(z)
        insertFixup(z);
    }

    /**
     * Fixes up the violation of the RedBlackTree properties that may have been caused during insert(z)
     * @param z: the node which was inserted and may have caused a violation of the RedBlackTree properties
     */
    private void insertFixup(Node z){
        Node y = nil;
        // While there is a violation of the RedBlackTree properties..
        while (z.parent.color == RED){
            // If z's parent is the the left child of it's parent.
            if (z.parent == z.parent.parent.left){
                // Initialize y to z 's cousin
                y = z.parent.parent.right;
                // Case 1: if y is red...recolors
                if (y.color == RED){
                    z.parent.color = BLACK;
                    y.color = BLACK;
                    z.parent.parent.color = RED;
                    z = z.parent.parent;
                }
                // Case 2: if y is black & z is a right child
                else if (z == z.parent.right){
                    // leftRotaet around z's parent
                    z = z.parent;
                    rotateLeft(z);
                }
                // Case 3: else y is black & z is a left child
                else{
                    // Recolors and rotate round z's grandpa
                    z.parent.color = BLACK;
                    z.parent.parent.color = RED;
                    rotateRight(z.parent.parent);
                }
            }
            // If z's parent is the right child of it's parent.
            else{
                // Initialize y to z's cousin
                y = z.parent.parent.left;
                // Case 1: if y is red...recolors
                if (y.color == RED){
                    z.parent.color = BLACK;
                    y.color = BLACK;
                    z.parent.parent.color = RED;
                    z = z.parent.parent;
                }
                // Case 2: if y is black and z is a left child
                else if (z == z.parent.left){
                    // rightRotate around z's parent
                    z = z.parent;
                    rotateRight(z);
                }
                // Case 3: if y  is black and z is a right child
                else{
                    // Recolors and rotate around z's grandpa
                    z.parent.color = BLACK;
                    z.parent.parent.color = RED;
                    rotateLeft(z.parent.parent);
                }
            }
        }
        // Color root black at all times
        root.color = BLACK;
    }

    /**
     * gets the smallest node in the tree
     * @param node: Node to test if smallest
     * @return Node: the node with the smallest key rooted at node
     */
    public Node treeMinimum(Node node){
        // while there is a smaller key, keep going left
        while (!isNil(node.left))
            node = node.left;
        return node;
    }

    /**
     * Returns the next largest key from the given node
     * @param x: Node whose successor we must find
     * @return Node: node the with the next largest key from x.key
     */
    public Node treeSuccessor(Node x){
        // if x.left is not nil, call treeMinimum(x.right) and
        // return it's value
        if (!isNil(x.left) )
            return treeMinimum(x.right);
        Node y = x.parent;
        // while x is it's parent's right child...
        while (!isNil(y) && x == y.right){
            // Keep moving up in the tree
            x = y;
            y = y.parent;
        }
        // Return successor
        return y;
    }

    /**
     * Removes a Node from the tree
     * @param node: Node which is to be removed from the the tree
     */
    public void remove(Node node){
        Node z = search(node.key);
        Node x = nil;
        Node y = nil;
        // if either one of z's children is nil, then we must remove z
        if (isNil(z.left) || isNil(z.right))
            y = z;
        // else we must remove the successor of z
        else y = treeSuccessor(z);
        // Let x be the left or right child of y (y can only have one child)
        if (!isNil(y.left))
            x = y.left;
        else
            x = y.right;
        // link x's parent to y's parent
        x.parent = y.parent;
        // If y's parent is nil, then x is the root
        if (isNil(y.parent))
            root = x;
        // else if y is a left child, set x to be y's left sibling
        else if (!isNil(y.parent.left) && y.parent.left == y)
            y.parent.left = x;
        // else if y is a right child, set x to be y's right sibling
        else if (!isNil(y.parent.right) && y.parent.right == y)
            y.parent.right = x;
        // if y != z, transfer y's satellite data into z.
        if (y != z){
            z.key = y.key;
        }
        // Update the numLeft and numRight numbers which might need
        // updating due to the deletion of z.key.
        fixNodeData(x, y);
        // If y's color is black, it is a violation 
        if (y.color == BLACK)
            removeFixup(x);
    }

    /**
     * Updates the numLeft and numRight numbers which might need updating due to the deletion
     * @param x: the RedBlackNode which was actually deleted from the tree
     * @param y: the value of the key that used to be in y
     */
    private void fixNodeData(Node x, Node y){
        // Initialize two variables which will help us traverse the tree
        Node current = nil;
        Node track = nil;
        // if x is nil, then we will start updating at y.parent
        // Set track to y, y.parent's child
        if (isNil(x)){
            current = y.parent;
            track = y;
        }
        // if x is not nil, then we start updating at x.parent
        // Set track to x, x.parent's child
        else{
            current = x.parent;
            track = x;
        }
        // while we haven't reached the root
        while (!isNil(current)){
            // if the node we deleted has a different key than
            // the current node
            if (y.key != current.key) {
                // if the node we deleted is greater than
                // current.key then decrement current.numRight
                if (y.key.compareTo(current.key) > 0)
                    current.numRight--;
                // if the node we deleted is less than
                // current.key then decrement current.numLeft
                if (y.key.compareTo(current.key) < 0)
                    current.numLeft--;
            }
            // if the node we deleted has the same key as the
            // current node we are checking
            else{
                // the cases where the current node has any nil
                // children and update appropriately
                if (isNil(current.left))
                    current.numLeft--;
                else if (isNil(current.right))
                    current.numRight--;
                // the cases where current has two children and
                // we must determine whether track is it's left
                // or right child and update appropriately
                else if (track == current.right)
                    current.numRight--;
                else if (track == current.left)
                    current.numLeft--;
            }
            // update track and current
            track = current;
            current = current.parent;
        }
    }

    /**
     * Restores the Red Black properties that may have been violated during removal
     * @param x: the child of the deleted node from remove(RedBlackNode v)
     */
    private void removeFixup(Node x){
        Node w;

        // While we haven't fixed the tree completely...
        while (x != root && x.color == BLACK){
            // if x is it's parent's left child
            if (x == x.parent.left){
                // set w = x's sibling
                w = x.parent.right;
                // Case 1, w's color is red.
                if (w.color == RED){
                    w.color = BLACK;
                    x.parent.color = RED;
                    rotateLeft(x.parent);
                    w = x.parent.right;
                }
                // Case 2, both of w's children are black
                if (w.left.color == BLACK &&
                        w.right.color == BLACK){
                    w.color = RED;
                    x = x.parent;
                }
                // Case 3 / Case 4
                else{
                    // Case 3, w's right child is black
                    if (w.right.color == BLACK){
                        w.left.color = BLACK;
                        w.color = RED;
                        rotateRight(w);
                        w = x.parent.right;
                    }
                    // Case 4, w = black, w.right = red
                    w.color = x.parent.color;
                    x.parent.color = BLACK;
                    w.right.color = BLACK;
                    rotateRight(x.parent);
                    x = root;
                }
            }
            // if x is it's parent's right child
            else{
                // set w to x's sibling
                w = x.parent.left;
                // Case 1, w's color is red
                if (w.color == RED){
                    w.color = BLACK;
                    x.parent.color = RED;
                    rotateRight(x.parent);
                    w = x.parent.left;
                }
                // Case 2, both of w's children are black
                if (w.right.color == BLACK && w.left.color == BLACK){
                    w.color = RED;
                    x = x.parent;
                }
                // Case 3 / Case 4
                else{
                    // Case 3, w's left child is black
                    if (w.left.color == BLACK){
                        w.right.color = BLACK;
                        w.color = RED;
                        rotateRight(w);
                        w = x.parent.left;
                    }
                    // Case 4, w = black, and w.left = red
                    w.color = x.parent.color;
                    x.parent.color = BLACK;
                    w.left.color = BLACK;
                    rotateRight(x.parent);
                    x = root;
                }
            }
        }
        // set x to black to ensure there is no violation
        x.color = BLACK;
    }

    /**
     * Searches for a node with key k and returns the first such node
     * @param key:  the key whose node we want to search for
     * @return RedBlackNode: node with the key if found or null
     */
    public Node search(T key){
        // Initialize a pointer to the root to traverse the tree
        Node current = root;
        // While we haven't reached the end of the tree
        while (!isNil(current)){
            // If we have found a node with a key equal to key
            if (current.key.equals(key))
                // return that node and exit search(int)
                return current;
            // go left or right based on value of current and key
            else if (current.key.compareTo(key) < 0)
                current = current.right;
            // go left or right based on value of current and key
            else
                current = current.left;
        }
        // we have not found a node whose key is "key"
        return null;
    }

    /**
     * Returns the number of nodes in the tree that are greater then the given key
     * @param key: any Comparable object to test
     * @return int: the number of elements greater than key
     */
    public int numGreater(T key){
        return findNumGreater(root, key);
    }

    /**
     * Returns the number of nodes in the tree that are smaller then the given key
     * @param key: any Comparable object to test
     * @return int: the number of elements smaller than key
     */
    public int numSmaller(T key){
        return findNumSmaller(root, key);

    }

    /**
     * Gets the number of nodes greater then the key of the given tree
     * @param node: Root or subtree root node to test on
     * @param key: key to compare to
     * @return int: number of nodes greater then the key
     */
    public int findNumGreater(Node node, T key){
        // Base Case: if node is nil, return 0
        if (isNil(node))
            return 0;
            // If key is less than node.key
        else if (key.compareTo(node.key) < 0)
            return 1 + node.numRight + findNumGreater(node.left, key);
            // If key is greater than node.key
        else
            return findNumGreater(node.right,key);
    }

    /**
     * Returns sorted list of keys greater than key.  Size of list
     * will not exceed maxReturned
     * @param key Key to search for
     * @param maxReturned Maximum number of results to return
     * @return List of keys greater than key.  List may not exceed maxReturned
     */
    public List<T> getGreaterThan(T key, Integer maxReturned) {
        List<T> list = new ArrayList<T>();
        getGreaterThan(root, key, list);
        return list.subList(0, Math.min(maxReturned, list.size()));
    }

    /**
     * Gets the nodes greater then the key
     * @param node: Node to test on key
     * @param key: Key to test
     * @param list: List of nodes greater then the key
     */
    private void getGreaterThan(Node node, T key, List<T> list) {
        if (!isNil(node)) {
            if (node.key.compareTo(key) > 0) {
                getGreaterThan(node.left, key, list);
                list.add(node.key);
                getGreaterThan(node.right, key, list);
            } else
                getGreaterThan(node.right, key, list);
        }
    }

    /**
     * Takes root or subtree root node and finds number of elements smaller then the given key
     * @param node: Root of the tree to start comparison
     * @param key: key to compare
     * @return int: number of nodes smaller than key.
     */
    public int findNumSmaller(Node node, T key){
        // Base Case: if node is nil, return 0
        if (isNil(node)) 
            return 0;
        // If key is less than node.key, look to the left as all
        else if (key.compareTo(node.key) <= 0)
            return findNumSmaller(node.left, key);
        // If key is larger than node.key, all elements to the left of
        else
            return node.numLeft + findNumSmaller(node.right, key) + 1;

    }

    /**
     * tests if a node is the nil node
     * @param node: node to test
     * @return boolean: true|false
     */
    private boolean isNil(Node node){
        return node == nil;
    }

    /**
     * returns the size of the tree
     * @return int: size of the tree
     */
    public int size(){
        return root.numLeft + root.numRight + 1;
    }
}
