/*******************************************************
*  AVLTree.js
*  Created by Stephen Hall on 7/3/16.
*  Copyright (c) 2016 Stephen Hall. All rights reserved.
*  A AVL Tree implementation in JavaScript
********************************************************/


/*******************************************************
*
* Class Declerations
*
********************************************************/

/** 
 * Node Object declaration
 * 
 * @param color of node, key value pair, left right node, count of subtree
 *
 * @return none
 * @throws none
 **/

// Pass in the attribute you want to use for comparing
 AVLTree = function (_comparison, _equality) {
    this.root = null;
    this.count = 0;

    this.comparison = _comparison ? _comparison : function (val1, val2) {
        return val1 - val2;
    };
    this.equality = _equality ? _equality : function (val1, val2) {
        return val1 === val2;
    };
}

    // Node is the internal class of the AVLTree that is used for
    //  storing the values and maintaining the structure of the
    //  AVLTree.
    //
    // The Node class has the following properties:
    //  val (?)         The value of the node.
    //  parent (Node)   The parent of the node.
    //  balanceFactor   The longest chain of nodes under its left child
    //   (number)       minus the longest chain of nodes under its right
    //                  child.
    // left (Node)      The left child of the node.
    // right (Node)     The right child of the node.
    //
    // @param val The value of the node.
    // @returns The new Node that is created.
    Node = function(val) {
        this.val = val;
        this.parent = null;
        this.balanceFactor = 0;
	
        this.left = null;
        this.right = null;
	
        this.isRoot = function() {
            return (this.parent == null);
        }
	
        this.isLeaf = function() {
            return (this.left == null) && (this.right == null);
        };
	
        this.isLeftChild = function() {
            return this.parent.left == this;
        };
    }

    // Clears all the nodes of the AVLTree.
    AVLTree.prototype.clear = function() {
        this.root = null;
        this.count = 0;
    }

    // Returns the minimal value currently present in the AVLTree.
    // @returns The minimal value.
    AVLTree.prototype.min = function () {
        if (this.root == null)
            return undefined;

        var maxNode = this.root;
        while (maxNode.left != null) {
            maxNode = maxNode.left;
        }

        return maxNode.val;
    }

    // Returns the maximal value currently present in the AVLTree.
    // @returns The maximal value.
    AVLTree.prototype.max = function () {
        if (this.root == null)
            return undefined;

        var maxNode = this.root;
        while (maxNode.right != null) {
            maxNode = maxNode.right;
        }

        return maxNode.val;
    }

    // Traverse the AVLTree in ascending sorted order.
    // @returns An array of the values in the AVLTree in ascending 
    //     sorted order.
    AVLTree.prototype.traverse = function () {
        var arr = [],
		    inOrder = function (node) {
		        if (node == null) {
		            return;
		        }
		        inOrder(node.left);
		        arr.push(node.val);
		        inOrder(node.right);
		    };
        inOrder(this.root);
        return arr;
    }

    // Adds a new value to the AVLTree and balances the AVLTree if
    //  neccessary.
    // @param val The value to be added.
    // @returns The new node that was added to the AVLTree.
    AVLTree.prototype.add = function(val) {
        var newNode = new Node(val);
        this.count += 1;
	
        if (this.root == null) {
            this.root = newNode;
            return newNode;
        }
	
        var currentNode = this.root;
        while (true) {
            if (this.comparison(val, currentNode.val) < 0) {
                if (currentNode.left) {
                    currentNode = currentNode.left;
                } else {
                    currentNode.left = newNode;
                    break;
                }
            } else {
                if (currentNode.right) {
                    currentNode = currentNode.right;
                } else {
                    currentNode.right = newNode;
                    break;
                }	
            }
        }
        newNode.parent = currentNode;
	
        currentNode = newNode;
        while (currentNode.parent) {
            var parent = currentNode.parent,
			    prevBalanceFactor = parent.balanceFactor;
		
            if (currentNode.isLeftChild()) {
                parent.balanceFactor += 1;
            } else {
                parent.balanceFactor -= 1;
            }
		
            if (Math.abs(parent.balanceFactor) < Math.abs(prevBalanceFactor)) {
                break;
            }
		
            if (parent.balanceFactor < -1 || parent.balanceFactor > 1) {
                this._rebalance(parent);
                break;
            }
            currentNode = parent;
        }
	
        return newNode;
    }

    // Searches for a certain value in the AVLTree and returns the
    //  corresponding node.
    // @param val The value to search for.
    // @returns The node that contains the value, or null when it is
    //     not in the AVLTree.
    AVLTree.prototype.search = function (val) {
        var currentNode = this.root;
        while (currentNode) {
            if (this.equality(val, currentNode.val)) {
                return currentNode;
            }

            if (this.comparison(val, currentNode.val) < 0) {
                currentNode = currentNode.left;
            } else {
                currentNode = currentNode.right;
            }
        }
        return null;
    }

    // Removes a val from the AVLTree and rebalances it when neccesarry.
    // @param val The value to remove.
    // @returns Whether the value was removed. The value can not be removed
    //    when it is not found in the tree.
    AVLTree.prototype.remove = function (val) {
        var foundNode = this.search(val);
        if (foundNode) {
            this.removeNode(foundNode);
        }
        return foundNode;
    }

    // Removes a node from the AVLTree and rebalances it when neccessary.
    // @param node The node to remove.
    AVLTree.prototype.removeNode = function (node) {
        var currentNode = node;
        this.count -= 1;
        if ((currentNode.left == null) || (currentNode.right == null)) {
            if (currentNode.isLeaf()) {
                //0-Children
                if (currentNode.isRoot()) {
                    this.root = null;
                } else {
                    var fakeNode = {
                        parent: currentNode.parent,
                        isLeftChild: function () { return true; }
                    };
                    if (currentNode.isLeftChild()) {
                        currentNode.parent.left = null;
                    } else {
                        currentNode.parent.right = null;
                        fakeNode.isLeftChild= function () { return false; };
                    }
                    currentNode = fakeNode;
                }
            } else {
                //1-Child
                var singleChild = currentNode.left ? currentNode.left : currentNode.right;
                if (currentNode.isRoot()) {
                    this.root = singleChild;
                } else {
                    if (currentNode.isLeftChild()) {
                        currentNode.parent.left = singleChild;
                    } else {
                        currentNode.parent.right = singleChild;
                    }
                }
                singleChild.parent = currentNode.parent;
                currentNode = singleChild;
            }
        } else {
            //2-Children
            var minNode = currentNode.left;
            while (minNode.right != null) {
                minNode = minNode.right;
            }
		
            if (currentNode.left == minNode) {
                //Special 2-Children Case
                if (currentNode.isRoot()) {
                    //Find parent of minNode and assign minNode
                    this.root = minNode;
                    //Set parent of minNode
                    minNode.parent = null;
                } else {
                    //Find parent of minNode and assign minNode
                    if (currentNode.isLeftChild()) {
                        currentNode.parent.left = minNode;
                    } else {
                        currentNode.parent.right = minNode;
                    }
                    //Set parent of minNode
                    minNode.parent = currentNode.parent;
                }
                //Connect right of minNode
                minNode.right = currentNode.right;
                minNode.right.parent = minNode;

                //Create fake node that comes from left of minNode to update balanceFactors
                minNode.balanceFactor = currentNode.balanceFactor;
                var fakeNode = {
                    parent: minNode,
                    isLeftChild: function() { return true; }
                };
                currentNode = fakeNode;
            } else {
                //Non-Special 2-Children Case

                //Cache parent and left of the minNode
                var minParent = minNode.parent;
                var minLeft = minNode.left;

                //Move minLeft to position of minNode
                minParent.right = minLeft;
                if (minLeft) {
                    minLeft.parent = minParent;
                }

                //Connect minNode to new parent
                if (currentNode.isRoot()) {
                    //Find parent of minNode and assign minNode
                    this.root = minNode;
                    //Set parent of minNode
                    minNode.parent = null;
                } else {
                    //Find parent of minNode and assign minNode
                    if (currentNode.isLeftChild()) {
                        currentNode.parent.left = minNode;
                    } else {
                        currentNode.parent.right = minNode;
                    }
                    //Set parent of minNode
                    minNode.parent = currentNode.parent;
                }

                //Connect minNode to children of currentNode
                minNode.right = currentNode.right;
                minNode.right.parent = minNode;
                minNode.left = currentNode.left;
                minNode.left.parent = minNode;
                minNode.balanceFactor = currentNode.balanceFactor;

                //Create fake node here:
                var fakeNode = {
                    parent: minParent,
                    isLeftChild: function() { return false; }
                };
                currentNode = fakeNode;
            }
        }
	
        //Rebalance the path back to the root
        while (currentNode.parent) {
            var parent = currentNode.parent,
			    prevBalanceFactor = parent.balanceFactor
		
            if (currentNode.isLeftChild()) {
                parent.balanceFactor -= 1;
            } else {
                parent.balanceFactor += 1;
            }
		
            if (Math.abs(parent.balanceFactor) > Math.abs(prevBalanceFactor)) {
                //If moving further away from 0 balanceFactor
			
                if (parent.balanceFactor < -1 || parent.balanceFactor > 1) {
                    //If out of balance
				
                    //Rebalance
                    this._rebalance(parent);
				
                    //If balanceFactor is now 0, length has decreased on both sides, keep going with tracing back path
                    if (parent.parent.balanceFactor === 0) {						
                        currentNode = parent.parent;
                    } else {
                        break;
                    }
                } else {
                    break;
                }
            } else {
                currentNode = parent;
            }
        }
    }

    // Private function used to rebalance the node when its balanceFactor is
    //  -2 or 2.
    AVLTree.prototype._rebalance = function (node) {
        if (node.balanceFactor < 0) {
            if (node.right.balanceFactor > 0) {
                this._rotateRight(node.right);
                this._rotateLeft(node);
            } else {
                this._rotateLeft(node);
            }
        } else if (node.balanceFactor > 0) {
            if (node.left.balanceFactor < 0) {
                this._rotateLeft(node.left);
                this._rotateRight(node);
            } else {
                this._rotateRight(node);
            }
        }
    }

    // Private function used to preform a left rotation with a node as rotation
    //  root.
    AVLTree.prototype._rotateLeft = function (rotRoot) {
        var newRoot = rotRoot.right;
        rotRoot.right = newRoot.left;
        if (newRoot.left != null) {
            newRoot.left.parent = rotRoot;
        }
        newRoot.parent = rotRoot.parent;
        if (rotRoot.parent == null) {
            this.root = newRoot;
        } else {
            if (rotRoot.isLeftChild()) {
                rotRoot.parent.left = newRoot;
            } else {
                rotRoot.parent.right = newRoot;
            }
        }
        newRoot.left = rotRoot;
        rotRoot.parent = newRoot;
        rotRoot.balanceFactor = rotRoot.balanceFactor + 1 - Math.min(newRoot.balanceFactor, 0);
        newRoot.balanceFactor = newRoot.balanceFactor + 1 + Math.max(rotRoot.balanceFactor, 0);
    }

    // Private function used to preform a right rotation with a node as rotation
    //  root.
    AVLTree.prototype._rotateRight = function (rotRoot) {
        var newRoot = rotRoot.left;
        rotRoot.left = newRoot.right;
        if (newRoot.right != null) {
            newRoot.right.parent = rotRoot;
        }
        newRoot.parent = rotRoot.parent;
        if (rotRoot.parent == null) {
            this.root = newRoot;
        } else {
            if (rotRoot.isLeftChild()) {
                rotRoot.parent.left = newRoot;
            } else {
                rotRoot.parent.right = newRoot;
            }
        }
        newRoot.right = rotRoot;
        rotRoot.parent = newRoot;
        rotRoot.balanceFactor = rotRoot.balanceFactor - 1 - Math.max(newRoot.balanceFactor, 0);
        newRoot.balanceFactor = newRoot.balanceFactor - 1 + Math.min(rotRoot.balanceFactor, 0);
    }
