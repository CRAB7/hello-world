using System;
using System.Collections;
using System.Collections.Generic;
// 二叉链表结点类
public class Node<T> where T:IComparable
{
    private T data;
    private Node<T> lChild;
    private Node<T> rChild;

    public Node(T val, Node<T> lp, Node<T> rp)
    {
        data = val;
        lChild = lp;
        rChild = rp;
    }

    public Node(Node<T> lp, Node<T> rp)
    {
        data = default;
        lChild = lp;
        rChild = rp;
    }

    public Node(T val)
    {
        data = val;
        lChild = null;
        rChild = null;
    }

    public Node()
    {
        data = default;
        lChild = null;
        rChild = null;
    }

    public T Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }

    public Node<T> LChild
    {
        get
        {
            return lChild;
        }
        set
        {
            lChild = value;
        }
    }

    public Node<T> RChild
    {
        get
        {
            return rChild;
        }
        set
        {
            rChild = value;
        }
    }
}

// 二叉链表类
public class BiTree<T> where T: IComparable
{
    private Node<T> head;

    public Node<T> Head
    {
        get
        {
            return head;
        }
        set
        {
            head = value;
        }
    }

    public BiTree()
    {
        head = null;
    }

    public BiTree(T val)
    {
        head = new Node<T>(val);
    }

    public BiTree(T val, Node<T> lp, Node<T> rp)
    {
        head = new Node<T>(val, lp, rp);
    }

