using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendGrafo.DAL
{
    public static class GrafoRepository
    {
        private static List<Vertice> ListaAdyacencia = new List<Vertice>();

        public static string AgregarVertice(ciudad objInfo)
        {
            try
            {
                ListaAdyacencia.Add(new Vertice(objInfo));
                return "Insertado";
            }
            catch (Exception ex)
            {
                return $"Error al agregar el vértice: {ex.Message}";
            }
        }

        public static string AgregarArista(int VertOrign, int VertDest, float cost3)
        {
            try
            {
                if (VertOrign < 0 || VertOrign >= ListaAdyacencia.Count || VertDest < 0 || VertDest >= ListaAdyacencia.Count)
                {
                    throw new Exception("La posición del origen o destino no es válida.");
                }

                ListaAdyacencia[VertOrign].AgregarArista(VertDest, cost3);
                return "Arista Agregada";
            }
            catch (Exception ex)
            {
                return $"Error al agregar la arista: {ex.Message}";
            }
        }

        public static List<int> MostrarAristasVertice(int posiVert)
        {
            try
            {
                if (posiVert < 0 || posiVert >= ListaAdyacencia.Count)
                {
                    throw new Exception("La posición del vértice no es válida.");
                }

                return ListaAdyacencia[posiVert].MuestraAristas().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al mostrar aristas del vértice: {ex.Message}");
                return null;
            }
        }

        public static List<ciudad> MostrarVertices()
        {
            return ListaAdyacencia.Select(v => v.info).ToList();
        }
        public static List<int> RecorridoDFS(int start)
        {
            try
            {
                List<int> visitados = new List<int>();
                bool[] visitado = new bool[ListaAdyacencia.Count];
                Stack<int> pila = new Stack<int>();
                pila.Push(start);

                while (pila.Count > 0)
                {
                    int vertice = pila.Pop();
                    if (!visitado[vertice])
                    {
                        visitado[vertice] = true;
                        visitados.Add(vertice);

                        foreach (var vecino in ListaAdyacencia[vertice].MuestraAristas())
                        {
                            if (!visitado[vecino])
                            {
                                pila.Push(vecino);
                            }
                        }
                    }
                }

                return visitados;
            }
            catch
            {
                return null;
            }
            
        }

        public static List<int> RecorridoBFS(int start)
        {
            try
            {
                List<int> visitados = new List<int>();
                bool[] visitado = new bool[ListaAdyacencia.Count];
                Queue<int> cola = new Queue<int>();
                cola.Enqueue(start);
                visitado[start] = true;

                while (cola.Count > 0)
                {
                    int vertice = cola.Dequeue();
                    visitados.Add(vertice);

                    foreach (var vecino in ListaAdyacencia[vertice].MuestraAristas())
                    {
                        if (!visitado[vecino])
                        {
                            visitado[vecino] = true;
                            cola.Enqueue(vecino);
                        }
                    }
                }

                return visitados;
            }
            catch
            {
                return null;
            }
            
        }

        public static List<int> OrdenTopologico()
        {
            List<int> resultado = new List<int>();
            Stack<int> stack = new Stack<int>();
            bool[] visitado = new bool[ListaAdyacencia.Count];

            for (int i = 0; i < ListaAdyacencia.Count; i++)
            {
                if (!visitado[i])
                {
                    UtilidadOrdenamientoTopologico(i, visitado, stack);
                }
            }

            while (stack.Count > 0)
            {
                resultado.Add(stack.Pop());
            }

            return resultado;
        }

        private static void UtilidadOrdenamientoTopologico(int v, bool[] visitado, Stack<int> stack)
        {
            visitado[v] = true;

            foreach (int vecino in ListaAdyacencia[v].MuestraAristas())
            {
                if (!visitado[vecino])
                {
                    UtilidadOrdenamientoTopologico(vecino, visitado, stack);
                }
            }

            stack.Push(v);
        }

        public static List<int> OrdenamientoTopologicoCamino(int start, int end)
        {
            List<int> resultado = new List<int>();
            List<int> _ordenTopologico = OrdenTopologico();
            bool[] visitado = new bool[ListaAdyacencia.Count];

            if (!_ordenTopologico.Contains(start) || !_ordenTopologico.Contains(end))
            {
                return resultado; // Si alguno de los vértices no está en el orden topológico, no hay camino
            }

            int startIndex = _ordenTopologico.IndexOf(start);
            int endIndex = _ordenTopologico.IndexOf(end);

            if (startIndex > endIndex)
            {
                return resultado; // Si el inicio está después del final en el orden topológico, no hay camino
            }

            Stack<int> stack = new Stack<int>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                int current = stack.Pop();
                resultado.Add(current);
                visitado[current] = true;

                if (current == end)
                {
                    break; // Se encontró el camino
                }

                foreach (int vecino in ListaAdyacencia[current].MuestraAristas())
                {
                    if (!visitado[vecino] && _ordenTopologico.IndexOf(vecino) > startIndex)
                    {
                        stack.Push(vecino);
                    }
                }
            }

            if (resultado[resultado.Count - 1] != end)
            {
                resultado.Clear(); // No se encontró un camino
            }

            return resultado;
        }

        public static List<int> Dijkstra(int start, int end)
        {
            try
            {
                int n = ListaAdyacencia.Count;
                float[] dist = new float[n];
                int[] prev = new int[n];
                bool[] visited = new bool[n];

                for (int i = 0; i < n; i++)
                {
                    dist[i] = float.MaxValue;
                    prev[i] = -1;
                    visited[i] = false;
                }

                dist[start] = 0;

                for (int i = 0; i < n; i++)
                {
                    int u = -1;
                    for (int j = 0; j < n; j++)
                    {
                        if (!visited[j] && (u == -1 || dist[j] < dist[u]))
                        {
                            u = j;
                        }
                    }

                    if (dist[u] == float.MaxValue)
                        break;

                    visited[u] = true;

                    foreach (int v in ListaAdyacencia[u].MuestraAristas())
                    {
                        float alt = dist[u] + ListaAdyacencia[u].ListaEnlaces.ObtenerCosto(v);
                        if (alt < dist[v])
                        {
                            dist[v] = alt;
                            prev[v] = u;
                        }
                    }
                }

                List<int> path = new List<int>();
                for (int at = end; at != -1; at = prev[at])
                {
                    path.Add(at);
                }
                path.Reverse();

                if (path[0] == start)
                    return path;
                else
                    throw new Exception("No se encontró un camino válido.");
            }
            catch 
            {
                return new List<int> { -1 }; // Devuelve una lista con un valor que indica error
            }
        }

        public static ciudad BuscarCiudadPorNombre(string nombreCiudad)
        {
            try
            {
                return ListaAdyacencia.Select(v => v.info).FirstOrDefault(c => c.NomCiudad.Equals(nombreCiudad, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar la ciudad por nombre: {ex.Message}");
                return null;
            }
        }

    }
}