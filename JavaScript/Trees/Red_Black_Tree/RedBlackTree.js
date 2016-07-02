/*******************************************************
*  RedBlackTree.js
*  Created by Stephen Hall on 7/1/16.
*  Copyright (c) 2016 Stephen Hall. All rights reserved.
*  A Red BlackTree implementation in JavaScript
********************************************************/

// Global Definitions for red and black nodes
var RED   = 0;
var BLACK = 1;


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
function Node(color, key, value, left, right, count) {
  this.color = color;
  this.key = key;
  this.value = value;
  this.left = left;
  this.right = right;
  this.count = count;
}

/** 
 * RedblackTree Object declaration
 * 
 * @param compaere function to be used and root element of new tree
 *
 * @return none
 * @throws none
 **/
function RedBlackTree(compare, root) {
  this.compare = compare;
  this.root = root;
}

/** 
 * RedBlackTreeIterator Object declaration
 * 
 * @param subtree to move through, stack to move through
 *
 * @return none
 * @throws none
 **/
function RedBlackTreeIterator(tree, stack) {
  this.tree = tree;
  this.stack = stack;
}


/*******************************************************
*
* RedBlackTree Property Declerations
*
********************************************************/

/** 
 * RedBlackTree begin Propery declaration
 *      Get iterator at begining of the tree
 * 
 * @param Object to define property on, name of property, function description
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
Object.defineProperty(RedBlackTree.prototype, "begin", {
  get: function() {
    var stack = [];
    var node = this.root;
      
    while(node) {
      stack.push(node);
      node = node.left;
    }
      
    return new RedBlackTreeIterator(this, stack);
  }
});

/** 
 * RedBlackTree end Propery declaration
 *      Get iterator at end of the tree
 * 
 * @param Object to define property on, name of property, function description
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
Object.defineProperty(RedBlackTree.prototype, "end", {
  get: function() {
    var stack = [];
    var node = this.root;
      
    while(node) {
      stack.push(n);
      node = node.right;
    }
      
    return new RedBlackTreeIterator(this, stack);
  }
});

/** 
 * RedBlackTree key Propery declaration
 *      Get Keys 
 * 
 * @param Object to define property on, name of property, function description
 *
 * @return keys[]
 * @throws none
 **/
Object.defineProperty(RedBlackTree.prototype, "keys", {
  get: function() {
      
    var result = [];
      
    this.forEach(function(k,v) {
      result.push(k);
    });
      
    return result;
  }
});


/** 
 * RedBlackTree value Propery declaration
 *      get values 
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return values[]
 * @throws none
 **/
Object.defineProperty(RedBlackTree.prototype, "values", {
  get: function() {
      
    var result = [];
      
    this.forEach(function(k,v) {
      result.push(v);
    })
    
    return result;
  }
});

/** 
 * RedBlackTree length Propery declaration
 *      get length of tree 
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return length of tree
 * @throws none
 **/
Object.defineProperty(RedBlackTree.prototype, "length", {
  get: function() {
      
    if(this.root) {
      return this.root.count;
    }
      
    return 0;
  }
});

/*******************************************************
*
* RedBlackTreeIterator Property Declerations
*
********************************************************/

/** 
 * RedBlackTreeIterator hasPrev Propery declaration
 *      determin if iterator has a previous node 
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if previos node exists
 * @throws none
 **/
Object.defineProperty(RedBlackTreeIterator.prototype;, "hasPrev", {
  get: function() {
    var stack = this.stack;
    
    if(stack.length === 0) {
      return false;
    }
    
    if(stack[stack.length-1].left) {
      return true;
    }
    
    for(var s = stack.length - 1; s > 0; --s) {
      if(stack[s - 1].right === stack[s]) {
        return true;
      }
    }
    return false;
  }
});

/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
Object.defineProperty(RedBlackTreeIterator.prototype;, "valid", {
  get: function() {
    return this.stack.length > 0;
  }
});

/** 
 * RedBlackTreeIterator index Propery declaration
 *      get iterator index
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return iterator index
 * @throws none
 **/
Object.defineProperty(RedBlackTreeIterator.prototype;, "index", {
  get: function() {
    if(this.stack.length > 0) {
      return this.stack[this.stack.length - 1];
    }
    
    return null;
  },
  enumerable: true;
});

/** 
 * RedBlackTreeIterator key Propery declaration
 *      get iterator key
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return iterator key
 * @throws none
 **/
