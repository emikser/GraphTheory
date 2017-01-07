﻿using GraphTheory.Lab3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphTheory.FordFulkerson
{
    public class EdmondsKarp
    {
        public float MaximumFlow(WeightedDiAdjacencyMatrix graph, int s, int t)
        {
            bool noPath = false;
            float fMax = 0f;
            var nettoMatrix = new WeightedDiAdjacencyMatrix(graph.Order);
            var predecesors = new int[graph.Order];
            var cfp = new float[graph.Order];
            Queue<int> bfsVertex = new Queue<int>();
            while (true)
            {
                for (int i = 0; i < predecesors.Length; i++)
                {
                    predecesors[i] = -1;
                }
                predecesors[s] = -2;
                cfp[s] = float.PositiveInfinity;
                while (bfsVertex.Count > 0)
                {
                    bfsVertex.Dequeue();
                }
                bfsVertex.Enqueue(s);
                while (bfsVertex.Count > 0)
                {
                    noPath = true;
                    bool breakWhile = false;
                    var x = bfsVertex.Dequeue();
                    foreach (var y in graph.Neighbours(x + 1))
                    {
                        var nettoValue = 0f;
                        if(nettoMatrix.IsEdgeByLabels(x+1,y))
                            nettoValue = nettoMatrix.GetEdgeByLabels(x + 1, y).Weight;

                        var cp = graph.GetEdgeByLabels(x + 1, y).Weight - nettoValue;
                        if (cp == 0f || predecesors[y - 1] != -1)
                            continue;

                        predecesors[y - 1] = x;
                        cfp[y - 1] = Math.Min(cfp[x], cp);

                        if ((y - 1) == t)
                        {
                            noPath = false;
                            breakWhile = true;
                            break;
                        }
                        bfsVertex.Enqueue(y - 1);
                    }
                    if (breakWhile)
                        break;
                }
                if (noPath)
                    return fMax;

                fMax += cfp[t];

                var ny = t;
                while (ny != s)
                {
                    var x = predecesors[ny];

                    if (!nettoMatrix.IsEdgeByLabels(x + 1, ny + 1))
                        nettoMatrix.AddEdge(x + 1, ny + 1, 0f);
                    nettoMatrix.GetEdgeByLabels(x + 1, ny + 1).Weight += cfp[t];

                    if (!nettoMatrix.IsEdgeByLabels(ny + 1, x + 1))
                        nettoMatrix.AddEdge(ny + 1, x + 1, 0f);

                    nettoMatrix.GetEdgeByLabels(ny + 1, x + 1).Weight -= cfp[t];
                    ny = x;
                }
            }
            return fMax;
        }
    }
}
