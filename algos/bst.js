// Binary search Tree

// Nodes
class Node{
    constructor(val){
        this.data = val;
        // All smaller values go to the left 
        this.left = null;
        // All larger values go to the rogth
        this.right = null;
    }
}

class BST {
    constructor(){
        // This is the same as SLLs head ponter
        // All trees start at the root
        this.root = null
    }

    //Is our tree empty?
    isEmpty(){
        return this.root == null;
    }

    //we cam find the min very quickly
    min(){
        //Start at the root
        var runner = this.root;
        //keep going left untill we find null
        while(runner.left){
            runner = runner.left;
        }
        return runner.data;
        // 
    }

    max(){
        //Start at the root
        var runner = this.root;
        //keep going right untill we find null
        while(runner.right){
            runner = runner.right;
        }
        return runner.data;
        // 

    }
    contains(val){
        var runner = this.root;
        while(runner){
            if(val == runner.data){
                return true;
            }
            if(val < runner.data){
                runner = runner.left;
            }
            if(val > runner.data){
                runner = runner.right;
            }
        }
        return false;
    }

    reccontains(val, runner = this.root){
        if(val == runner.data){
            return true;
        }
        if(!runner.left && !runner.right){
            return false;
        }
        if(val < runner.data){
            return this.reccontains(val, runner.left);
        }
        else if(val > runner.data){
            return this.reccontains(val, runner.right);
        }
    }

    range(){
        return this.max() - this.min();
    }

    insert(val){
        var runner = this.root;
        if(this.isEmpty()){
            this.root = new Node(val);
        }
        while(runner){
            if(runner.data == val){
                console.log("Already there")
                return this
            }
            if(runner.data > val){
                if(runner.left == null){
                    runner.left = new Node(val);
                    return this
                }
                runner = runner.left
            }
            if(runner.data < val){
                if(runner.right == null){
                    runner.right = new Node(val);
                    return this
                }
                runner = runner.right
            }
        }
    }

    recursiveInsert(val, runner = this.root, prevRunner = this.root) {
        if (this.isEmpty()){
            this.root = new Node(val);
            return this;
        }

        if(runner) {
            if(val > runner.data){
                return this.recursiveInsert(val, runner.right, prevRunner = runner);
            } else {
                return this.recursiveInsert(val, runner.left, prevRunner = runner);
            }
        }

        if(val > prevRunner.data){
            prevRunner.right = new Node(val);
        } else {
            prevRunner.left = new Node(val);
        }
        
        return this;
    }

    size(runner = this.root){
        if(runner == null){
            return 0
        }
        return 1 + this.size(runner.left) + this.size(runner.right);
    }

    height(node = this.root){
        if(!node){
        return 0;
        }
        let leftHeight = this.height(node.left);
        let rightHeight = this.height(node.right);
        if(leftHeight > rightHeight)
        {
        return leftHeight + 1
        } else {
        return rightHeight + 1;
        }
    }

    isFull(node = this.root) {
        // If empty tree
        if (!node) {
        return false;
        }
    
        // if leaf node, leaf node is the end which means it has no left or right
        if (!node.left && !node.right) {
        return true;
        }
    
        // if both left and right subtrees are not null and
        // both left and right subtrees are full
        if (node.left && node.right) {
        return this.isFull(node.left) && this.isFull(node.right);
        }
        return false;
    }

    dfspreorder(){
        var values = [];
        var runner = this.root;
        values.push(runner.data);
        let prevrunner;
        while(runner){
            prevrunner = runner;
            if(runner.left){
                runner = runner.left;
                values.push(runner.data)
            }
            else if(prevrunner.right){
                runner = runner.right;
                values.push(runner.data)
            }
            else{
                runner = runner.right;
            }
        }
        return values;
    }
}

var myBST = new BST();
console.log("Is my tree empty")
console.log(myBST.isEmpty())
var node1 = new Node(30);
myBST.root = node1;
console.log("Is my tree empty")
console.log(myBST.isEmpty())
console.log(myBST);
var node2 = new Node(20);
var node3 = new Node(40);
var node4 = new Node(10);
var node5 = new Node(50);

myBST.root.left = node2;
myBST.root.right = node3;
myBST.root.left.left = node4;
myBST.root.right.right = node5;

console.log("Our min value is: " + myBST.min());
console.log("Our max value is: " + myBST.max());

console.log("The range is " + myBST.range())

console.log(myBST.contains(60))
console.log(myBST.contains(40))

console.log(myBST.reccontains(60))
console.log(myBST.reccontains(40))

myBST.insert(9)
myBST.insert(51)


console.log("Our min value is: " + myBST.min());
console.log("Our max value is: " + myBST.max());

console.log(myBST.insert(50))

console.log(myBST.size())

console.log(myBST.dfspreorder());







