
using System;
using System.Collections.Generic;

namespace EstructurasDeDatos
{
    // Nodo para la lista enlazada simple
    class Nodo
    {
        public int Dato;
        public Nodo Siguiente;

        public Nodo(int dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    // Implementación de una lista enlazada simple
    class ListaEnlazada
    {
        private Nodo cabeza;

        public void Agregar(int dato)
        {
            Nodo nuevo = new Nodo(dato);

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo actual = cabeza;

                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }

                actual.Siguiente = nuevo;
            }

            Console.WriteLine("Dato agregado a la lista.");
        }

        public void Mostrar()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista esta vacia.");
                return;
            }

            Nodo actual = cabeza;
            Console.Write("Lista: ");

            while (actual != null)
            {
                Console.Write(actual.Dato + " -> ");
                actual = actual.Siguiente;
            }

            Console.WriteLine("NULL");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaEnlazada lista = new ListaEnlazada();

            // Matriz de 2 filas y 3 columnas
            int[,] matriz = new int[2, 3];

            // Pila de enteros
            Stack<int> pila = new Stack<int>();

            int opcion;

            do
            {
                Console.WriteLine("\n===== MENU PRINCIPAL =====");
                Console.WriteLine("1. Lista enlazada simple");
                Console.WriteLine("2. Matriz bidimensional");
                Console.WriteLine("3. Pila Stack<T>");
                Console.WriteLine("4. Salir");
                Console.Write("Selecciona una opcion: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Ingresa una opcion numerica.");
                    opcion = 0;
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        MenuLista(lista);
                        break;

                    case 2:
                        MenuMatriz(matriz);
                        break;

                    case 3:
                        MenuPila(pila);
                        break;

                    case 4:
                        Console.WriteLine("Programa finalizado.");
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }

            } while (opcion != 4);
        }

        // Menu de la lista enlazada
        static void MenuLista(ListaEnlazada lista)
        {
            Console.Write("Ingresa un numero entero: ");

            if (int.TryParse(Console.ReadLine(), out int dato))
            {
                lista.Agregar(dato);
                lista.Mostrar();
            }
            else
            {
                Console.WriteLine("Dato no valido.");
            }
        }

        // Menu de la matriz bidimensional
        static void MenuMatriz(int[,] matriz)
        {
            Console.WriteLine("\nCaptura los datos de la matriz:");

            for (int fila = 0; fila < matriz.GetLength(0); fila++)
            {
                for (int columna = 0;
                     columna < matriz.GetLength(1); columna++)
                {
                    Console.Write("Fila " + fila +
                                  ", columna " + columna + ": ");

                    while (!int.TryParse(Console.ReadLine(),
                                         out matriz[fila, columna]))
                    {
                        Console.Write("Ingresa un numero entero: ");
                    }
                }
            }

            Console.WriteLine("\nContenido de la matriz:");

            for (int fila = 0; fila < matriz.GetLength(0); fila++)
            {
                for (int columna = 0;
                     columna < matriz.GetLength(1); columna++)
                {
                    Console.Write(matriz[fila, columna] + "\t");
                }

                Console.WriteLine();
            }
        }

        // Menu de la pila
        static void MenuPila(Stack<int> pila)
        {
            Console.WriteLine("\n===== MENU DE PILA =====");
            Console.WriteLine("1. Apilar un elemento (Push)");
            Console.WriteLine("2. Desapilar un elemento (Pop)");
            Console.WriteLine("3. Consultar el elemento superior (Peek)");
            Console.WriteLine("4. Mostrar la pila");
            Console.Write("Selecciona una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                Console.WriteLine("Opcion no valida.");
                return;
            }

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingresa un numero: ");

                    if (int.TryParse(Console.ReadLine(), out int dato))
                    {
                        pila.Push(dato);
                        Console.WriteLine("Elemento agregado a la pila.");
                    }
                    else
                    {
                        Console.WriteLine("Dato no valido.");
                    }
                    break;

                case 2:
                    if (pila.Count > 0)
                    {
                        Console.WriteLine("Elemento eliminado: "
                                          + pila.Pop());
                    }
                    else
                    {
                        Console.WriteLine("La pila esta vacia.");
                    }
                    break;

                case 3:
                    if (pila.Count > 0)
                    {
                        Console.WriteLine("Elemento superior: "
                                          + pila.Peek());
                    }
                    else
                    {
                        Console.WriteLine("La pila esta vacia.");
                    }
                    break;

                case 4:
                    if (pila.Count == 0)
                    {
                        Console.WriteLine("La pila esta vacia.");
                    }
                    else
                    {
                        Console.WriteLine("Elementos de arriba hacia abajo:");

                        foreach (int elemento in pila)
                        {
                            Console.WriteLine(elemento);
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }
        }
    }
}
