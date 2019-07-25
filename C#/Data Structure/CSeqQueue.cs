using System;
using System.Text;

// 测试优先循环队列用
public class PqItem : IComparable
{
    private int priority;
    private string mydata;

    public PqItem(int pri, string data)
    {
        priority = pri;
        mydata = data;
    }
    public override string ToString()
    {
        return string.Format("<{0},{1}>", priority, mydata);
    }
    public int CompareTo(object obj)
    {
        PqItem pqItem = obj as PqItem;
        if (pqItem == null)
        {
            Console.WriteLine("Not a PqItem!");
            return -1;
        }

        return this.priority - pqItem.priority;
    }

    public string Mydata
    {
        get
        {
            return mydata;
        }
        set
        {
            mydata = value;
        }
    }
}

// 优先循环队列
public class CSeqQueue<T> where T : IComparable
{
    private int maxsize;
    private T[] data;
    private int front;
    private int rear;

    // 索引器
    public T this[int index]
    {
        get
        {
            return data[index];
        }
        set
        {
            data[index] = value;
        }
    }

    // 容量属性
    public int Maxsize
    {
        get
        {
            return maxsize;
        }
        set
        {
            maxsize = value;
        }
    }

    // 队头属性
    public int Front
    {
        get
        {
            return front;
        }
        set
        {
            front = value;
        }
    }

    //队尾属性
    public int Rear
    {
        get
        {
            return rear;
        }
        set
        {
            rear = value;
        }
    }

    //构造器
    public CSeqQueue(int size)
    {
        data = new T[size + 1];
        maxsize = size + 1;
        front = rear = -1;
    }

    // 求循环队列的长度
    public int GetLength()
    {
        return (rear - front + maxsize) % maxsize;
    }

    // 清空循环队列
    public void Clear()
    {
        front = rear = -1;
    }

    // 判断循环队列是否为空
    public bool IsEmpty()
    {
        if (front == rear)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 判断循环队列是否为满
    public bool IsFull()
    {
        if ((rear + 1) % maxsize == front)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //// 入队+自动扩容
    //public CSeqQueue<T> In(T item)
    //{

    //    if(IsFull())
    //    {
    //        Console.WriteLine("Queue is Full! Add Capacity.");
    //        CSeqQueue<T> tempCSeqQueue = AddCapacity();
    //        tempCSeqQueue.data[++tempCSeqQueue.rear] = item;
    //        return tempCSeqQueue;
    //    }
    //    data[++rear] = item;
    //    return this;
    //}

    // 入队
    public void In(T item)
    {
        if (IsFull())
        {
            Console.WriteLine("Queue is Full! Failed.");
            return;
        }
        data[(++rear) % maxsize] = item;
    }

    // 优先级最高项出队
    public T Out()
    {
        T tmp = default;
        if (IsEmpty())
        {
            Console.WriteLine("Queue is empty!");
            return tmp;
        }

        int maxindex = (front + 1) % maxsize;
        for (int x = 2; x <= GetLength(); x++)
        {
            if (data[(front + x) % maxsize].CompareTo(data[maxindex]) > 0)
            {
                maxindex = (front + x) % maxsize;
            }
        }
        T temp = data[(front + 1) % maxsize];
        data[(front + 1) % maxsize] = data[maxindex];
        data[maxindex] = temp;
        tmp = data[(++front) % maxsize];

        return tmp;
    }

    // 获取队头数据元素
    public T GetFront()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Queue is empty!");
            return default;
        }
        return data[front + 1];
    }

    // 扩容
    public void AddCapacity()
    {
        var data1 = new T[2 * maxsize - 1];
        for (int i = 0; i < GetLength(); i++)
        {
            Console.WriteLine("in for , getlength : {0} -> {1}", GetLength(), data[(front + 1 + i) % maxsize]);
            data1[i] = data[(front + 1 + i) % maxsize];
        }
        Console.WriteLine("Before copy, front {0}, rear {1}, length {2}, maxsize {3}.", front, rear, GetLength(), maxsize - 1);
        rear = GetLength() - 1;
        front = -1;
        maxsize = 2 * maxsize - 1;
        data = data1;
        Console.WriteLine("After copy, front {0}, rear {1}, length {2}, maxsize {3}.", front, rear, GetLength(), maxsize - 1);
        Console.WriteLine("Queue capacity is {0} now, {1} items are in the queue.", maxsize, GetLength());
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i <= GetLength(); i++)
        {
            sb.Append((front + i) % maxsize).Append(">>").Append(data[(front + i) % maxsize]).Append("|");
        }
        return sb.ToString();
    }
}

// Main()
//class Test1
//{
//    static void Main()
//    {
//        CSeqQueue<PqItem> erwait = new CSeqQueue<PqItem>(5);
//        PqItem erpatient = new PqItem(1, "mine is 1");
//        erwait.In(erpatient);
//        erpatient = new PqItem(0, "mine is 0");
//        erwait.In(erpatient);
//        erpatient = new PqItem(2, "mine is 2");
//        erwait.In(erpatient);
//        Console.WriteLine("Before out, there are {0} patient waiting.", erwait.GetLength());
//        Console.WriteLine(erwait.ToString());
//        PqItem nextpatient;
//        nextpatient = erwait.Out();
//        Console.WriteLine("Next patient is {0}", nextpatient.Mydata);
//        nextpatient = erwait.Out();
//        Console.WriteLine("Next patient is {0}", nextpatient.Mydata);
//        Console.WriteLine(erwait.ToString());
//        erpatient = new PqItem(0, "mine is 0");
//        erwait.In(erpatient);
//        erpatient = new PqItem(2, "mine is 2");
//        erwait.In(erpatient);
//        erpatient = new PqItem(3, "mine is 3");
//        erwait.In(erpatient);
//        erpatient = new PqItem(2, "mine is 2");
//        erwait.In(erpatient);
//        erpatient = new PqItem(1, "mine is 1");
//        erwait.In(erpatient);
//        Console.WriteLine(erwait.ToString());
//        erwait.AddCapacity();
//        erpatient = new PqItem(1, "mine is 1");
//        erwait.In(erpatient);
//        erpatient = new PqItem(3, "mine is 3");
//        erwait.In(erpatient);
//        erpatient = new PqItem(2, "mine is 2");
//        erwait.In(erpatient);
//        erpatient = new PqItem(1, "mine is 1");
//        erwait.In(erpatient);
//        erpatient = new PqItem(4, "mine is 4");
//        erwait.In(erpatient);
//        Console.WriteLine(erwait.ToString());
//        nextpatient = erwait.Out();
//        Console.WriteLine("Next patient is {0}", nextpatient.Mydata);
//        nextpatient = erwait.Out();
//        Console.WriteLine("Next patient is {0}", nextpatient.Mydata);
//        Console.WriteLine(erwait.ToString());
//        erpatient = new PqItem(2, "mine is 2");
//        erwait.In(erpatient);
//        erpatient = new PqItem(1, "mine is 1");
//        erwait.In(erpatient);
//        erpatient = new PqItem(4, "mine is 4");
//        erwait.In(erpatient);
//        Console.WriteLine(erwait.ToString());
//        Console.ReadKey();
//    }
//}
