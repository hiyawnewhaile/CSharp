// Our Node and SLL classes

//We need a Node class  
class Node{
    //we oass in a  value everytime we create a node
    constructor(value){
        //that value is passed into dta     
        this.data = value;
        //we cant assume this node has anthing to point to so it starts at null
        this.next = null;
    }
}

//Our singly linked list class
class SLL{
    //this creates a list with nothing inside it
    constructor(){
        this.head = null;
    }
    // We are goiny yo be writing methods that make this class function
    // How do we indentify that as a singly linked  is empty?
    isEmpty(){
        //if the head is pointing a null there is nothing in our sll
        if(this.head == null){
            //yes, the sll is empty
            return true
        }
        else{
            return false;
        }
        // a more efficient way of writting the question
        //return this.head == null;
    }
    print(){
        //print out all the values in our singly linked list
        //we want to push all the values into an array and print that array
        var arr = [];
        var runner = this.head;
        // We need to use .push to push in the data of the node
        // arr.push(runner.data);
        // we want to keep going until we hit null
        // we dont know how many times we're going to run so a while loop is the optimal solution
        while(runner){
            //we need to add the data to the array
            arr.push(runner.data);
            //we NEED to iterate
            runner = runner.next;
        }
        console.log(arr)
    }

    insertAtBack(val){
        if(this.isEmpty()){
            this.head = new Node(val);
        }
        else{
            var runner = this.head;
        }
        // We need to get to the  back
        while(runner.next){
            runner = runner.next;
        }
        runner.next = new Node(val);
    }

    // Given a value, insert that value as a node at the front of your singly linked list
    insertAtFront(val) {
        var newnode = new Node(val);
        newnode.next = this.head;
        this.head = newnode;
    }

    // Remove and return the head node from your list (remember this means we need a new head)
    removeHead(){
        var current = this.head
        this.head = current.next
    }

    // EXTRA: calculate the average of all the values in your list (ex: if you list contained the values 3, 5, 2, 7, 3, then your average should come out as 4)
    average(){
        let runner = this.head
        let length = 0
        let sum = 0

        while(runner){
            length++
            sum += runner.data
            runner = runner.next
        }
        console.log(sum/length)
    }
    contains(val){
        let runner = this.head;
        while (runner){
            if(runner.data === val){
                return true;
            }
            runner = runner.next;
        }
        return false;
    }

    containsRecursive(value, runner = this.head){
        //check if the runner data is equal to value. If so, return true.
        if(runner.data == value){
            return true;
        }
        //next check to make sure the next runner exists. If not, we're at end of list and need to return false
        if(!runner.next){
            return false;
        }
        //if we make it this far, assign runner to the next node and plug back into the function
        return this.containsRecursive(value, runner.next);
    }

    removeback(){
        let runner = this.head;
        if(this.isEmpty()){
            return "There are no nodes in this list"
        }
        if(!runner.next){
            let temp = runner;
            this.removeHead();
            return temp;
        }
        while(runner.next.next){
            runner = runner.next;
        }
        let temp = new Node(runner.next.data)
        runner.next = null;
        return temp
    }
    secondtolast(){
        let runner = this.head

        if(this.isEmpty()){
            return null
        }
        if(!runner.next){
            return this.head.data
        }
        while(runner.next.next){
            runner = runner.next
        }
        return runner.data
    }

    remove(val){
        let runner = this.head
        if(this.isEmpty()){
            return "No data in List"
        }
        if(this.head.data == val){
            this.removeHead()
            return true
        }
        
        while(runner){
            if(runner.next == null){
                return false
            }
            if(runner.next.data == val){
                runner.next = runner.next.next
                return true
            }
            runner = runner.next
        }
    }

    removeDupeValue(val){
        var runner = this.head;
        let count = 0;
        let previous;
        
        while (runner){
            if (this.head.data == val){
                this.removeHead();
                runner = this.head;
                count++;
                continue;
            }
            if (runner.data == val){
                previous.next = runner.next;
                this.size--;
                count++;
            }

            previous = runner;
            runner = runner.next;
        }
        if (count > 0){
            return true;
        } else {
            return false;
        }
    }

    prepend(ValA, ValB) {
        let runner = this.head;
        if(runner.data == ValB){
            this.insertAtFront(ValA);
            return true;
        }
        if(this.isEmpty()){
            return false;
        }
        while(runner.next){
            if(runner.next.data == ValB){
                break;
            }
            runner = runner.next;
        }
        if(!runner.next){
            return false;
        }
        let temp = runner.next;
        runner.next = new Node(ValA, temp);
        return true;
    }

    addlist(list){
        var runner = list.head
        while(runner){
            this.insertAtBack(runner.data)
            runner = runner.next
        }
        return this
    }

    smallestfirst(){
        var runner = this.head
        let x = this.head.data
        while(runner){
            if (runner.data < x){
                x = runner.data
            }
            runner = runner.next
        }
        this.remove(x)
        this.insertAtFront(x)
        return x
    }

    reverse(){
        let runner = this.head;
        const revsll = new SLL();

        while(runner){
            revsll.insertAtFront(runner.data);
            runner = runner.next
        }
        return revsll;
    }

    hasloop(){
        let runner = this.head;
        let counter = 0;
        while(runner){
            counter ++;
            runner = runner.next;
            if(counter > this.length){
                return true;
            }
        }
        return false;
    }

    removenegative(){
        if(this.isEmpty){
            return "is Empty";
        }
        let runner = this.head;
        let prevrunner = this.head;

        while(runner){
            if(runner.data <0){
                this.remove(runner.data);
            }
            runner = runner.next;
        }
        return this;
    }
}

var sll = new SLL();
var node1 = new Node(5);
var node2 = new Node(7);
var node3 = new Node(9);
var node4 = new Node(1);

var sll2 = new SLL();
var node5 = new Node(1);



console.log(sll.isEmpty());
sll.head = node1;
console.log(sll.isEmpty());

sll2.head = node5
sll2.insertAtBack(2)
sll2.insertAtBack(3)
sll2.insertAtBack(4)
// Remeber the head is a pointer
// The pointer is pointing at a node
// that node has a data and a nect value
// So when we represent the node as head we are able to grab its attribute
// sll.head.next = node2;
// sll.head.next.next = node3;
// sll.head.next.next.next = node4;
sll.insertAtBack(1)
sll.insertAtBack(7)
sll.insertAtBack(9)
sll.insertAtBack(8)
sll.insertAtBack(1)
sll.insertAtFront(9)
sll.insertAtFront(10)
sll.removeHead()
sll.average()
console.log(sll.contains(3))
console.log(sll.contains(1))
console.log(sll.containsRecursive(3))
console.log(sll.containsRecursive(1))
console.log(sll.removeback())



// console.log(sll)
sll.print()

console.log(sll.secondtolast())

sll.print()

console.log(sll.remove(8))

sll.print()

console.log(sll.addlist(sll2))

sll.print()

console.log(sll.smallestfirst())

sll.print()












