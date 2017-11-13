/*******************************************************
 *  AvlTree.java
 *  Created by Stephen Hall on 11/13/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Avl Tree implementation in Java
 ********************************************************/


public class AvlTree<T extends Comparable<T>> {

    /**
     * Node class for AVL Tree
     */
    public class Node {
        public T data;
        public Node left;
        public Node right;
        public int height;

        /**
         * Empty Node constructor
         * @param theElement
         */
        public Node(T data){
            this(data, null, null);
        }

        /**
         * Node constructor with left and right leafs
         * @param data: data to hold
         * @param left: left child
         * @param right: right child
         */
        public Node(T data, Node left, Node right){
            this.data = data;
            this.left = left;
            this.right = right;
        }
    }

    private Node root;
    private int count;

    /**
     * Avl Tree Constructor.
     */
    public AvlTree (){
        root = null;
        count = 0;
    }

    /**
     * Gets the height of a node
     * @param node: node to test
     * @return int: height of the node
     */
    public int Height(Node node){
        return node == null ? -1 : node.height;
    }

    /**
     * Find the max value among the given numbers.
     * @param a First number
     * @param b Second number
     * @return Maximum value
     */
    public int Max(int a, int b){
        if (a > b)
            return a;
        return b;
    }

    /**
     * Insert an element into the tree.
     * @param data: data to insert into the tree
     * @return boolean: success|fail
     */
    public boolean Insert(T data){
        try {
            root = Insert (data, root);
            count++;
            return true;
        } catch(Exception e){
            return false;
        }
    }

    /**
     * Private Insert Helper
     * @param data: data to add
     * @param node Root of the tree
     * @return Node: New root of the tree
     * @throws Exception: failure or duplicate value
     */
    private Node Insert(T data, Node node) throws Exception{
        if (node == null)
            node = new Node(data);
        else if (data.compareTo (node.data) < 0){
            node.left = Insert (data, node.left);

            if (Height(node.left) - Height(node.right) == 2){
                if (data.compareTo (node.left.data) < 0){
                    node = RotateLeft(node);
                }
                else {
                    node = RotateRightLeft(node);
                }
            }
        }
        else if (data.compareTo (node.data) > 0){
            node.right = Insert(data, node.right);

            if (Height(node.right) - Height(node.left) == 2)
                if (data.compareTo (node.right.data) > 0){
                    node = RotateRight(node);
                }
                else{
                    node = RotateLeftRight(node);
                }
        }
        else {
            throw new Exception("Attempting to insert duplicate value");
        }

        node.height = Max(Height(node.left), Height(node.right)) + 1;
        return node;
    }

    /**
     * Rotates Node left
     * @param node: node to rotate
     * @return Node: new root node
     */
     private Node RotateLeft (Node node){
        Node tmp = node.left;

        node.left = tmp.right;
        tmp.right = node;

        node.height = Max(Height(node.left), Height(node.right)) + 1;
        tmp.height = Max(Height(tmp.left), node.height) + 1;

        return (tmp);
    }


    /**
     * Rotate left child Right then rotate left
     * @param node: node to rotate
     * @return Node: new root node
     */
    private Node RotateRightLeft(Node node){
        node.left = RotateRight(node.left);
        return RotateLeft (node);
    }

    /**
     * Rotate Right
     * @param node: node to rotate
     * @return Node: new root node
     */
    private Node RotateRight(Node node){
        Node tmp = node.right;

        node.right = tmp.left;
        tmp.left = node;

        node.height = Max(Height(node.left), Height(node.right)) + 1;
        tmp.height = Max(Height(tmp.right), node.height) + 1;

        return (tmp);
    }

    /**
     * Rotate right child left then rotate right
     * @param node: node to rotate
     * @return Node: new root node
     */
    private Node RotateLeftRight (Node node){
        node.right = RotateLeft(node.right);
        return RotateRight(node);
    }

