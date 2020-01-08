using System.Collections.Generic;
using UnityEngine;

namespace Nav2D
{
    class A_Star
    {
        const int STRAIGHT_COST = 10;
        const int DIAGONAL_COST = 14;

        ANode origin = null;
        ANode destination = null;
        BaseNode[,] graph = null;
        List<ANode> openList = null;
        List<ANode> closedList = null;

        public A_Star(ANode origin, ANode destination, BaseNode[,] graph)
        {
            this.origin = origin;
            this.destination = destination;

            openList = new List<ANode> { this.origin };
            closedList = new List<ANode>();
            this.graph = graph;
        }

        public List<ANode> CalculatePath()
        {
            for (int y = 0; y < graph.GetLength(0); y++)
            {
                for (int x = 0; x < graph.GetLength(1); x++)
                {
                    ANode node = (ANode) graph[y, x];
                    node.g = int.MaxValue;
                    node.cameFrom = null;
                }
            }

            origin.g = 0;
            origin.h = CalculateH(origin, this.destination);

            while (openList.Count > 0)
            {
                ANode current = GetLowestFCostNode(openList);

                if (current == this.destination) // reached destination
                    return GetPath(this.destination);

                openList.Remove(current);
                closedList.Add(current);

                foreach (ANode neighbour in GetNeighbours(current))
                {
                    if (closedList.Contains(neighbour)) continue;

                    int tentative = current.g + CalculateH(current, neighbour);

                    if (tentative < neighbour.g)
                    {
                        neighbour.cameFrom = current;
                        neighbour.g = tentative;
                        neighbour.h = CalculateH(neighbour, this.destination);

                        if (!openList.Contains(neighbour))
                            openList.Add(neighbour);

                    }
                }
            }

            // Could not find a path
            return null;
        }

        public List<ANode> GetNeighbours(ANode node)
        {
            List<ANode> neighbours = new List<ANode>();

            int x = node.x, y = node.y;

            // node left
            if (x < graph.GetLength(1))
            {
                ANode nodeLeft = (ANode) graph[y, x - 1];
                neighbours.Add(nodeLeft);
            }

            // node right
            if (x > 0)
            {
                ANode nodeRight = (ANode) graph[y, x + 1];
                neighbours.Add(nodeRight);
            }

            // node below
            if (y > 0)
            {
                ANode nodeBelow = (ANode) graph[y - 1, x];
                neighbours.Add(nodeBelow);
            }

            // node above
            if (y < graph.GetLength(0)-1)
            {
                ANode nodeBelow = (ANode) graph[y + 1, x];
                neighbours.Add(nodeBelow);
            }

            return neighbours;
        }

        public List<ANode> GetPath(ANode destination)
        {
            List<ANode> path = new List<ANode> { destination };
            ANode node = destination;

            while(node.cameFrom != null)
            {
                path.Add(node);
                node = (ANode) node.cameFrom;
            }
            path.Reverse();
            return path;
        }

        ANode GetLowestFCostNode(List<ANode> list)
        {
            ANode lfcNode = list[0]; // lowest f cost node

            foreach(ANode node in list)
            {
                if (node.F < lfcNode.F)
                    lfcNode = node;
            }

            return lfcNode;
        }

        public int CalculateH(ANode a, ANode b)
        {
            int xDis = Mathf.Abs(a.x - b.x);
            int yDis = Mathf.Abs(a.y - b.y);
            int rest = Mathf.Abs(xDis - yDis);

            return DIAGONAL_COST * Mathf.Min(xDis, yDis) + STRAIGHT_COST * rest;
        }
    }
}