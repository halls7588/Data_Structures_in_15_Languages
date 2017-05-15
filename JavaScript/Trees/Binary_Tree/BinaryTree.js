/*******************************************************
*  LinkedList.js
*  Created by Stephen Hall on 6/29/16.
*  Copyright (c) 2016 Stephen Hall. All rights reserved.
*  A Binary Tree implementation in JavaScript
********************************************************/

/** 
 * Node Object declaration
 * 
 * @param  generic data
 *
 * @return none
 * @throws none
 **/
function Node(data){
    this.parent = null;
    this.left = null;
    this.right = null;
    this.data = data; 
};

/** 
 * Binary Tree Object declaration
 *          
 * @param  none
 *
 * @return none
 * @throws none
 **/
function BinaryTree(){
    this.root = null;
    
    /** 
     * Node compare function
     *      Compares nodes data to data for equality
     * 
     * @param  generic data, node to compare
     *
     * @return int to represent greater then, less then, equal to
     * @throws none
     **/
    this.compare = function(data, node){
        if(node == null || data == null)
            return null;
        
        if(node.data > data)
            return 1;
        
        if(node.data < data)
            return -1;
        
        if(node.data == data)
            return 0;
        
        return null;
    };
    
    /** 
     * Insert Helper method
     *      Inserts node into the tree
     *
     * @param  generic data, compare node
     *
     * @return Inserted Node
     * @throws none
     **/
    this.insertHelper = function(data, node){
        var cmp = this.compare(data, node);
        if(cmp != null) {
            if(cmp == 1){
                if(!node.right){
                    newNode = new Node(data);
                    node.right = newNode;
                    newNode.parent = node;
                    return newNode;
                }
                else
                    return this.insertHelper(data, node.right);
            }
            if(cmp <= 0){
                if(!node.left){
                    newNode = new Node(data);
                    node.left = newNode;
                    newNode.parent = node;
                    return newNode;
                }
                else
                    return this.insertHelper(data, node.left);
            }
        }
        else
            return null;
    };
    
    /** 
     * Remove helper function
     *          Removes node from the tree
     *
     * @param  generic data, compare node
     *
     * @return removed node
     * @throws none
     **/
    this.removeHelper = function(data, node){
        var cmp = this.compare(data, node);
        if(cmp != null){
            if(cmp == 1)
                return this.removeHelper(data, node.right);
            if(cmp == -1)
                return this.removeHelper(data, node.left);
            if(cmp == 0){
                //has no children

                var tempNode = null;

                if(!node.left && !node.right) {
                    node = node.parent;
                }
                
                //has one child
                if(node.parent) { 
                	tempNode = node.parent;
                }
                else { // this is the root node
                	if(node.right) {
                		tempNode = node.right;
                		tempNode.parent = null;
                		this.root.right = null;
                		this.root = tempNode;
                		return node;
                	}
                	else {
            			tempNode = node.left;
            			tempNode.parent = null;
                		this.root.left = null;
                		this.root = tempNode;
                		return node;
                	}
                }
                
                if(tempNode.left === node) {
        			if(node.left) {
        				tempNode.left = node.left;
        				node.left.parent = tempNode;
        				return node;
        			}
        			else if(node.right) {
        				tempNode.right = node.right;
        				node.right.parent = tempNode;
        				return node;
        			}
            	}
	            if(tempNode.right === node) {
        			if(node.left) {
        				tempNode.left = node.left;
        				node.left.parent = tempNode;
        				return node;
        			}
        			else if(node.right) {
        				tempNode.right = node.right;
        				node.right.parent = tempNode;
        				return node;
        			}
            	}

                else if (node.left && node.right) { // test for 2 children
                	tempNode = node.right; // find min value of right child tree to replace the node
                	while(tempNode.left)
        				tempNode = tempNode.left;
        			var temp = node.data;
                	node.data = tempNode.data; // replace the node with the tempNode
                	tempNode.data = temp;
                	tempNode.parent.left = null;
                	tempNode.parent = null;
                	return tempNode;
                }
                
            }

        }
        else
            return null;
    };
    
}

/** 
 * Insert prototype function
 *          Inserts node into the tree
 *
 * @param  generic data
 *
 * @return Inserted node
 * @throws none
 **/
BinaryTree.prototype.insert = function(data){
    if(!this.root){
       return this.root = new Node(data);
    }
    console.log("added " + data);
    return this.insertHelper(data, this.root);

};

/** 
 * Remove prototype function
 *          Removes node from the tree
 *
 * @param  generic data 
 *
 * @return removed node
 * @throws none
 **/
BinaryTree.prototype.remove = function(data) {
    if(!this.root)
        return null;
    return this.removeHelper(data, this.root);

};

/** 
 * Get Minimum value prototype function
 *          finds the smallest value in the tree
 *
 * @param  none
 *
 * @return smallest node
 * @throws none
 **/
BinaryTree.prototype.getMin = function(){
    if(!this.root)
        return null;
    
    node = this.root;
    
    while(node.left)
        node = node.left;
    
    return node;
};

/** 
 * Get Maximum value prototype function
 *          finds the largest value in the tree
 *
 * @param  none
 *
 * @return largest node
 * @throws none
 **/
BinaryTree.prototype.getMax = function(){
    if(!this.root)
        return null;
    
    node = this.root;
    
    while(node.right)
        node = node.right;
    
    return node;
};


/** 
 * Preorder traversal prototype function
 *          preforms recursive preorder traversal on the tree
 *
 * @param  start node
 *
 * @return none
 * @throws none
 **/
BinaryTree.prototype.preOrderTraversal = function(node){
    if(node !== null){
        console.log(node.data);
        this.preOrderTraversal(node.left);
        this.preOrderTraversal(node.right);
    }
};

/** 
 * Post traversal prototype function
 *          preforms recursive Postorder traversal on the tree
 *
 * @param  start node
 *
 * @return none
 * @throws none
 **/
BinaryTree.prototype.postOrderTraversal = function(node){
    if(node != null){
        this.postOrderTraversal(node.left);
        this.postOrderTraversal(node.right);
        console.log(node.data);
    }
};

/** 
 * Inorder traversal prototype function
 *          preforms recursive inorder traversal on the tree
 *
 * @param  start node
 *
 * @return none
 * @throws none
 **/
BinaryTree.prototype.inOrderTraversal = function(node){
    if(node != null){
        this.inOrderTraversal(node.left);
        console.log(node.data);
        this.inOrderTraversal(node.right);
    }
};

/** 
 * Depth Frist Search prototype function
 *          preforms Depth First Search on the tree
 *
 * @param  start node
 *
 * @return none
 * @throws none
 **/
BinaryTree.prototype.depthFirstSearch = function(node){
		if(node == null)
            return;

        var stack = [];
        stack.push(node);
        
		while (stack.length > 0) {
			console.log(node = stack.pop());
			if(node.right!=null) 
                stack.push(node.right);
			if(node.left!=null) 
                stack.push(node.left);			
		}
};
	
/** 
 * Breadth First Search prototype function
 *          preforms Breadth First Search on the tree
 *
 * @param  start node
 *
 * @return none
 * @throws none
 **/
BinaryTree.prototype.breadthFirstSearch = function(node){
    if(node == null)
        return;

    var queue = [];
    queue.push(node);

    while (queue.length > 0) {
        console.log((node = queue.shift()));
        if(node.left != null) 
            queue.push(node.left);			
        if(node.right != null) 
            queue.push(node.right);

    }
};

