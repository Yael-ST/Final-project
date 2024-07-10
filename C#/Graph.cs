using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    internal class Edge
    {
        public Sentence Source { get; set; }
        public Sentence Destination { get; set; }

        public Edge(Sentence source, Sentence destination)
        {
            Source = source;
            Destination = destination;
        }
    }

    internal class Graph
    {
        private Dictionary<int, Sentence> vertices;
        private List<Edge> edges;

        public Graph()
        {
            vertices = new Dictionary<int, Sentence>();
            edges = new List<Edge>();
        }

        public void AddVertex(Sentence sentence)
        {
            if (!vertices.ContainsKey(sentence.Id))
            {
                vertices[sentence.Id] = sentence;
            }
        }

        public void AddEdge(Sentence sentence1, Sentence sentence2)
        {
            if (!vertices.ContainsKey(sentence1.Id))
            {
                AddVertex(sentence1);
            }
            if (!vertices.ContainsKey(sentence2.Id))
            {
                AddVertex(sentence2);
            }
            edges.Add(new Edge(sentence1, sentence2));
        }

        public Sentence FindSentenceById(int id)
        {
            return vertices.ContainsKey(id) ? vertices[id] : null!;
        }

        public List<Sentence> GetNeighbors(Sentence sentence)
        {
            return edges
                .Where(e => e.Source == sentence)
                .Select(e => e.Destination)
                .Concat(edges.Where(e => e.Destination == sentence).Select(e => e.Source))
                .ToList();
        }


        // פונקציה לסריקת DFS רקורסיבית
        public void DFS(int startId)
        {
            var startSentence = FindSentenceById(startId);
            if (startSentence == null)
            {
                Console.WriteLine("Sentence not found.");
                return;
            }

            HashSet<int> visited = new HashSet<int>();
            DFSRecursive(startSentence, visited);
        }

        private void DFSRecursive(Sentence sentence, HashSet<int> visited)
        {
            if (visited.Contains(sentence.Id))
                return;

            visited.Add(sentence.Id);
            Console.WriteLine($"{sentence.Id}: {sentence.Text}");

            foreach (Sentence neighbor in GetNeighbors(sentence))
            {
                if (!visited.Contains(neighbor.Id))
                {
                    DFSRecursive(neighbor, visited);
                }
            }
        }
    }
}
