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
//---------------------------------------------------------------------------------------------------------------
/** 
 * RedBlackTree forEach method
 *     visit nodes in tree
 * 
 * @param key value pair to be inserted
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
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Find the ith item in the tree
RedBlackTree.prototype.at = function(idx) {
  if(idx < 0) {
    return new RedBlackTreeIterator(this, [])
  }
  var n = this.root;
  var stack = [];
  while(true) {
    stack.push(n);
    if(n.left) {
      if(idx < n.left.count) {
        n = n.left;
        continue;
      }
      idx -= n.left.count;
    }
    if(!idx) {
      return new RedBlackTreeIterator(this, stack);
    }
    idx -= 1;
    if(n.right) {
      if(idx >= n.right.count) {
        break;
      }
      n = n.right;
    } else {
      break;
    }
  }
  return new RedBlackTreeIterator(this, []);
}

/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
RedBlackTree.prototype.ge = function(key) {
  var cmp = this.compare;
  var n = this.root;
  var stack = [];
  var last_ptr = 0;
  while(n) {
    var d = cmp(key, n.key);
    stack.push(n);
    if(d <= 0) {
      last_ptr = stack.length;
    }
    if(d <= 0) {
      n = n.left;
    } else {
      n = n.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}

/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
RedBlackTree.prototype.gt = function(key) {
  var cmp = this.compare;
  var n = this.root;
  var stack = [];
  var last_ptr = 0;
  while(n) {
    var d = cmp(key, n.key);
    stack.push(n);
    if(d < 0) {
      last_ptr = stack.length;
    }
    if(d < 0) {
      n = n.left;
    } else {
      n = n.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}

/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
RedBlackTree.prototype.lt = function(key) {
  var cmp = this.compare;
  var n = this.root;
  var stack = [];
  var last_ptr = 0;
  while(n) {
    var d = cmp(key, n.key);
    stack.push(n);
    if(d > 0) {
      last_ptr = stack.length;
    }
    if(d <= 0) {
      n = n.left;
    } else {
      n = n.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}

/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
RedBlackTree.prototype.le = function(key) {
  var cmp = this.compare;
  var n = this.root;
  var stack = [];
  var last_ptr = 0;
  while(n) {
    var d = cmp(key, n.key);
    stack.push(n);
    if(d >= 0) {
      last_ptr = stack.length;
    }
    if(d < 0) {
      n = n.left;
    } else {
      n = n.right;
    }
  }
  stack.length = last_ptr;
  return new RedBlackTreeIterator(this, stack);
}
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Finds the item with key if it exists
RedBlackTree.prototype.find = function(key) {
  var cmp = this.compare;
  var n = this.root;
  var stack = [];
    
  while(n) {
    var d = cmp(key, n.key);
    stack.push(n);
      
    if(d === 0) {
      return new RedBlackTreeIterator(this, stack);
    }
      
    if(d <= 0) {
      n = n.left;
    } 
    else {
      n = n.right;
    }
  }
    
  return new RedBlackTreeIterator(this, []);
}
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Removes item with key from tree
RedBlackTree.prototype.remove = function(key) {
  var iter = this.find(key);
    
  if(iter) {
    return iter.remove();
  }
    
  return this;
}
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Returns the item at `key`
RedBlackTree.prototype.get = function(key) {
  var cmp = this.compare;
  var n = this.root;
    
  while(n) {
    var d = cmp(key, n.key);
      
    if(d === 0) {
      return n.value;
    }
      
    if(d <= 0) {
      n = n.left;
    } 
    else {
      n = n.right;
    }
  }
    
  return;
}


/*******************************************************
* RedBlackTree Methods Declerations
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
//Makes a copy of an iterator
RedBlackTreeIterator.prototype;.clone = function() {
  return new RedBlackTreeIterator(this.tree, this.stack.slice());
}
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Removes item at iterator from tree
RedBlackTreeIterator.prototype;.remove = function() {
  var stack = this.stack;
  if(stack.length === 0) {
    return this.tree;
  }
  //First copy path to node
  var cstack = new Array(stack.length);
  var n = stack[stack.length-1];
  cstack[cstack.length-1] = new Node(n.color, n.key, n.value, n.left, n.right, n.count);
  for(var i=stack.length-2; i>=0; --i) {
    var n = stack[i];
    if(n.left === stack[i+1]) {
      cstack[i] = new Node(n.color, n.key, n.value, cstack[i+1], n.right, n.count);
    } else {
      cstack[i] = new Node(n.color, n.key, n.value, n.left, cstack[i+1], n.count);
    }
  }

  //Get node
  n = cstack[cstack.length-1];
  //console.log("start remove: ", n.value)

  //If not leaf, then swap with previous node
  if(n.left && n.right) {
    //console.log("moving to leaf")

    //First walk to previous leaf
    var split = cstack.length;
    n = n.left;
    while(n.right) {
      cstack.push(n);
      n = n.right;
    }
    //Copy path to leaf
    var v = cstack[split-1];
    cstack.push(new Node(n.color, v.key, v.value, n.left, n.right, n.count));
    cstack[split-1].key = n.key;
    cstack[split-1].value = n.value;

    //Fix up stack
    for(var i=cstack.length-2; i>=split; --i) {
      n = cstack[i];
      cstack[i] = new Node(n.color, n.key, n.value, n.left, cstack[i+1], n.count);
    }
    cstack[split-1].left = cstack[split];
  }
  //console.log("stack=", cstack.map(function(v) { return v.value }))

  //Remove leaf node
  n = cstack[cstack.length-1];
  if(n.color === RED) {
    //Easy case: removing red leaf
    //console.log("RED leaf")
    var p = cstack[cstack.length-2];
    if(p.left === n) {
      p.left = null;
    } else if(p.right === n) {
      p.right = null;
    }
    cstack.pop();
    for(var i=0; i<cstack.length; ++i) {
      cstack[i].count--;
    }
    return new RedBlackTree(this.tree.compare, cstack[0]);
  } else {
    if(n.left || n.right) {
      //Second easy case:  Single child black parent
      //console.log("BLACK single child")
      if(n.left) {
        swapNode(n, n.left);
      } else if(n.right) {
        swapNode(n, n.right);
      }
      //Child must be red, so repaint it black to balance color
      n.color = BLACK
      for(var i=0; i<cstack.length-1; ++i) {
        cstack[i].count--;
      }
      return new RedBlackTree(this.tree.compare, cstack[0]);
    } else if(cstack.length === 1) {
      //Third easy case: root
      //console.log("ROOT")
      return new RedBlackTree(this.tree.compare, null);
    } else {
      //Hard case: Repaint n, and then do some nasty stuff
      //console.log("BLACK leaf no children")
      for(var i=0; i<cstack.length; ++i) {
        cstack[i].count--;
      }
      var parent = cstack[cstack.length-2];
      fixDoubleBlack(cstack);
      //Fix up links
      if(parent.left === n) {
        parent.left = null;
      } else {
        parent.right = null;
      }
    }
  }
  return new RedBlackTree(this.tree.compare, cstack[0]);
};
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Advances iterator to next element in list
RedBlackTreeIterator.prototype;.next = function() {
  var stack = this.stack;
  if(stack.length === 0) {
    return;
  }
  var n = stack[stack.length-1]
  if(n.right) {
    n = n.right;
    while(n) {
      stack.push(n);
      n = n.left;
    }
  } else {
    stack.pop();
    while(stack.length > 0 && stack[stack.length-1].right === n) {
      n = stack[stack.length-1];
      stack.pop();
    }
  }
};
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Update value
RedBlackTreeIterator.prototype;.update = function(value) {
  var stack = this.stack;
  if(stack.length === 0) {
    throw new Error("Can't update empty node!");
  }
  var cstack = new Array(stack.length);
  var n = stack[stack.length-1];
    
  cstack[cstack.length-1] = new Node(n.color, n.key, value, n.left, n.right, n.count);
    
  for(var i=stack.length-2; i>=0; --i) {
    n = stack[i];
      
    if(n.left === stack[i+1]) {
      cstack[i] = new Node(n.color, n.key, n.value, cstack[i+1], n.right, n.count);
    } 
    else {
      cstack[i] = new Node(n.color, n.key, n.value, n.left, cstack[i+1], n.count);
    }
  }
  return new RedBlackTree(this.tree.compare, cstack[0]);
};
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Moves iterator backward one element
RedBlackTreeIterator.prototype;.prev = function() {
    
  var stack = this.stack;
    
  if(stack.length === 0) {
    return;
  }
    
  var n = stack[stack.length-1];
    
  if(n.left) {
    n = n.left;
      
    while(n) {
      stack.push(n);
      n = n.right;
    }
      
  } 
  else {
    stack.pop();
      
    while(stack.length > 0 && stack[stack.length-1].left === n) {
      n = stack[stack.length-1];
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
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Default comparison function
function defaultCompare(a, b) {
  if(a < b) {
    return -1;
  }
  if(a > b) {
    return 1;
  }
  return 0;
}
/** 
 * RedBlackTreeIterator valid Propery declaration
 *      determin if iterator is valid
 * 
 * @param Object to define property on, namw of property, function description
 *
 * @return if iterator is valid
 * @throws none
 **/
//Build a tree
function createRBTree(compare) {
  return new RedBlackTree(compare || defaultCompare, null);
}

/** 
 * Helper Function declaration
 *     Visit all nodes inorder
 *
 * @param 
 *
 * @return visit node
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
