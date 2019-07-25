using System;

public class GraphAdjMatrix<T>
{
    private T[] nodes;
    public int numEdges { get; set; }
    private int[,] matrix;

    public GraphAdjMatrix(int n)
    {
        nodes = new T[n];
        matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i != j)
                    matrix[i, j] = int.MaxValue;
                else
                    matrix[i, j] = 0;
            }
        }
        numEdges = 0;
    }

    // 获取索引为index的顶点信息
    public T GetNode(int index)
    {
        return nodes[index];
    }

    // 设置索引为index的顶点信息
    public void SetNode(int index, T v)
    {
        nodes[index] = v;
    }

    // 获取边信息
    public int GetMatrix(int index1,int index2)
    {
        return matrix[index1, index2];
    }

    // 设置边信息
    public void SetMatrix(int index1,int index2,int v)
    {
        matrix[index1, index2] = v;
    }

    // 获取顶点数目
    public int GetNumOfVertex()
    {
        return nodes.Length;
    }

    // 获取边数目
    public int GetNumOfEdge()
    {
        return numEdges;
    }

    //判断v是否是图的顶点
    public bool IsNode(T v)
    {
        foreach(T node in nodes)
        {
            if (v.Equals(node))
            {
                return true;
            }
        }
        return false;
    }

    // 获取顶点v在顶点数组中的索引
    public int GetIndex(T v)
    {
        for (int i = 0; i < nodes.Length; ++i)
        {
            if (nodes[i].Equals(v))
                return i;
        }
        return -1;
    }

    // 在顶点v1和v2间添加权值为v的边
    public void SetEdge(T v1,T v2,int v)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return;
        }
        //if (v != 1)
        //{
        //    Console.WriteLine("Wrong weight!");
        //    return;
        //}
        matrix[GetIndex(v1), GetIndex(v2)] = v;
        matrix[GetIndex(v2), GetIndex(v1)] = v;
        ++numEdges;
    }

    // 删除顶点v1和v2之间的边
    public void DeleteEdge(T v1, T v2)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return;
        }
        //if (matrix[GetIndex(v1), GetIndex(v2)] == 0)
        //    return;
        //matrix[GetIndex(v1), GetIndex(v2)] = 0;
        //matrix[GetIndex(v2), GetIndex(v1)] = 0;
        //--numEdges;
        if (matrix[GetIndex(v1), GetIndex(v2)] == int.MaxValue)
            return;
        matrix[GetIndex(v1), GetIndex(v2)] = int.MaxValue;
        matrix[GetIndex(v2), GetIndex(v1)] = int.MaxValue;
        --numEdges;
    }

    // 判断顶点v1,v2是否连接
    public bool EdgeExist(T v1, T v2)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return false;
        }
        //if (matrix[GetIndex(v1), GetIndex(v2)] == 0)
        //    return false;
        //else
        //    return true;
        if (matrix[GetIndex(v1), GetIndex(v2)] == int.MaxValue)
            return false;
        else
        {
            return true;
        }
    }

    // Prim算法求最小生成树
    public int[] Prim()
    {
        var lowcost = new int[nodes.Length]; // 权值数组
        var closevex = new int[nodes.Length]; // 顶点数组
        int mincost = int.MaxValue;

        // 辅助技能初始化
        for(int i = 1; i < nodes.Length; ++i)
        {
            lowcost[i] = matrix[0, i];
            closevex[i] = 0;
        }
        // 某个顶点加入集合U
        lowcost[0] = 0;
        closevex[0] = 0;
        for(int i = 0; i < nodes.Length; ++i)
        {
            int k = 1;
            int j = 1;
            mincost = int.MaxValue;

            // 选取权值最小的边和顶点
            while (j<nodes.Length)
            {
                if (lowcost[j]<mincost&&lowcost[j]!=0)
                {
                    k = j;
                    mincost = lowcost[j];
                }
                ++j;
            }

            // 新顶点加入集合U
            lowcost[k] = 0;

            // 重新计算该顶点到其余顶点的边和权值
            for (j = 1; j < nodes.Length; ++j)
            {
                if (matrix[k,j]<lowcost[j])
                {
                    lowcost[j] = matrix[k, j];
                    closevex[j] = k;
                }
            }
        }

        return closevex;
    }
}

public class adjListNode<T>
{
    public int adjvex { get; set; }
    public adjListNode<T> next { get; set; }

    public adjListNode(int vex)
    {
        adjvex = vex;
        next = null;
    } 
}

public class VexNode<T>
{
    public T data { get; set; }
    public adjListNode<T> firstAdj { get; set; }

    public VexNode()
    {
        data = default;
        firstAdj = default;
    }