Object.defineProperty(RedBlackTreeIterator.prototype;, "key", {
  get: function() {
    if(this.stack.length > 0) {
      return this.stack[this.stack.length-1].key;
    }
    return;
  },
  enumerable: true;
});

/** 
 * RedBlackTreeIterator value Propery declaration
 *      get iterator value
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return iterator value
 * @throws none
 **/
Object.defineProperty(RedBlackTreeIterator.prototype;, "value", {
  get: function() {
    if(this.stack.length > 0) {
      return this.stack[this.stack.length-1].value;
    }
    return;
  },
  enumerable: true;
});

/** 
 * RedBlackTreeIterator index Propery declaration
 *      Returns the position of this iterator in the sorted list
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return iterator index
 * @throws none
 **/
Object.defineProperty(RedBlackTreeIterator.prototype;, "index", {
  get: function() {
    var idx = 0;
    var stack = this.stack;
    
    if(stack.length === 0) {
      var r = this.tree.root;
        
      if(r) {
        return r.count;
      }
      return 0
    } 
    else if(stack[stack.length - 1].left) {
      idx = stack[stack.length - 1].left.count;
    }
    for(var s = stack.length - 2; s >= 0; --s) {
      if(stack[s + 1] === stack[s].right) {
        ++idx;
        if(stack[s].left) {
          idx += stack[s].left.count;
        }
      }
    }
    return idx;
  },
  enumerable: true;
});

/** 
 * RedBlackTreeIterator hasNext Propery declaration
 *      Checks if iterator is at end of tree
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return iterator index
 * @throws none
 **/
Object.defineProperty(RedBlackTreeIterator.prototype;, "hasNext", {
  get: function() {
    
    var stack = this.stack;
    
    if(stack.length === 0) {
      return false;
    }
    
    if(stack[stack.length-1].right) {
      return true;
    }
    
    for(var s=stack.length-1; s>0; --s) {
      if(stack[s-1].left === stack[s]) {
        return true;
      }
        
    }
    return false;
  }
});

/*******************************************************
* RedBlackTree Methods Declerations
********************************************************/

/** 
 * RedBlackTree insert method
 *     insert new node into the tree
 * 
 * @param key value pair to be inserted
 *
 * @return new RedBlacktree
 * @throws none
 **/
