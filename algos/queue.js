// Queue - FIFO = First In First Out

class Node
{
    constructor(val)
    {
        this.data=val;
        this.next=null;
    }
}

class Queue
{
    constructor()
    {
        this.top = null;
        // We keep track of a tail because it's how we add to the queue
        this.tail = null;
        this.length = 0;
    }

    // Push a Value into Queue (at the back) - Enqueue
    enqueue(val)
    {
        var node = new Node(val);
        if (this.isEmpty())
        {
            this.top = node;
            this.tail = node;
            this.length++
            return this;
        }
        this.tail.next = node;
        this.tail = node;
        this.length++
        return this;
    }

    // Pop a Value (remove from front) - Dequeue
    dequeue()
    {
        if (this.isEmpty()){
            return;
        }
        this.top = this.top.next;
        this.length--
        return this;
    }

    // IsEmpty (return true/false)
    isEmpty()
    {
        if (!this.top){
            return true;
        }
        return false;
    }

    // Front (Does NOT remove the value)
    front()
    {
        if (this.isEmpty()){
            return "The Queue is Empty";
        }
        return this.top.data;
    }

    log()
    {
        let str="";
        for(let node=this.top;node;node=node.next)
        {
            str+=node.data+"->";
        }
        console.log(str);
    }

    // Compare 2 Queues - Using Built In Methods We Already Have (enqueue, dequeue, isEmpty, front)
    compare(otherQ)
    {
        if(this.length !== otherQ.length){
            return false;
        }
        let tempQ = otherQ;
        let runner = this .top;
        while(runner){
            if(runner.data == tempQ.front()){
                tempQ.enqueue(tempQ.front())
                tempQ.dequeue();
                runner = runner.next;
            }
            else{
                tempQ.enqueue(tempQ.front())
                tempQ.dequeue();
                return false
            }
        }
        return true;
    }

    // Is this Queue a Palindrome - Using Built In Methods We Already Have (enqueue, dequeue, isEmpty, front)
    isPalindrome()
    {
        
    }

    SumOfHalves()
    {
        if(this.isEmpty()){
            return console.log("Queue is empty")
        }
        let count = Math.floor(this.length/2);
        let sum1 = 0;
        let sum2 = 0;

        for(var i=0; i<count; i++){
            this.enqueue(this.front())
            sum1 += this.front()
            this.dequeue()
        }

        if(this.length%2 !=0){
            this.enqueue(this.front())
            this.dequeue()
        }

        for(var i=0; i<count; i++){
            this.enqueue(this.front())
            sum2 += this.front()
            this.dequeue()
        }
        return sum1 == sum2;
    }
}

const q1= new Queue();
const q2= new Queue();

q1.enqueue(1).enqueue(2).enqueue(3).enqueue(3).enqueue(2).enqueue(1);
q1.log();
console.log(q1.SumOfHalves())
// q2.enqueue(1).enqueue(2).enqueue(3);
// q1.log();
// console.log(q1.compare(q2));