    public VexNode(T node)
    {
        data = node;
        firstAdj = default;
    }

    public VexNode(T node, adjListNode<T> alNode)
    {
        data = node;
        firstAdj = alNode;
    }
}

public class GraphAdjList<T>
{
    private VexNode<T>[] adjList;
    private int[] visited;

    public VexNode<T> this[int index]
    {
        get
        {
            return adjList[index];
        }
        set
        {
            adjList[index] = value;
        }
    }
    
    public GraphAdjList(T[] nodes)
    {
        adjList = new VexNode<T>[nodes.Length];
        for(int i = 0; i < nodes.Length; ++i)
        {
            adjList[i].data = nodes[i];
            adjList[i].firstAdj = default;
        }

        visited = new int[adjList.Length];
        for (int i = 0; i < visited.Length; ++i)
        {
            visited[i] = 0;
        }
    }

    // 获取顶点数目
    public int GetNumOfVertex()
    {
        return adjList.Length;
    }

    // 获取边数目
    public int GetNumOfEdges()
    {
        int i = 0;
        foreach(VexNode<T> node in adjList)
        {
            var p = node.firstAdj;
            while (p != null)
            {
                ++i;
                p = p.next;
            }
        }
        return i / 2;
    }

    // 判断v是否是图的顶点
    public bool IsNode(T v)
    {
        foreach (var node in adjList)
        {
            if (v.Equals(node.data))
            {
                return true;
            }
        }
        return false;
    }

    // 获取顶点v在邻接表数组中的索引
    public int GetIndex(T v)
    {
        for (int i = 0; i < adjList.Length; ++i)
        {
            if (adjList[i].data.Equals(v))
                return i;
        }
        return -1;
    }

    // 判断v1,v2间是否存在边
    public bool IsEdge(T v1,T v2)
    {
        if (!IsNode(v1)||!IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return false;
        }
        var p = adjList[GetIndex(v1)].firstAdj;
        while (p != null)
        {
            if (p.adjvex==GetIndex(v2))
            {
                return true;
            }
            p = p.next;
        }
        return false;
    }

    // 顶点v1,v2间添加权值为v的边
    public int SetEdge(T v1, T v2,int v)
    {
        if (!IsNode(v1) || !IsNode(v2) || !IsEdge(v1, v2))
        {
            Console.WriteLine("Node or Edge not belong to Graph!");
            return 0;
        }

        if (v != 1)
        {
            Console.WriteLine("Wrong weight!");
            return 0;
        }

        var p = new adjListNode<T>(GetIndex(v2));

        if (adjList[GetIndex(v1)].firstAdj == null)
            adjList[GetIndex(v1)].firstAdj = p;
        else
        {
            p.next = adjList[GetIndex(v1)].firstAdj;
            adjList[GetIndex(v1)].firstAdj = p;
        }

        p = new adjListNode<T>(GetIndex(v1));

        if (adjList[GetIndex(v2)].firstAdj == null)
            adjList[GetIndex(v2)].firstAdj = p;
        else
        {
            p.next = adjList[GetIndex(v2)].firstAdj;
            adjList[GetIndex(v2)].firstAdj = p;
        }

        return 1;
    }

    // 删除顶点v1,v2之间的边
    public int DeleteEdge(T v1,T v2)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return 0;
        }

        if (IsEdge(v1, v2))
        {
            var p = adjList[GetIndex(v1)].firstAdj;
            adjListNode<T> pre = null;

            while (p != null)
            {
                if (p.adjvex != GetIndex(v2))
                {
                    pre = p;
                    p = p.next;
                }
            }

            pre.next = p.next;

            p = adjList[GetIndex(v2)].firstAdj;
            pre = null;

            while (p != null)
            {
                if (p.adjvex != GetIndex(v1))
                {
                    pre = p;
                    p = p.next;
                }
            }

            pre.next = p.next;
        }

        return 1;
    }

    // 图深度优先遍历
    public void DFS()
    {
        for (int i = 0; i < visited.Length; ++i)
        {
            if (visited[i] == 0)
            {
                DFSAL(i);
            }
        }
    }

    public void DFSAL(int i)
    {
        visited[i] = 1;
        var p = adjList[i].firstAdj;
        Console.WriteLine("Visit " + adjList[i]);

        while (p!=null)
        {
            if (visited[p.adjvex] == 0)
            {
                DFSAL(p.adjvex);
            }
            p = p.next;
        }
    }

    // 广度优先遍历
    public void BFS()
    {
        for(int i = 0; i < visited.Length; ++i)
        {
            if (visited[i] == 0)
            {
                BFSAL(i);
            }
        }
    }

    public void BFSAL(int i)
    {
        visited[i] = 1;
        Console.WriteLine("Visit " + adjList[i].data);
        CSeqQueue<int> cq = new CSeqQueue<int>(visited.Length);
        cq.In(i);
        while (!cq.IsEmpty())
        {
            int k = cq.Out();
            var p = adjList[k].firstAdj;
            while (p != null)
            {
                if (visited[p.adjvex] == 0)
                {
                    visited[p.adjvex] = 1;
                    Console.WriteLine("Visit " + p.adjvex);
                    cq.In(p.adjvex);
                }
                p = p.next;
            }
        }
    }
}

