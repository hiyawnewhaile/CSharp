// Piriority queue
// Use the new classes priorityNode and priorityQueue to create an enqueue method that takes priority value into consideration to properly place the item within the list

class PriNode {
    constructor(value, pri) {
        this.data = value;
        this.next = null;
        this.priority = pri;
    }
}

class PriQueue {
    constructor() {
        this.head = null;
        this.tail = null;
    }

    log()
    {
        let str="";
        for(let node=this.head;node;node=node.next)
        {
            str+="("+node.data + "|" + node.priority + ")" + "->";
        }
        console.log(str);
    }

    enqueue(val, pri) {
        let newNode = new PriNode(val, pri);
        if(!this.head){
            this.head = newNode;
            this.tail = this.head;
            return this;
        }

        if(newNode.priority < this.head.priority){
            newNode.next = this.head;
            this.head = newNode;
            return this;
        }

        let runner = this.head;
        while(runner.next){
            if(newNode.priority < runner.next.priority){
                newNode.next = runner.next;
                runner.next = newNode;
                return this;
            }
            runner = runner.next;
        }

        runner.next = newNode;
        this.tail = newNode;
        return this;
    }
}

pq = new PriQueue()

pq.enqueue(1, 1).enqueue(2, 1).enqueue(2, 2).enqueue(5, 3).enqueue(1, 2).enqueue(6, 3).enqueue(2, 5);
pq.log()