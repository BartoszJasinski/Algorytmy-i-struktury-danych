
using System;
using ASD.Graphs;

namespace ASD.Lab03
{

    public static class Lab03GraphExtender
    {

        // Część 1
        // Wyznaczanie odwrotności grafu
        //   0.5 pkt
        // Odwrotność grafu to graf skierowany o wszystkich krawędziach przeciwnie skierowanych niż w grafie pierwotnym
        // Parametry:
        //   g - graf wejściowy
        // Wynik:
        //   odwrotność grafu
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu nieskierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Graf wynikowy musi być w takiej samej reprezentacji jak wejściowy
        public static Graph Lab03Reverse(this Graph g)
        {
            if (!g.Directed)
                throw new Lab03Exception();
            Graph a = g.IsolatedVerticesGraph();

            for (int i = 0; i < g.VerticesCount; i++)
            {
                foreach (var e in g.OutEdges(i))
                {
                    a.AddEdge(e.To, e.From, e.Weight);
                }
            }

            return a;  // zmienić
        }

        // Część 2
        // Badanie czy graf jest dwudzielny
        //   1.5 pkt
        // Graf dwudzielny to graf nieskierowany, którego wierzchołki można podzielić na dwa rozłączne zbiory
        // takie, że dla każdej krawędzi jej końce należą do róźnych zbiorów
        // Parametry:
        //   g - badany graf
        //   vert - tablica opisująca podział zbioru wierzchołków na podzbiory w następujący sposób
        //          vert[i] == 1 oznacza, że wierzchołek i należy do pierwszego podzbioru
        //          vert[i] == 2 oznacza, że wierzchołek i należy do drugiego podzbioru
        // Wynik:
        //   true jeśli graf jest dwudzielny, false jeśli graf nie jest dwudzielny (w tym przypadku parametr vert ma mieć wartość null)
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu skierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Podział wierzchołków może nie być jednoznaczny - znaleźć dowolny
        //   4) Pamiętać, że każdy z wierzchołków musi być przyporządkowany do któregoś ze zbiorów
        //   5) Metoda ma mieć taki sam rząd złożoności jak zwykłe przeszukiwanie (za większą będą kary!)
        public static bool Lab03IsBipartite(this Graph g, out int[] vert)
        {
            if (g.Directed)
                throw new Lab03Exception();

            int cc = 0;
            int[] t = new int[g.VerticesCount];
            vert = new int[g.VerticesCount];  // zmienić
            for (int i = 0; i < g.VerticesCount; i++)
                t[i] = 0;

            Predicate<Edge> bipar = delegate (Edge e)
           {
               if (t[e.From] == 0 && t[e.To] == 0)
               {
                   t[e.From] = 1;
                   t[e.To] = 2;
                   return true;
               }
               else if (t[e.From] != t[e.To])
               {
                   if (t[e.To] == 0)
                       t[e.To] = 3 - t[e.From];
                   if (t[e.From] == 0)
                       t[e.From] = 3 - t[e.To];
                   return true;
               }

               return false;
           };

            

            if (g.GeneralSearchAll<EdgesStack>(null, bipar, out cc) == false)
            {
                vert = null;
                return false;
            }
            vert = t;
            for (int i = 0; i < g.VerticesCount; i++)
            {
                if (vert[i] == 0)
                    vert[i] = 1;
            }
            return true;  // zmienić
        }

        // Część 3
        // Wyznaczanie minimalnego drzewa rozpinającego algorytmem Kruskala
        //   1.5 pkt
        // Schemat algorytmu Kruskala
        //   1) wrzucić wszystkie krawędzie do "wspólnego worka"
        //   2) wyciągać z "worka" krawędzie w kolejności wzrastających wag
        //      - jeśli krawędź można dodać do drzewa to dodawać, jeśli nie można to ignorować
        //      - punkt 2 powtarzać aż do skonstruowania drzewa (lub wyczerpania krawędzi)
        // Parametry:
        //   g - graf wejściowy
        //   mstw - waga skonstruowanego drzewa (lasu)
        // Wynik:
        //   skonstruowane minimalne drzewo rozpinające (albo las)
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu skierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Wykorzystać klasę UnionFind z biblioteki Graph
        //   4) Jeśli graf g jest niespójny to metoda wyznacza las rozpinający
        //   5) Graf wynikowy (drzewo) musi być w takiej samej reprezentacji jak wejściowy
        
        public static Graph Lab03Kruskal(this Graph g, out int mstw)
        {
            if (g.Directed)
                throw new Lab03Exception();
            mstw = 0;       // zmienić
            Graph a = g.IsolatedVerticesGraph();
            int counter = g.VerticesCount;
            UnionFind uf = new UnionFind(g.VerticesCount);
           
            EdgesMinPriorityQueue pq = new EdgesMinPriorityQueue();

            for (int i = 0; i < a.VerticesCount; i++)
            {
                foreach (var e in g.OutEdges(i))
                {
                    if (e.To < e.From)
                        pq.Put(e);
                }
            }

            while (!pq.Empty && counter > 1)
            {
                Edge e = pq.Get();
                if (uf.Union(e.From, e.To))
                {
                    counter--;
                    a.AddEdge(e);
                    mstw += e.Weight;
                }
                
            }
                return a;  // zmienić
        }

        // Część 4
        // Badanie czy graf nieskierowany jest acykliczny
        //   0.5 pkt
        // Parametry:
        //   g - badany graf
        // Wynik:
        //   true jeśli graf jest acykliczny, false jeśli graf nie jest acykliczny
        // Uwagi:
        //   1) Metoda uruchomiona dla grafu skierowanego powinna zgłaszać wyjątek Lab03Exception
        //   2) Graf wejściowy pozostaje niezmieniony
        //   3) Najpierw pomysleć jaki, prosty do sprawdzenia, warunek spełnia acykliczny graf nieskierowany
        //      Zakodowanie tefo sprawdzenia nie powinno zająć więcej niż kilka linii!
        //      Zadanie jest bardzo łatwe (jeśli wydaje się trudne - poszukać prostszego sposobu, a nie walczyć z trudnym!)
        public static bool Lab03IsUndirectedAcyclic(this Graph g)
        {
            if (g.Directed)
                throw new Lab03Exception();
             if (g.EdgesCount >= g.VerticesCount)
                return false;
            int cc;
            bool b = g.GeneralSearchAll<EdgesStack>(null, null, out cc);
            if (g.VerticesCount - cc == g.EdgesCount) return true;
            return false;  // zmienić
        }

    }

}
