
function Node(data){
    this.parent = null;
    this.left = null;
    this.right = null;
    this.data = data; 
}

function BinaryTree(){
    this.root = null;
    
    var compare = function(data, node){
        if(node == null || data == null)
            return null;
        
        if(node.data > data){
            return 1;
        }
        
        if(node.data < data){
            return -1;
        }
        
        if(node.data == data){
            return 0;
        }
        return null;
    };
    
    var insertHelper = function(data, node){
        var cmp = this.compare(data, node);
        if(cmp !- null){
            if(cmp == 1){
                if(!node.right){
                    newNode = new Node(data);
                    node.right = newNode;
                    newNode.parent = node;
                    return newNode;
                }
                else
                    return insertHelper(data, node.right);
            }
            if(cmp <= 0){
                if(!node.left){
                    newNode = new Node(data);
                    node.left = newNode;
                    newNode.parent = node;
                    return newNode;
                }
                else
                    return insertHelper(data, node.left);
            }
        }
        else
            return null;
    };
    
    var removeHelper = function(data, node){
        var cmp = this.compare(data, node);
        if(cmp !- null){
            if(cmp == 1)
                return removeHelper(data, node.right);
            if(cmp == -1)
                return removeHelper(data, node.left);
            if(cmp == 0){
                //has no children
                if(!node.left && !node.right){
                    pnode = node.parent;
                    if(pnode.left)
                }
                //has one child
                
                //has 2 children
                
            }

        }
        else
            return null;
    };
    
}

BinaryTree.prototype.insert = function(data){
    if(!this.root){
       return this.root = new Node(data);
    }
    return insertHelper(data, this.root);
};


BinaryTree.prototype.remove = function (data) {
    if(!this.root)
        return null;
    return this.removeHelper(data, this.root);

};
    
BinaryTree.prototype.getMin = function(){
    if(!this.root)
        return null;
    
    node = this.root;
    
    while(nide.left)
        node = node.left;
    
    return node;
};
    
BinaryTree.prototype.getMax = function(){
    if(!this.root)
        return null;
    
    node = this.root;
    
    while(nide.right)
        node = node.right;
    
    return node;
};


BinaryTree.prototype.preOrderTraversal = function(node){
    if(node !== null){
        console.log(node.data);
        preOrderTraversal(node.left);
        preOrderTraversal(node.right);
    }
};

BinaryTree.prototype.postOrderTraversal = function(node){
    if(node != null){
        postOrderTraversal(node.left);
        postOrderTraversal(node.right);
        console.log(node.data);
    }
};

BinaryTree.prototype.inOrderTraversal = function(data){
    if(node != null){
        inOrderTraversal(node.left);
        console.log(node.data);
        inOrderTraversal(node.right);
    }
};

BinaryTree.prototype.depthFirstSearch = function(node){
		if(node == null)
            return;

        var stack = [];
        stack.push(node);
        
		while (stack.length > 0) {
			console.log(stack.pop());
			if(x.right!=null) 
                stack.push(node.right);
			if(x.left!=null) 
                stack.push(node.left);			
		}
};
	

BinaryTree.prototype.breadthFirstSearch = function(node){
    if(node == null)
        return;

    var queue = [];
    queue.push(node);

    while (stack.length > 0) {
        console.log((node = queue.shift()));
        if(node.right != null) 
            queue.push(node.left);			
        if(node.left != null) 
            queue.push(node.right);

    }
};