    /**
     * Deletes all nodes from the tree.
     */
    public void MakeEmpty(){
        root = null;
    }

    /**
     * Determine if the tree is empty.
     * @return True if the tree is empty
     */
    public boolean IsEmpty(){
        return (root == null);
    }

    /**
     * Find the smallest item in the tree.
     * @return T: smallest item or null if empty.
     */
    public T FindMin(){
        if(IsEmpty())
            return null;

        return FindMin(root).data;
    }

    /**
     * Find the largest item in the tree.
     * @return T: the largest item or null if empty.
     */
    public T FindMax(){
        if(IsEmpty( ))
            return null;
        return FindMax(root).data;
    }

    /**
     * Find min helper
     * @param Node: root to test
     * @return Node: node containing the smallest item.
     */
    private Node FindMin(Node node){
        if(node == null )
            return node;

        while(node.left != null )
            node = node.left;

        return node;
    }

    /**
     * Find max helper
     * @param node: root to test.
     * @return Node: node containing the largest item.
     */
    private Node FindMax(Node node) {
        if(node == null)
            return node;

        while(node.right != null)
            node = node.right;

        return node;
    }

    /**
     * Remove item from the tree.
     * @param data: item to remove.
     */
    public void Remove(T data) {
        root = Remove(data, root);
    }

    /**
     * Remove helper function
     * @param data: data to remove
     * @param node: root to start at
     * @return Node: new root node
     */
    private Node Remove(T data, Node node) {
        if (node == null){
            return null;
        }

        if (data.compareTo(node.data) < 0 ) {
            node.left = Remove(data, node.left);
            int l = node.left != null ? node.left.height : 0;

            if((node.right != null) && (node.right.height - l >= 2)) {
                int rightHeight = node.right.right != null ? node.right.right.height : 0;
                int leftHeight = node.right.left != null ? node.right.left.height : 0;

                if(rightHeight >= leftHeight)
                    node = RotateLeft(node);
                else
                    node = RotateLeftRight(node);
            }
        }
        else if (data.compareTo(node.data) > 0) {
            node.right = Remove(data, node.right);
            int r = node.right != null ? node.right.height : 0;
            if((node.left != null) && (node.left.height - r >= 2)) {
                int leftHeight = node.left.left != null ? node.left.left.height : 0;
                int rightHeight = node.left.right != null ? node.left.right.height : 0;
                if(leftHeight >= rightHeight)
                    node = RotateRight(node);
                else
                    node = RotateRightLeft(node);
            }
        }
        else if(node.left != null) {
            node.data = FindMax(node.left).data;
            Remove(node.element, node.left);

            if((node.right != null) && (node.right.height - node.left.height >= 2)) {
                int rightHeight = node.right.right != null ? node.right.right.height : 0;
                int leftHeight = node.right.left != null ? node.right.left.height : 0;

                if(rightHeight >= leftHeight)
                    node = RotateLeft(node);
                else
                    node = RotateLeftRight(t);
            }
        }

        else
            node = (node.left != null) ? node.left : node.right;

        if(node != null) {
            int leftHeight = noe.left != null ? node.left.height : 0;
            int rightHeight = node.right!= null ? node.right.height : 0;
            node.height = Max(leftHeight, rightHeight) + 1;
        }
        return node;
    }

    /**
     * Detrimines is the data exists in the tree
     * @param data: data to find
     * @return boolean: success|fail
     */
    public boolean Contains(T data){
        return Contains(data, root);
    }

    /**
     * Contains helper method
     * @param data: data to find
     * @param node: node to test
     * @return boolean: success|fail
     */
    private boolean Contains(T data, Node node) {
        if (node == null){
            return false; // The node was not found

        } else if (data.compareTo(node.data) < 0){
            return Contains(data, node.left);
        } else if (data.compareTo(node.data) > 0){
            return Contains(data, node.right);
        }

        return true;
    }
}