public class DirecNetAdjMatrix<T> 
{ 
    private T[] nodes;
    private int numArcs { get; set; }
    private int[,] matrix;

    public DirecNetAdjMatrix(int n)
    {
        nodes = new T[n];
        matrix = new int[n, n];
        numArcs = 0;
    }

    // 获取索引为index的顶点信息
    public T GetNode(int index)
    {
        return nodes[index];
    }

    // 设置索引为index的顶点的信息
    public void SetNode(int index,T v)
    {
        nodes[index] = v;
    }

    // 获取弧
    public int GetMatrix(int index1,int index2)
    {
        return matrix[index1, index2];
    }

    // 获取顶点数目
    public int GetNumOfVertex()
    {
        return nodes.Length;
    }

    // 获取弧数目
    public int GetNumOfArcs()
    {
        return numArcs;
    }

    // 判断v是否是网的顶点
    public bool IsNode(T v)
    {
        foreach (T node in nodes)
        {
            if (v.Equals(node))
            {
                return true;
            }
        }
        return false;
    }

    // 获取v在顶点数组中的索引
    public int GetIndex(T v)
    {
        int i = -1;
        for ( i = 0; i < nodes.Length; ++i)
        {
            if (nodes[i].Equals(v))
            {
                return i;
            }
        }
        return i;
    }

    // 添加弧
    public int SetArc(T v1, T v2, int v)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return 0;
        }
        matrix[GetIndex(v1), GetIndex(v2)] = v;
        return 1;
    }

    // 删除弧
    public int DelArc(T v1, T v2)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return 0;
        }

        if (matrix[GetIndex(v1),GetIndex(v2)]==int.MaxValue)
        {
            return 1;
        }
        matrix[GetIndex(v1), GetIndex(v2)] = int.MaxValue;
        --numArcs;
        return 1;
    }

    // 判断是否为弧
    public bool IsArc(T v1,T v2)
    {
        if (!IsNode(v1) || !IsNode(v2))
        {
            Console.WriteLine("Node not belong to Graph!");
            return false;
        }

        if (matrix[GetIndex(v1), GetIndex(v2)] == int.MaxValue)
            return false;
        else
            return true;
    }

    // Dijkstra算法单源最短路
    public void Dijkstra(ref bool[,] pathMatricArr,ref int[] shortPathArr,T n)
    {
        int k = 0;
        bool[] final = new bool[nodes.Length];

        // 初始化
        for (int i = 0; i < nodes.Length; ++i)
        {
            final[i] = false;
            shortPathArr[i] = matrix[GetIndex(n), i];
            for (int j = 0; j < nodes.Length; ++j)
            {
                pathMatricArr[i, j] = false;
            }
            if (shortPathArr[i] != 0 && shortPathArr[i] < int.MaxValue)
            {
                pathMatricArr[i, GetIndex(n)] = true;
                pathMatricArr[i, i] = true;
            }
        }

        // n为源点，处理n到n的最短路径
        shortPathArr[GetIndex(n)] = 0;
        final[GetIndex(n)] = true;

        // 处理n到其余顶点的最短路径
        for (int i = 0; i < nodes.Length; ++i)
        {
            int min = int.MaxValue;

            // 比较n到其余顶点的路径长度
            for (int j = 0; j < nodes.Length; ++j)
            {
                if (!final[j])
                {
                    if (shortPathArr[j] < min)
                    {
                        k = j;
                        min = shortPathArr[j];
                    }
                }
            }

            // n到顶点k的路径长度最小
            final[k] = true;

            // 更新当前最短路径及距离
            for (int j = 0; j < nodes.Length; ++j)
            {
                if (!final[j] && (min + matrix[k, j] < shortPathArr[j]))
                {
                    shortPathArr[j] = min + matrix[k, j];
                    for (int w = 0; w < nodes.Length; ++w)
                    {
                        pathMatricArr[j, w] = pathMatricArr[k, w];
                    }
                    pathMatricArr[j, j] = true;
                }
            }
        }
    }
}