RedBlackTree.prototype.insert = function(key, value) {
    
  var cmp = this.compare;
  var node = this.root;
  var nstack = [];
  var dstack = [];
    
  while(node) {
    var node2 = cmp(key, node.key);
      
    nstack.push(node);
    dstack.push(node2);
      
    if(d <= 0) {
      node = node.left;
    } 
    else {
      node = node.right;
    }
  }

  
  //Rebuild path to leaf node
  nstack.push(new Node(RED, key, value, null, null, 1))
  
  for(var i = nstack.length - 2; i >= 0; --i) {
    var node = nstack[i];
      
    if(dstack[i] <= 0) {
      nstack[i] = new Node(node.color, node.key, node.value, nstack[i + 1], node.right, node.count + 1);
    } 
    else {
      nstack[i] = new Node(node.color, node.key, node.value, node.left, nodestack[i + 1], node.count + 1);
    }
  }
    
  //Rebalance tree using rotations
  for(var i = nstack.length - 1; i > 1; --i) {
    var node = nstack[i];
    var node2 = nstack[i - 1];

    if(node2.color === BLACK || node.color === BLACK) {
      break;
    }
      
    var node3 = nstack[i - 2];
      
    if(node3.left === node2) {
      if(node2.left === node) {          
        var node4 = node3.right;
          
        if(node4 && node4.color === RED) {
          node2.color = BLACK;
            
          node3.right = repaint(BLACK, node4);
          node3.color = RED;
            
          i -= 1;
        } 
        else {
          node3.color = RED;
          node3.left = node2.right;
            
          node2.color = BLACK;
          node2.right = node3;
            
          nstack[i - 2] = node2;
          nstack[i - 1] = node;
            
          recount(node3);
          recount(node2);
            
          if(i >= 3) {
              node4 = nstack[i - 3];
              
            if(node4.left === node3) {
              node4.left = node2;
            } 
            else {
              node4.right = node2;
            }
          }
          break
        }
      } 
    else {
        var node4 = node3.right;
        
        if(node4 && node4.color === RED) {
          node2.color = BLACK;
            
          node3.right = repaint(BLACK, node4);
          node3.color = RED;
            
          i -= 1;
        } 
        else {
          node2.right = node.left;
            
          node3.color = RED;
          node3.left = node.right;
            
          node.color = BLACK;
          node.left = node2;
          node.right = node3;
            
          nstack[i - 2] = node;
          nstack[i - 1] = node2;
            
          recount(node3);
          recount(node2);
          recount(node);
            
          if(i >= 3) {
            node4 = nstack[i - 3];
              
            if(node4.left === node3) {
              node4.left = node;
            } 
            else {
              node4.right = node;
            }
          }
          break;
        }
      }
    } 
    else {
      if(node2.right === node) {
        var node4 = node3.left;
          
        if(node4 && node4.color === RED) {
          node2.color = BLACK;
            
          node3.left = repaint(BLACK, node4);
          node3.color = RED;
            
          i -= 1;
        } 
        else {
          node3.color = RED;
          node3.right = node4.left;
            
          node2.color = BLACK;
          node2.left = node3;
            
          nstack[i - 2] = node2;
          nstack[i - 1] = node;
            
          recount(node3);
          recount(node2);
            
          if(i >= 3) {
            node = nstack[i - 3];
              
            if(node4.right === node3) {
              node4.right = node2;
            } 
            else {
              node4.left = node2;
            }
          }
          break;
        }
      } 
      else {
        var node4 = node3.left;
          
        if(node4 && node4.color === RED) {
          node2.color = BLACK;
            
          node3.left = repaint(BLACK, node4);
          node3.color = RED;
            
          i -= 1;
        } 
        else {
          node2.left = node.right;
            
          node3.color = RED;
          node3.right = node.left;
            
          node.color = BLACK;
          node.right = node2;
          node.left = node3;
            
          nstack[i - 2] = node;
          nstack[i - 1] = node2;
            
          recount(node3);
          recount(node2);
          recount(node);
            
          if(i >= 3) {
            node4 = nstack[i - 3];
              
            if(node4.right === node3) {
              node4.right = node;
            } 
            else {
              node4.left = node;
            }
          }
          break;
        }
      }
    }
  }
    
  //Return new tree
  nstack[0].color = BLACK;
  return new RedBlackTree(cmp, nstack[0]);
};

/** 
 * RedBlackTree forEach method
 *     visit nodes in tree
 * 
 * @param visit, hi and lo node
 *
 * @return new RedBlacktree
 * @throws none
 **/
RedBlackTree.prototype.forEach = function rbTreeForEach(visit, lo, hi) {
  if(!this.root) {
    return;
  }
    
  switch(arguments.length) {
    case 1:
      return doVisitFull(visit, this.root);
    break;

    case 2:
      return doVisitHalf(lo, this.compare, visit, this.root);
    break;

    case 3:
      if(this.compare(lo, hi) >= 0) {
        return;
      }
      return doVisit(lo, hi, this.compare, visit, this.root);
    break;
  }
};

