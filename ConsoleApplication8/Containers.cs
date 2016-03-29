
using System;

namespace ASD
{

public interface IContainer
    {
    void Put(int x);      //  dodaje element do kontenera

    int  Get();           //  zwraca pierwszy element kontenera i usuwa go z kontenera
                          //  w przypadku pustego kontenera zg�asza wyj�tek typu EmptyException (zdefiniowany w Lab01_Main.cs)

    int  Peek();          //  zwraca pierwszy element kontenera (ten, kt�ry b�dzie pobrany jako pierwszy),
                          //  ale pozostawia go w kontenerze (czyli nie zmienia zawarto�ci kontenera)
                          //  w przypadku pustego kontenera zg�asza wyj�tek typu EmptyException (zdefiniowany w Lab01_Main.cs)

    int  Count { get; }   //  zwraca liczb� element�w w kontenerze

    int  Size  { get; }   //  zwraca rozmiar kontenera (rozmiar wewn�tznej tablicy)
    }

public class Stack : IContainer
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
    // nie wolno dodawa� �adnych p�l ani innych sk�adowych

    public Stack(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
            // uzupe�ni�
            if (count >= tab.Length)
            {
                int[] tab2 = new int[tab.Length * 2];

                for (int i = 0; i < tab.Length; i++)
                    tab2[i] = tab[i];
                tab = tab2;
                
                tab[count] = x;
                count++;
            }
            else
            {
               
                tab[count] = x;
                count++;
            }
        }

    public int Get()
        {
            if (count == 0)
                throw new EmptyException();
          
            int tmp = tab[count-1];
            count--;

            return tmp; // zmieni�
        }

    public int Peek()
        {
            if (count == 0)
                throw new EmptyException();
            int tmp = tab[count-1];


            return tmp; // zmieni�
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class Stack


public class Queue : IContainer   //TODO SPRAWDZI�
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
                            // mo�na doda� jedno pole (wi�cej nie potrzeba)
        private int first;
    public Queue(int n=2)
        {
        tab = new int[n>2?n:2];
        first = 0;
        }

    public void Put(int x)
        {
            // uzupe�ni�
            if (count >= tab.Length )
            {
                int[] tab2 = new int[tab.Length * 2];

                for (int i = 0; i < tab.Length; i++)
                    tab2[i] = tab[(first + i) % tab.Length];
                tab = tab2;

                tab[count] = x;
                first = 0;
            }

            tab[(first + count) % tab.Length] = x;
            count++;

        }

    public int Get()
        {
            if (count == 0)
                throw new EmptyException();
            count--;
            int tmp = tab[first];
            if (first == (tab.Length - 1)) first = 0;
            else first = first + 1;
            
             
            
    
            return tmp; // zmieni�
        }

    public int Peek()
        {
            if (count == 0)
                throw new EmptyException();

            int tmp = tab[first];

            return tmp; // zmieni�
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class Queue


public class LazyPriorityQueue : IContainer
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
    // nie wolno dodawa� �adnych p�l ani innych sk�adowych

    public LazyPriorityQueue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
            // uzupe�ni�
            if (count >= tab.Length)
            {
                int[] tab2 = new int[tab.Length * 2];

                for (int i = 0; i < tab.Length; i++)
                    tab2[i] = tab[i];
                tab = tab2;

                tab[count] = x;
                count++;
            }
            else
            {

                tab[count] = x;
                count++;
            }
        }

    public int Get()
        {
            if (count == 0)
                throw new EmptyException();
            int index = 0;
            int max = tab[0];
            for(int i = 0;i < count;i++)
            {
                if (max < tab[i])
                {
                    index = i;
                    max = tab[i];
                }

            }
            int tmp = tab[index];
            
            for (int i = index; i < count; i++)
                tab[i] = tab[i + 1];
            count--;
            return tmp; // zmieni�
        }

    public int Peek()
        {
            if (count == 0)
                throw new EmptyException();
            int index = 0;
            int max = tab[0];
            for (int i = 0; i < count; i++)
            {
                if (max < tab[i])
                {
                    index = i;
                    max = tab[i];
                }
            }
            int tmp = tab[index];
           
            return tmp; // zmieni�
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class LazyPriorityQueue


public class HeapPriorityQueue : IContainer
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
    // nie wolno dodawa� �adnych p�l ani innych sk�adowych

    public HeapPriorityQueue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
    {
         
    }

    public int Get()
    {
                   
    }

        private void downHeap(int i)
        {
            int k = 2 * i;
            int v = tab[i];

            while(k <= count)
            {
                if (k + 1 <= count)
                {
                    if (tab[k + 1] > tab[k])
                        k = k + 1;
                        
                }

                if (tab[k] > v)
                {
                    tab[i] = tab[k];
                    i = k;
                    k = 2 * i;
                }
            }
            tab[i] = v;
    }
   private void upHeap(int i)
   {
        
         int v = tab[i];
         while (tab[i / 2] < v)
         {
                tab[i] = tab[i / 2];
                i /= 2; 
         }
            tab[i] = v;

   }
    public int Peek()
        {
            /*    if (count == 0)
                    throw new EmptyException();
                int index = 0;
                int max = tab[0];
                for (int i = 0; i < count; i++)
                {
                    if (max < tab[i])
                    {
                        index = i;
                        max = tab[i];
                    }
                }
                int tmp = tab[index];

                return tmp; // zmieni� */
            if (count == 0)
                throw new EmptyException();

            return tab[1];
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class HeapPriorityQueue

} // namespace ASD
