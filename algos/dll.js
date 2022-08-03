class Node {
    constructor(value) {
        this.data = value;
        this.next = null;
        this.previous = null;
    }
}

class DLL {
    constructor() {
        this.head = null;
        this.tail = null;
        this.length = 0;
    }

    isEmpty() {
        return this.head == null;
    }

    print() {
        //create runner that starts at head
        let runner = this.head;
        //create array that will house all of the data
        let sllArr = [];
        //create while loop that will continue to push each node's data value into the array until we reach the node where next is null
        while (runner){
            sllArr.push(runner.data);
            runner = runner.next;
        }
        //log the final array to the console
        console.log(sllArr);
        return this;
    }

    // Insert at Front
    // Insert a given value to the front of the doubly linked list
    InsertAtFront(val) {
        if(this.isEmpty()) {
            const newNode = new Node(val);
            this.head = newNode;
            this.tail = newNode;
            this.length++;
            return this;
        }

        const newNode = new Node(val);
        newNode.next = this.head;
        this.head.previous = newNode;
        this.head = newNode;
        this.length ++;

        return this;
    }

    // Insert at Back
    // Insert a given value to the back of the doubly linked list
    InsertAtBack(val) {
        if(this.isEmpty()){
            const newNode = new Node(val);
            this.head = newNode;
            this.tail = newNode;
            this.length++;
            return this
        }
        const newNode = new Node(val);
        newNode.previous =this.tail;
        this.tail.next = newNode;
        this.tail = newNode;
        this.length++;
        return this;
    }

    // Remove Middle Node
    // Remove the node in the middle of your doubly linked list
    RemoveMiddle() {
        if(this.isEmpty()){
            return this;
        }
        let runner = this.head;

        for(var i=0; i<Math.floor(this.length/2); i++){
            runner = runner.next;
        }
        runner.previous.next = runner.next;
        runner.next.previous = runner.previous;

        return this;
    }

    InserAfter(nod,val){
        if(this.isEmpty()){
            return console.log("Value ${val} could not be found")
        }
        let runner = this.head;

        while(runner.data != nod){
            if(runner == this.tail){
                return console.log("Value ${val} could not be found")
            }
            runner = runner.next
        }

        if(runner == this.tail){
            this.InsertAtBack(val)
            return this;
        }

        const newNode = new Node(val);
        newNode.previous = runner;
        newNode.next = runner.next;
        runner.next.previous = newNode;
        runner.next = newNode;
        this.length++
        return this;
    }

    InsertBefore(nod,val){
        if(this.isEmpty()){
            return console.log("Value ${val} could not be found")
        }
        let runner = this.head;

        while(runner.data != nod){
            if(runner == this.tail){
                return console.log("Value ${val} could not be found")
            }
            runner = runner.next
        }

        const newNode = new Node(val);

        if(this.head.data == nod){
            this.head.previous = newNode;
            newNode.next = this.head;
            this.head = newNode;
            this.length++;
            return this;
        }

        newNode.next = runner;
        newNode.previous = runner.previous;
        runner.previous.next = newNode;
        runner.previous = newNode;
        this.length++;
        return this
    }

    reverse(){
    const revdll = new DLL();
    let runner = this.tail;
    while(runner){
        revdll.InsertAtBack(runner.data);
        runner = runner.previous;
        }
    return revdll;
    }
}
const dll = new DLL();
// dll.InsertAtFront(1).InsertAtFront(2).InsertAtFront(3).InsertAtFront(4).InsertAtFront(5);

// dll.print();

dll.InsertAtBack(1).InsertAtBack(2).InsertAtBack(3).InsertAtBack(4).InsertAtBack(5);
dll.print();

dll.RemoveMiddle();
dll.print();

dll.InserAfter(2,3)
dll.print();

dll.InsertBefore(2,9)
dll.print();

dll.reverse().print()