/** 
 * RedBlackTree at  method
 *     Find the ith item in the tree
 * 
 * @param index in tree
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
RedBlackTree.prototype.at = function(index) {
  if(index < 0) {
    return new RedBlackTreeIterator(this, [])
  }
  var node = this.root;
  var stack = [];
    
  while(true) {
    stack.push(node);
      
    if(node.left) {
      if(index < n.left.count) {
        node = node.left;
        continue;
      }
      index -= node.left.count;
    }
      
    if(!index) {
      return new RedBlackTreeIterator(this, stack);
    }
      
    index -= 1;
      
    if(n.right) {
      if(index >= node.right.count) {
        break;
      }
      node = node.right;
    } 
    else {
      break;
    }
  }
  return new RedBlackTreeIterator(this, []);
}

/** 
 * RedBlackTree ge method
 *      greater then equal to iterator by key 
 * 
 * @param key
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
RedBlackTree.prototype.ge = function(key) {
  var cmp = this.compare;
  var node = this.root;
  var stack = [];
  var last_ptr = 0;
    
  while(node) {
    var tmp = cmp(key, node.key);
    stack.push(node);
      
    if(tmp <= 0) {
      last_ptr = stack.length;
    }
      
    if(tmp <= 0) {
      node = node.left;
    } 
    else {
      node = node.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}

/** 
 * RedBlackTree gt method
 *     greater then iterator by key 
 * 
 * @param key
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
RedBlackTree.prototype.gt = function(key) {
  var cmp = this.compare;
  var node = this.root;
  var stack = [];
  var last_ptr = 0;
    
  while(node) {
    var tmp = cmp(key, node.key);
    stack.push(node);
      
    if(tmp < 0) {
      last_ptr = stack.length;
    }
      
    if(tmp < 0) {
      node = node.left;
    } 
    else {
      node = node.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}

/** 
 * RedBlackTree lt method
 *      less then iterator by key 
 * 
 * @param key
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
RedBlackTree.prototype.lt = function(key) {
  var cmp = this.compare;
  var node = this.root;
  var stack = [];
  var last_ptr = 0;
    
  while(node) {
    var tmp = cmp(key, node.key);
    stack.push(node);
      
    if(tmp > 0) {
      last_ptr = stack.length;
    }
    if(tmp <= 0) {
      node = node.left;
    } 
    else {
      node = node.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}

/** 
 * RedBlackTree le method
 *      less then equal to iterator by key 
 * 
 * @param key
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
RedBlackTree.prototype.le = function(key) {
  var cmp = this.compare;
  var node = this.root;
  var stack = [];
  var last_ptr = 0;
    
  while(node) {
    var tmp = cmp(key, node.key);
    stack.push(node);
      
    if(tmp >= 0) {
      last_ptr = stack.length;
    }
      
    if(tmp < 0) {
      node = node.left;
    } 
    else {
      node = node.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}

/** 
 * RedBlackTree find method
 *     Finds the item with key if it exists
 * 
 * @param key to find
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
RedBlackTree.prototype.find = function(key) {
  var cmp = this.compare;
  var node = this.root;
  var stack = [];
    
  while(node) {
    var tmp = cmp(key, node.key);
    stack.push(node);
      
    if(tmp === 0) {
      return new RedBlackTreeIterator(this, stack);
    }
      
    if(tmp <= 0) {
      node = node.left;
    } 
    else {
      node = node.right;
    }
  }
    
  return new RedBlackTreeIterator(this, []);
}

/** 
 * RedBlackTree remove method
 *      Removes item with key from tree
 * 
 * @param key to remove
 *
 * @return RedBlackTree
 * @throws none
 **/
RedBlackTree.prototype.remove = function(key) {
  var iter = this.find(key);
    
  if(iter) {
    return iter.remove();
  }
    
  return this;
}

/** 
 * RedBlackTree get method
 *       get item with key
 * 
 * @param key to get
 *
 * @return data
 * @throws none
 **/
RedBlackTree.prototype.get = function(key) {
  var cmp = this.compare;
  var node = this.root;
    
  while(node) {
    var tmp = cmp(key, node.key);
      
    if(tmp === 0) {
      return node.value;
    }
      
    if(tmp <= 0) {
      node = node.left;
    } 
    else {
      node = node.right;
    }
  }
    
  return;
}


/*******************************************************
* RedBlackTreeIterator Methods Declerations
********************************************************/

/** 
 * RedBlackTreeIterator clone method
 *      Makes a copy of an iterator
 * 
 * @param none
 *
 * @return RedBlackTreeIterator
 * @throws none
 **/
RedBlackTreeIterator.prototype.clone = function() {
  return new RedBlackTreeIterator(this.tree, this.stack.slice());
}

/** 
 * RedBlackTreeIterator remove method
 *      Removes item at iterator from tree
 * 
 * @param none
 *
 * @return RedBlackTree
 * @throws none
 **/