    // 判断是否二叉树是否为空
    public bool IsEmpty()
    {
        if (head == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 获取节点的左孩子
    public Node<T> GetLChild(Node<T> p)
    {
        return p.LChild;
    }

    // 获取节点的右孩子
    public Node<T> GetRChild(Node<T> p)
    {
        return p.RChild;
    }

    // 插入新节点val到节点p的左子树
    public void InsertL(T val, Node<T> p)
    {
        var tmp = new Node<T>(val);
        tmp.LChild = p.LChild;
        p.LChild = tmp;
    }

    // 插入新节点val到节点p的右子树
    public void InsertR(T val, Node<T> p)
    {
        var tmp = new Node<T>(val);
        tmp.RChild = p.RChild;
        p.RChild = tmp;
    }

    // 若p非空，删除p的左子树
    public Node<T> DeleteL(Node<T> p)
    {
        if ((p != null) && (p.LChild != null))
        {
            var tmp = p.LChild;
            p.LChild = null;
            return tmp;
        }
        return null;
    }

    // 若p非空，删除p的右子树
    public Node<T> DeleteR(Node<T> p)
    {
        if ((p != null) && (p.RChild != null))
        {
            var tmp = p.RChild;
            p.RChild = null;
            return tmp;
        }
        return null;
    }

    // 获得节点的度
    public int GetDegree(Node<T> p)
    {
        int degree = 0;
        if (p.LChild != null)
            degree++;
        if (p.RChild != null)
            degree++;
        return degree;
    }

    // 判断是否是叶子节点
    public bool IsLeaf(Node<T> p)
    {
        if ((p != null) && (GetDegree(p) == 0))
            return true;
        else
            return false;
    }

    // 先序遍历
    public void PreOrder(Node<T> root)
    {
        if (root == null)
            return;
        // 打印根节点
        Console.WriteLine(root.Data);
        // 先序遍历左子树
        PreOrder(root.LChild);
        // 先序遍历右子树
        PreOrder(root.RChild);
    }

    // 中序遍历
    public void InOrder(Node<T> root)
    {
        if (root == null)
            return;
        InOrder(root.LChild);
        Console.WriteLine(root.Data);
        InOrder(root.RChild);
    }

    // 后续遍历
    public void PostOrder(Node<T> root)
    {
        if (root == null)
            return;
        PostOrder(root.LChild);
        PostOrder(root.RChild);
        Console.WriteLine(root.Data);
    }

    // 层序遍历
    public void LevelOrder(Node<T> root)
    {
        if (root == null)
            return;
        // 保存层序遍历的节点的队列
        Queue<Node<T>> sq = new Queue<Node<T>>(50);
        sq.Enqueue(root);
        while (sq.Count != 0)
        {
            Node<T> tmp = sq.Dequeue();
            Console.WriteLine(tmp);
            if (tmp.LChild != null)
                sq.Enqueue(tmp.LChild);
            if (tmp.RChild != null)
                sq.Enqueue(tmp.RChild);
        }
    }

    // 根据值查找节点
    public Node<T> Search(Node<T> root,T value)
    {
        Node<T> p = root;
        if (p == null)
        {
            Console.WriteLine("Tree is null.");
            return null;
        }
        if (!p.Data.Equals(value))
            return p;
        if (p.LChild != null)
            Search(p.LChild, value);
        if (p.RChild != null)
            Search(p.RChild, value);
        return null;
    }

    // 获得叶子节点数目
    public int CountLeafNode(Node<T> root)
    {
        if (root == null)
            return 0;
        else if (GetDegree(root)==0)
            return 1;
        else
            return (CountLeafNode(root.LChild) + CountLeafNode(root.RChild));
    }

    // 求二叉树深度
    public int GetHeight(Node<T> root)
    {
        int lh;
        int rh;
        if (root==null)
        {
            return 0;
        }
        else if (root.LChild==null && root.RChild==null)
        {
            return 1;
        }
        else
        {
            lh = GetHeight(root.LChild);
            rh = GetHeight(root.RChild);
            return (lh > rh ? lh : rh) + 1;
        }
    }

    
}

// 二叉平衡树操作类
public class BST<T> where T:IComparable
{
    public BST()
    {

    }

    // 查找操作，返回值为0表示没找到，返回值为1表示找到
    public int BSTSearch(BiTree<T> bt, T key)
    {
        // BST为空
        if (bt.IsEmpty() == true)
        {
            Console.WriteLine("The Binary Sorting Tree is empty!");
            return 0;
        }

        var p = bt.Head;
        // BST非空
        while (p != null)
        {
            // 存在要查找的记录
            if (p.Data.CompareTo(key)==0)
            {
                Console.WriteLine(key + " exist!");
                return 1;
            }
            // 待查找记录的关键码大于节点关键码
            else if (p.Data.CompareTo(key)<0)
            {
                p = p.RChild;
            }
            // 待查找记录的关键码校园节点关键码
            else
            {
                p = p.LChild;
            }
        }

        Console.WriteLine("{0} not exist!", key);
        return 0;

    }

    // 插入操作，返回值为0表示插入失败，返回值为1表示插入成功
    public int BSTInsert(BiTree<T> bt, T key)
    {
        Node<T> p;
        Node<T> parent = new Node<T>();

        p = bt.Head;
        //二叉排序树非空
        while (p != null)
        {
            if (p.Data.CompareTo(key)==0)
            {
                Console.WriteLine("Record is exist!");
                return 0;
            }
            parent = p;
            if (p.Data.CompareTo(key)<0)
            {
                p = p.RChild;
            }
            else
            {
                p = p.LChild;
            }
        }

        p = new Node<T>(key);
        // 二叉排序树为空
        if (parent == null)
        {
            bt.Head = p;
        }
        else if (p.Data.CompareTo(parent.Data)<0)
        {
            parent.LChild = p;
        }
        else
        {
            parent.RChild = p;
        }
        return 1;
    }

    // 删除操作
    public int BSTDelete(BiTree<T> bt, T key)
    {
        Node<T> p;
        Node<T> parent = new Node<T>();
        Node<T> s = new Node<T>();
        Node<T> q = new Node<T>();

        // BST为空
        if (bt.IsEmpty())
        {
            Console.WriteLine("The Binary Soring Tree is empty!");
            return 0;
        }
        p = bt.Head;
        parent = p;
        // BST非空
        while (p != null)
        {
            // 存在关键码为key的结点
            if (p.Data.CompareTo(key)==0)
            {
                // 结点为叶子结点
                if (bt.IsLeaf(p))
                {
                    if (p == bt.Head)
                    {
                        bt.Head = null;
                    }
                    else if (p == parent.LChild)
                    {
                        parent.LChild = null;
                    }
                    else
                    {
                        parent.RChild = null;
                    }
                }
                // 左结点非空，右结点为空
                else if ((p.RChild == null) && (p.LChild != null))
                {
                    if (p == parent.LChild)
                    {
                        parent.LChild = p.LChild;
                    }
                    else
                    {
                        parent.RChild = p.LChild;
                    }
                }
                // 右结点非空，左结点为空
                else if ((p.RChild != null) && (p.LChild == null))
                {
                    if (p == parent.LChild)
                    {
                        parent.LChild = p.RChild;
                    }
                    else
                    {
                        parent.RChild = p.RChild;
                    }
                }
                // 左右结点都为空
                else
                {
                    q = p;
                    s = p.RChild;
                    while (s.LChild != null)
                    {
                        q = s;
                        s = s.LChild;
                    }
                    p.Data = s.Data;
                    if (p == q)
                    {
                        q.RChild = s.RChild;
                    }
                    else
                    {
                        q.LChild = s.RChild;
                    }
                }
                Console.WriteLine("{0} has been deleted!", key);
                return 1;
            }
            else if (p.Data.CompareTo(key)<0)
            {
                parent = p;
                p = p.RChild;
            }
            else
            {
                parent = p;
                p = p.LChild;
            }
        }
        Console.WriteLine("{0} not exist!", key);
        return 0;
    }

    // 控制输出右边填充空格位数
    public static string Indent(int count)
    {
        return " ".PadLeft(count);
    }

    // 展示BST,i≡1, p≡root
    public void BSTShow(Node<T> p,int i)
    {
        if (p!=null)
        {
            Console.WriteLine(Indent(i) + p.Data);
            BSTShow(p.LChild,i+2);
            BSTShow(p.RChild,i+2);
        }
        else
        {
            Console.WriteLine(Indent(i) + "*");
        }         
    }
}

// 哈夫曼节点类
public class HaNode
{
    private int weight;
    private int lChild;
    private int rChild;
    private int parent;

    public int Weight
    {
        get
        {
            return weight;
        }
        set
        {
            weight = value;
        }
    }

    public int LChild
    {
        get
        {
            return lChild;
        }
        set
        {
            lChild = value;
        }
    }

    public int RChild
    {
        get
        {
            return rChild;
        }
        set
        {
            rChild = value;
        }
    }

    public int Parent
    {
        get
        {
            return parent;
        }
        set
        {
            parent = value;
        }
    }

    public HaNode()
    {
        weight = default;
        lChild = -1;
        rChild = -1;
        parent = -1;
    }

    public HaNode(int w, int lc, int rc, int p)
    {
        weight = w;
        lChild = lc;
        rChild = rc;
        parent = p;
    }
}

// 哈夫曼树类
public class HaTree
{
    private HaNode[] data;
    private int leafNum;

    public HaNode this[int index]
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

    public int LeafNum
    {
        get
        {
            return leafNum;
        }
        set
        {
            leafNum = value;
        }
    }

    public HaTree(int n)
    {
        data = new HaNode[2 * n - 1];
        leafNum = n;
    }

    public void Create()
    {
        int m1, m2, x1, x2;
        for (int i = 0; i < leafNum; ++i)
            data[i].Weight = Console.Read();
        for(int i = 0; i < leafNum; ++i)
        {
            m1 = m2 = Int32.MaxValue;
            x1 = x2 = 0;
            for (int j = 0; j < leafNum+i; ++j)
            {
                if ((data[i].Weight < m1) && (data[i].Parent == -1))
                {
                    m2 = m1;
                    x2 = x1;
                    x1 = j;
                    m1 = data[j].Weight;
                }
                else if ((data[i].Weight<m2)&&(data[i].Parent==-1))
                {
                    m2 = data[j].Weight;
                    x2 = j;
                }
            }
            data[x1].Parent = leafNum + i;
            data[leafNum + i].Weight = data[x1].Weight + data[x2].Weight;
            data[leafNum + i].LChild = x1;
            data[leafNum + i].RChild = x2;
        }
    }
}

// 平衡二叉树结点类
public class AVLNode<T> where T : IComparable
{
    private T data;
    private AVLNode<T> lChild;
    private AVLNode<T> rChild;
    private int height;

    public AVLNode(T d, AVLNode<T> lc,AVLNode<T> rc, int h)
    {
        data = d;
        lChild = lc;
        rChild = rc;
        height = h;
    }

    public AVLNode(T d)
    {
        data = d;
        lChild = null;
        rChild = null;
        height = 1;
    }

    public AVLNode(AVLNode<T> lc,AVLNode<T> rc, int h)
    {
        data = default;
        lChild = rc;
        rChild = lc;
        height = h;
    }
    
    public AVLNode()
    {
        data = default;
        lChild = null;
        rChild = null;
        height = 1;
    }

    public T Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
        }
    }

