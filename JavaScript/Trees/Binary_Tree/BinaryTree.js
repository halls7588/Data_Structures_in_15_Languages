
function Node(data){
    this.parent = null;
    this.left = null;
    this.right = null;
    this.data = data; 
}

function BinaryTree(){
    this.root = null;
}

BinaryTree.prototype.insert = function(data){
    
    
    if(!this.root){
       return (this.root = new node);
    }
    
    var node = new Node(data);
    var tnp = this.root;
    
    while(tmp){
        if(node.data > tmp.data){
            if(tmp.right == null){
                tmp.right = node;
                return node;
            }
            else{
                tmp = tmp.right;
            }
        }
        else if(node.data <= tmp.data){
            if(tmp.left == null){
                tmp.left = node;
                return node;
            }
            else{
                tmp = tmp..left;
            }
        }
    }
    
};


BinaryTree.prototype.remove = function (data) {
    if(!this.root)
        return null;
    
    // is root
    
    // has no child
    
    // has one child
    
    // has two children
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
			console.log(node = stack.pop());
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
        if(node.left != null) 
            queue.push(node.left);			
        if(node.right != null) 
            queue.push(node.right);

    }
};