RedBlackTreeIterator.prototype.remove = function() {
  var stack = this.stack;
    
  if(stack.length === 0) {
    return this.tree;
  }
    
  //First copy path to node
  var cstack = new Array(stack.length);
  var node = stack[stack.length - 1];
  cstack[cstack.length - 1] = new Node(node.color, node.key, node.value, node.left, node.right, node.count);
    
  for(var i = stack.length - 2; i >= 0; --i) {
    var n = stack[i];
      
    if(n.left === stack[i + 1]) {
      cstack[i] = new Node(n.color, n.key, n.value, cstack[i+1], n.right, n.count);
    } 
    else {
      cstack[i] = new Node(n.color, n.key, n.value, n.left, cstack[i+1], n.count);
    }
  }

  //Get node
  node = cstack[cstack.length - 1];
  //start remove: n.value

  //If not leaf, then swap with previous node
  if(node.left && node.right) {
    //moving to leaf

    //First walk to previous leaf
    var split = cstack.length;
    node = node.left;
      
    while(node.right) {
      cstack.push(node);
      node = node.right;
    }
      
    //Copy path to leaf
    var v = cstack[split - 1];
    cstack.push(new Node(node.color, v.key, v.value, node.left, node.right, node.count));
    cstack[split - 1].key = node.key;
    cstack[split - 1].value = node.value;

    //Fix up stack
    for(var i = cstack.length - 2; i >= split; --i) {
      node = cstack[i];
      cstack[i] = new Node(node.color, node.key, node.value, node.left, cstack[i + 1], node.count);
    }
    cstack[split - 1].left = cstack[split];
  }

  //Remove leaf node
  node = cstack[cstack.length - 1];
    
  if(node.color === RED) {
    //Easy case: removing red leaf
    var p = cstack[cstack.length - 2];
      
    if(p.left === node) {
      p.left = null;
    } 
    else if(p.right === node) {
      p.right = null;
    }
      
    cstack.pop();
    for(var i = 0; i < cstack.length; ++i) {
      cstack[i].count--;
    }
    return new RedBlackTree(this.tree.compare, cstack[0]);
  } 
  else {
    if(node.left || node.right) {
      //Second easy case:  Single child black parent
      if(node.left) {
        swapNode(node, node.left);
      } 
      else if(node.right) {
        swapNode(node, node.right);
      }
        
      //Child must be red, so repaint it black to balance color
      node.color = BLACK
      
      for(var i = 0; i < cstack.length - 1; ++i) {
        cstack[i].count--;
      }
      return new RedBlackTree(this.tree.compare, cstack[0]);
    } 
    else if(cstack.length === 1) {
      //Third easy case: root
      return new RedBlackTree(this.tree.compare, null);
    } 
    else {
      //Hard case:  BLACK leaf no children
      for(var i = 0; i<cstack.length; ++i) {
        cstack[i].count--;
      }
        
      var parent = cstack[cstack.length - 2];
      fixDoubleBlack(cstack);
        
      //Fix up links
      if(parent.left === node) {
        parent.left = null;
      } 
      else {
        parent.right = null;
      }
    }
  }
  return new RedBlackTree(this.tree.compare, cstack[0]);
};

/** 
 * RedBlackTreeIterator next method
 *      Advances iterator to next element in list
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return node
 * @throws none
 **/
RedBlackTreeIterator.prototype;.next = function() {
  var stack = this.stack;
    
  if(stack.length === 0) {
    return;
  }
    
  var node = stack[stack.length-1];
    
  if(node.right) {
    node = node.right;
      
    while(node) {
      stack.push(node);
      node = node.left;
    }
  } 
  else {
    stack.pop();
      
    while(stack.length > 0 && stack[stack.length - 1].right === node) {
      node = stack[stack.length - 1];
      stack.pop();
    }
  }
};

/** 
 * RedBlackTreeIterator updae method 
 *      Update value
 * 
 * @param value
 *
 * @return RedBlackTree
 * @throws Error
 **/
RedBlackTreeIterator.prototype.update = function(value) {
  var stack = this.stack;
    
  if(stack.length === 0) {
    throw new Error("Can't update empty node!");
  }
    
  var cstack = new Array(stack.length);
  var node = stack[stack.length - 1];
    
  cstack[cstack.length - 1] = new Node(node.color, node.key, value, node.left, node.right, node.count);
    
  for(var i = stack.length - 2; i >= 0; --i) {
    node = stack[i];
      
    if(node.left === stack[i + 1]) {
      cstack[i] = new Node(node.color, node.key, node.value, cstack[i + 1], node.right, node.count);
    } 
    else {
      cstack[i] = new Node(node.color, node.key, node.value, node.left, cstack[i + 1], node.count);
    }
  }
  return new RedBlackTree(this.tree.compare, cstack[0]);
};

/** 
 * RedBlackTreeIterator prev method 
 *      Moves iterator backward one element
 * 
 * @param value
 *
 * @return RedBlackTree
 * @throws Error
 **/
RedBlackTreeIterator.prototype.prev = function() {
    
  var stack = this.stack;
    
  if(stack.length === 0) {
    return;
  }
    
  var node = stack[stack.length - 1];
    
  if(node.left) {
    node = node.left;
      
    while(node) {
      stack.push(node);
      node = node.right;
    }
      
  } 
  else {
    stack.pop();
      
    while(stack.length > 0 && stack[stack.length - 1].left === node) {
      node = stack[stack.length - 1];
      stack.pop();
    }
  }
};

