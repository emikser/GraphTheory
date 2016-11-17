﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GraphTheory.Lab1
{
    public class AdjacencyMatrix
    {
        private List<List<int>> matrix;
        
        public AdjacencyMatrix()
        {
            matrix = new List<List<int>>();
        }

        public AdjacencyMatrix(int verticleCount)
        {
            matrix = new List<List<int>>();
            for (int i = 0; i < verticleCount; i++)
            {
                AddVertex();
            }
        }

        public void PrintAdjacency()
        {
            Console.WriteLine("Adjacency Matrix:");

            foreach (var item in matrix)              
            {
                foreach (var field in item)
                {
                    Console.Write(field + " ");
                }
                Console.WriteLine();
            }
        }

        public int AddVertex()
        {
            matrix.Add(Enumerable.Repeat(0, matrix.Count).ToList());
            foreach (var item in matrix)
            {
                item.Add(0);
            }

            return matrix.Count;
        }

        public void RemoveVertex(int vertex)
        {
            vertex -= 1;
            matrix.Remove(matrix[vertex]);
            foreach (var item in matrix)
            {
                item.RemoveAt(vertex);
            }
        }

        public void AddEdge(int vertexA, int vertexB)
        {
            vertexA -= 1;
            vertexB -= 1;
            int val = 1;
            if (vertexA == vertexB)
                val = 2;
            matrix[vertexA][vertexB] = matrix[vertexB][vertexA] += val;
        }

        public void RemoveEdge(int vertexA, int vertexB)
        {
            vertexA -= 1;
            vertexB -= 1;
            int val = 1;
            if (vertexA == vertexB)
                val = 2;

            if(matrix[vertexA][vertexB] <= 0)
            {
                Console.WriteLine("There is no such edge [" + (vertexA + 1) + ";" + (vertexB + 1) + "]");
                return;
            }
            matrix[vertexA][vertexB] = matrix[vertexB][vertexA] -= val;
        }

        public int VertexDegree(int vertex)
        {
            return matrix[vertex - 1].Sum();
        }

        public void PrintVerticesDegrees()
        {
            var degrees = new Dictionary<int, int>();
            for (int i = 1; i <= matrix.Count; i++)
            {
                degrees.Add(i, VertexDegree(i));
            }

            var items = from pair in degrees
                        orderby pair.Value descending
                        select pair;

            int oddCount = 0;
            foreach (var item in items)
            {
                if (item.Value % 2 != 0)
                    oddCount += 1;
                Console.Write("Deg(" + item.Key + ")==" + item.Value + ";  ");
            }
            Console.WriteLine("\nOdd=="+oddCount+"; Even=="+(items.Count() - oddCount));
        }
    }
}