    public AVLNode<T> LChild
    {
        get
        {
            return lChild;
        }
        set
        {
            lChild = value;
        }
    }

    public AVLNode<T> RChild
    {
        get
        {
            return rChild;
        }
        set
        {
            rChild = value;
        }
    }
    
    public int Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
        }
    }

    public bool IsLeaf(AVLNode<T> p)
    {
        if (p.LChild == null && p.RChild == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class AVLTree<T> where T : IComparable
{
    private AVLNode<T> head;

    public AVLNode<T> Head
    {
        get
        {
            return head;
        }
        set
        {
            head = value;
        }
    }

    public AVLTree(T val)
    {
        head = new AVLNode<T>(val);
    }

    public AVLTree()
    {
        head = null;
    }

    public AVLTree(T d, AVLNode<T> lc, AVLNode<T> rc, int h)
    {
        head = new AVLNode<T>(d, lc, rc, h);
    }

    // 获得深度
    public int GetHeight(AVLNode<T> p)
    {
        return p == null ? 0 : p.Height;
    }

    // 插入
    public int Insert(T key)
    {
        var result = Insert(key, head);
        if (result == null)
            return 0;
        else
        {
            head = result;
            return 1;
        }
    }

    public AVLNode<T> Insert(T key, AVLNode<T> root)
    {
        if (root  == null)
        {
            root  = new AVLNode<T>(key);
        }
        else if (root.Data.CompareTo(key)==0)
        {
            Console.WriteLine(key + " has exist!");
            return null;
        }
        else if (root.Data.CompareTo(key)>0)
        {
            root.LChild = Insert(key, root.LChild);
            if (GetHeight(root.LChild) - GetHeight(root.RChild) == 2 && GetHeight(root.LChild.LChild) - GetHeight(root.LChild.RChild) != -1)
                root = RotateLL(root);
            else if (GetHeight(root.LChild) - GetHeight(root.RChild) == 2 && GetHeight(root.LChild.LChild) - GetHeight(root.LChild.RChild) == -1)
            {
                root.LChild = RotateRR(root.LChild);
                root = RotateLL(root);
            }
        }
        else if(root.Data.CompareTo(key)<0)
        {
            root.RChild = Insert(key, root.RChild);
            if (GetHeight(root.LChild) - GetHeight(root.RChild) == -2 && GetHeight(root.RChild.LChild) - GetHeight(root.RChild.RChild) != 1)
                root = RotateRR(root);
            else if (GetHeight(root.LChild) - GetHeight(root.RChild) == -2 && GetHeight(root.RChild.LChild) - GetHeight(root.RChild.RChild) == 1)
            {
                root.RChild = RotateLL(root.RChild);
                root = RotateRR(root);
            }   
        }

        root.Height = Math.Max(GetHeight(root.LChild), GetHeight(root.RChild)) + 1;
        return root;
    }

    // 右旋
    public AVLNode<T> RotateLL(AVLNode<T> root)
    {
        var p = root.LChild;
        root.LChild = p.RChild;
        p.RChild = root;
        root.Height = Math.Max(GetHeight(root.LChild), GetHeight(root.RChild)) + 1;
        p.Height = Math.Max(GetHeight(p.LChild), GetHeight(p.RChild)) + 1;
        return p;
    }

    // 左旋
    public AVLNode<T> RotateRR(AVLNode<T> root)
    {
        var p = root.RChild;
        root.RChild = p.LChild;
        p.LChild = root;
        root.Height = Math.Max(GetHeight(root.LChild), GetHeight(root.RChild)) + 1;
        p.Height = Math.Max(GetHeight(p.LChild), GetHeight(p.RChild)) + 1;
        return p;
    }

    // 控制输出右边填充空格位数
    public static string Indent(int count)
    {
        return " ".PadLeft(count);
    }

    // 展示AVL,i≡1, p≡root
    public void AVLShow(AVLNode<T> p, int i)
    {
        if (p != null)
        {
            Console.WriteLine(Indent(i) + p.Data);
            AVLShow(p.LChild, i + 2);
            AVLShow(p.RChild, i + 2);
        }
        else
        {
            Console.WriteLine(Indent(i) + "*");
        }
    }
}

//class Test3
//{
//    static void Main()
//    {
//        var avl = new AVLTree<int>(50);
//     //   avl.AVLShow(avl.Head, 1);
//        avl.Insert(40);
//       // avl.AVLShow(avl.Head, 1);
//        avl.Insert(60);
//        avl.AVLShow(avl.Head, 1);
//        avl.Insert(70);
//        avl.Insert(65);
//        avl.AVLShow(avl.Head, 1);
//        Console.ReadKey();
//    }
//}

//class Test2
//{
//    static void Main()
//    {
//        var bt = new BiTree<int>(50);
//        //var x = bt.Head;
//        //Console.WriteLine("xxx:" + x.Data.CompareTo(50));
//        var bst = new BST<int>();
//        bst.BSTInsert(bt, 40);
//        bst.BSTInsert(bt, 60);
//        bst.BSTInsert(bt, 45);
//        bst.BSTInsert(bt, 30);
//        bst.BSTInsert(bt, 35);
//        bst.BSTInsert(bt, 42);
//        bst.BSTInsert(bt, 48);
//        bst.BSTInsert(bt, 30);
//        bst.BSTShow(bt.Head, 1);
//        //bst.BSTDelete(bt, 40);
//        //bst.BSTShow(bt, bt.Head, 1);
//        //bst.BSTSearch(bt, 60);
//        //bt.PreOrder(bt.Head);
//        //bt.InOrder(bt.Head);
//        //bt.PostOrder(bt.Head);
//        Console.WriteLine("Height is " + bt.GetHeight(bt.Head));
//        Console.ReadKey();
//    }
//}