/*******************************************************
* Helper Functions
********************************************************/

/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Swaps two nodes
function swapNode(n, v) {
  n.key = v.key;
  n.value = v.value;
  n.left = v.left;
  n.right = v.right;
  n.color = v.color;
  n.count = v.count;
}

/** 
 * Helper Function declaration
 *    Default comparison function
 *
 * @param 
 *
 * @return nodes to campare
 * @throws none
 **/
function defaultCompare(a, b) {
  if(a < b) {
    return - 1;
  }
  if(a > b) {
    return 1;
  }
  return 0;
}

/** 
 * Helper Function declaration
 *    Build a tree
 *
 * @param compare function
 *
 * @return RedBlackTree
 * @throws none
 **/
function createRBTree(compare) {
  return new RedBlackTree(compare || defaultCompare, null);
}

/** 
 * Helper Function declaration
 *     Visit all nodes inorder
 *
 * @param nodes to visit
 *
 * @return node
 * @throws none
 **/
function doVisitFull(visit, node) {
  if(node.left) {
    var v = doVisitFull(visit, node.left);
      
    if(v) {
        return v; 
    }
  }
    
  var v = visit(node.key, node.value);
    
  if(v) { 
      return v;
  }
    
  if(node.right) {
    return doVisitFull(visit, node.right);
  }
}

/** 
 * Helper Function declaration
 *      Visit half ns in order
 *
 * @param  lo node, compare funtion, visit, node
 *
 * @return visiting node
 * @throws none
 **/
function doVisitHalf(lo, compare, visit, n) {
  var l = compare(lo, n.key);
    
  if(l <= 0) {
    if(n.left) {
      var v = doVisitHalf(lo, compare, visit, n.left);
        
      if(v) { 
          return v;
      }
    }
      
    var v = visit(n.key, n.value);
      
    if(v) { 
        return v;
    }
  }
    
  if(n.right) {
    return doVisitHalf(lo, compare, visit, n.right);
  }
}

/** 
 * Helper Function declaration
 *      Visit all nodes within a range
 * 
 * @param hi node, low node, compare function, visit, curent node 
 *
 * @return node to visit
 * @throws none
 **/
function doVisit(lo, hi, compare, visit, node) {
  var l = compare(lo, node.key);
  var h = compare(hi, node.key);
  var v;
    
  if(l <= 0) {
    if(node.left) {
      v = doVisit(lo, hi, compare, visit, node.left);
        
      if(v) { 
          return v;
      }
    }
      
    if(h > 0) {
      v = visit(node.key, node.value);
        
      if(v) { 
          return v;
      }
    }
  }
  if(h > 0 && node.right) {
    return doVisit(lo, hi, compare, visit, node.right);
  }
}

/** 
 * Helper Function declaration
 *      Clone passed in node 
 *
 * @param  Node to be cloned
 *
 * @return New Node
 * @throws none
 **/
function cloneNode(node) {
  return new Node(node.color, node.key, node.value, node.left, node.right, node.count);
}

/** 
 * Helper Function declaration
 *      Clones passed in node with new color 
 * 
 * @param node to be cloned and repainted
 *
 * @return New Node
 * @throws none
 **/
function repaint(color, node) {
  return new Node(color, node.key, node.value, node.left, node.right, node.count);
}

/** 
 * Helper Function declaration
 *      get/set current count of node and subtrees 
 * 
 * @param Node to set count of
 *
 * @return none
 * @throws none
 **/
function recount(node) {
  node.count = 1 + (node.left ? node.left.count : 0) + (node.right ? node.right.count : 0);
}

/** 
 * Helper Function declaration
 *      fix double black in tree
 * 
 * @param stack of nodes 
 *
 * @return none
 * @throws none
 **/
