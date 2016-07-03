
function Node(data){
    this.left = null;
    this.right = null;
    this.data = data; 
}

function BinaryTree(){
    this.root = null;
}

BinaryTree.prototype.insert = function(data){
    var node = new Node(data);

   if(!root){
      this.root = node;
      return;
   }
   var tmp = this.root;

   while(tmp){
      if(data < tmp.data){
          if(!tmp.left){
             tmp.left = node;
             break;
          }
          else{
             tmp = tmp.left;
        }
     }
     else{
         if(!tmp.right){
            tmp.right = node;
            break;
         }
         else{
            tmp = tmp.right;
         }
     }
  }


BinaryTree.prototype.remove = function (value) {
    this.root = this.remove(value, this.root);
};

BinaryTree.prototype.preOrderTraversal = function(node){
    if(node !== null){
        console.log(node.data);
        preOrderTraversal(node.left);
        preOrderTraversal(node.right);
    }
}

BinaryTree.prototype.postOrderTraversal = function(node){
    if(node != null){
        postOrderTraversal(node.left);
        postOrderTraversal(node.right);
        console.log(node.data);
    }
}

BinaryTree.prototype.inOrderTraversal = function(data){
    if(node != null){
        inOrderTraversal(node.left);
        console.log(node.data);
        inOrderTraversal(node.right);
    }
}

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
}
	

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
}
