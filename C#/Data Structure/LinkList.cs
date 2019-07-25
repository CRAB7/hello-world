//// 单链表节点
//public class Node<T>
//{
//    private T data;
//    private Node<T> next;

//    // 构造器
//    public Node(T val, Node<T> p)
//    {
//        data = val;
//        next = p;
//    }

//    // 构造器
//    public Node(T val)
//    {
//        data = val;
//        next = null;
//    }

//    // 构造器
//    public Node(Node<T> p)
//    {
//        next = p;
//    }

//    // 构造器
//    public Node()
//    {
//        next = null;
//    }

//    // 数据域属性
//    public T Data
//    {
//        get
//        {
//            return data;
//        }
//        set
//        {
//            data = value;
//        }
//    }

//    public Node<T> Next
//    {
//        get
//        {
//            return next;
//        }
//        set
//        {
//            next = value;
//        }
//    }
//}

//// 单链表类
//public class LinkList<T>
//{
//    private Node<T> head;

//    public Node<T> Head
//    {
//        get
//        {
//            return head;
//        }
//        set
//        {
//            head = value;
//        }
//    }

//    // 构造器
//    public LinkList()
//    {
//        head = null;
//    }

//    // 链表长度
//    public int GetLength()
//    {
//        Node<T> p = head;
//        int len = 0;
//        while (p != null)
//        {
//            ++len;
//            p = p.Next;
//        }
//        return len;
//    }

//    // 清空链表
//    public void Clear()
//    {
//        head = null;
//    }

//    // 判断是否为空
//    public bool IsEmpty()
//    {
//        if (head == null)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    //在单链表的末尾添加新元素
//    public void Append(T item)
//    {
//        Node<T> q = new Node<T>(item);
//        Node<T> p = new Node<T>();

//        if (head == null)
//        {
//            head = q;
//            return;
//        }

//        p = head;
//        while (p.Next != null)
//        {
//            p = p.Next;
//        }
//        p.Next = q;
//    }
//}