function fixDoubleBlack(stack) {
  var n, p, s, z;
    
  for(var i = stack.length - 1; i >= 0; --i) {
    n = stack[i];
      
    if(i === 0) {
      n.color = BLACK;
      return;
    }
    //visit node:, n.key, i, stack[i].key, stack[i-1].key
    p = stack[i - 1];
      
    if(p.left === n) {
      //left child
      s = p.right;
        
      if(s.right && s.right.color === RED) {
        //case 1: right sibling child red
        s = p.right = cloneNode(s);
        z = s.right = cloneNode(s.right);
          
        p.right = s.left;
        s.left = p;
        s.right = z;
        s.color = p.color;
          
        n.color = BLACK;
        p.color = BLACK;
        z.color = BLACK;
          
        recount(p);
        recount(s);
          
        if(i > 1) {
          var pp = stack[i - 2];
            
          if(pp.left === p) {
            pp.left = s;
          } 
          else {
            pp.right = s;
          }
        }
        stack[i - 1] = s;
        return;
          
      } 
      else if(s.left && s.left.color === RED) {
        //case 1: left sibling child red
        s = p.right = cloneNode(s);
        z = s.left = cloneNode(s.left);
          
        p.right = z.left;
        s.left = z.right;
          
        z.left = p;
        z.right = s;
        z.color = p.color;
          
        p.color = BLACK;
        s.color = BLACK;
        n.color = BLACK;
          
        recount(p);
        recount(s);
        recount(z);
          
        if(i > 1) {
          var pp = stack[i - 2];
            
          if(pp.left === p) {
            pp.left = z;
          } 
          else {
            pp.right = z;
          }
        }
          
        stack[i - 1] = z;
        return
      }
        
      if(s.color === BLACK) {
        if(p.color === RED) {
          //case 2: black sibling, red parent, p.right.value
          p.color = BLACK;
          p.right = repaint(RED, s);
          return;
        } 
        else {
          //case 2: black sibling, black parent, p.right.value
          p.right = repaint(RED, s);
          continue;
        }
      } 
      else {
        //case 3: red sibling
        s = cloneNode(s);
        p.right = s.left;
        s.left = p;
        s.color = p.color;
        p.color = RED;
          
        recount(p);
        recount(s);
          
        if(i > 1) {
          var pp = stack[i - 2];
            
          if(pp.left === p) {
            pp.left = s;
          } 
          else {
            pp.right = s;
          }
        }
        stack[i - 1] = s;
        stack[i] = p;
          
        if(i+1 < stack.length) {
          stack[i + 1] = n;
        } 
        else {
          stack.push(n);
        }
        i = i + 2;
      }
    } 
    else {
      //right child
      s = p.left;
      if(s.left && s.left.color === RED) {
        //case 1: left sibling child red, p.value, p.color
        s = p.left = cloneNode(s);
        z = s.left = cloneNode(s.left);
          
        p.left = s.right;
        s.right = p;
        s.left = z;
        s.color = p.color;
          
        n.color = BLACK;
        p.color = BLACK;
        z.color = BLACK;
          
        recount(p);
        recount(s);
          
        if(i > 1) {
          var pp = stack[i - 2];
            
          if(pp.right === p) {
            pp.right = s;
          } 
          else {
            pp.left = s;
          }
        }
        stack[i - 1] = s;
        return;
          
      } 
      else if(s.right && s.right.color === RED) {
        //case 1: right sibling child red
        s = p.left = cloneNode(s);
        z = s.right = cloneNode(s.right);
          
        p.left = z.right;
        s.right = z.left;
        z.right = p;
        z.left = s;
        z.color = p.color;
          
        p.color = BLACK;
        s.color = BLACK;
        n.color = BLACK;
          
        recount(p);
        recount(s);
        recount(z);
          
        if(i > 1) {
          var pp = stack[i - 2];
          if(pp.right === p) {
            pp.right = z;
              
          } 
          else {
            pp.left = z;
          }
        }
        stack[i - 1] = z;
        return;
      }
      if(s.color === BLACK) {
        if(p.color === RED) {
          //case 2: black sibling, red parent
          p.color = BLACK;
          p.left = repaint(RED, s);
          return;
        } 
        else {
          //case 2: black sibling, black parent
          p.left = repaint(RED, s);
          continue;
        }
      } 
      else {
        //case 3: red sibling
        s = cloneNode(s);
        p.left = s.right;
        s.right = p;
        s.color = p.color;
        p.color = RED;
          
        recount(p);
        recount(s);
          
        if(i > 1) {
          var pp = stack[i - 2];
            
          if(pp.right === p) {
            pp.right = s;
          } 
          else {
            pp.left = s;
          }
        }
          
        stack[i - 1] = s;
        stack[i] = p;
          
        if(i + 1 < stack.length) {
          stack[i + 1] = n;
        } 
        else {
          stack.push(n);
        }
        i = i + 2;
      }
    }
  }
}
