using System;

namespace EstructurasDeDatos
{
    public class NodoDoble
    {
        public int Dato;
        public NodoDoble Anterior;
        public NodoDoble Siguiente;

        public NodoDoble(int dato)
        {
            Dato = dato;
            Anterior = null;
            Siguiente = null;
        }
    }

    public class ListaDobleEnlazada
    {
        private NodoDoble cabeza;
        private NodoDoble cola;

        public ListaDobleEnlazada()
        {
            cabeza = null;
            cola = null;
        }

        public void AgregarInicio(int dato)
        {
            NodoDoble nuevo = new NodoDoble(dato);

            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
                return;
            }

            nuevo.Siguiente = cabeza;
            cabeza.Anterior = nuevo;
            cabeza = nuevo;
        }

        public void AgregarFinal(int dato)
        {
            NodoDoble nuevo = new NodoDoble(dato);

            if (cola == null)
            {
                cabeza = nuevo;
                cola = nuevo;
                return;
            }

            nuevo.Anterior = cola;
            cola.Siguiente = nuevo;
            cola = nuevo;
        }

        public void MostrarAdelante()
        {
            Console.WriteLine("Recorrido hacia adelante:");
            NodoDoble actual = cabeza;

            while (actual != null)
            {
                Console.Write(actual.Dato + " <-> ");
                actual = actual.Siguiente;
            }

            Console.WriteLine("NULL");
        }

        public void MostrarAtras()
        {
            Console.WriteLine("Recorrido hacia atrás:");
            NodoDoble actual = cola;

            while (actual != null)
            {
                Console.Write(actual.Dato + " <-> ");
                actual = actual.Anterior;
            }

            Console.WriteLine("NULL");
        }

        public bool Buscar(int dato)
        {
            NodoDoble actual = cabeza;

            while (actual != null)
            {
                if (actual.Dato == dato)
                    return true;

                actual = actual.Siguiente;
            }

            return false;
        }

        public void Eliminar(int dato)
        {
            NodoDoble actual = cabeza;

            while (actual != null)
            {
                if (actual.Dato == dato)
                {
                    if (actual.Anterior != null)
                        actual.Anterior.Siguiente = actual.Siguiente;
                    else
                        cabeza = actual.Siguiente;

                    if (actual.Siguiente != null)
                        actual.Siguiente.Anterior = actual.Anterior;
                    else
                        cola = actual.Anterior;

                    Console.WriteLine($"Se eliminó el dato {dato}");
                    return;
                }

                actual = actual.Siguiente;
            }

            Console.WriteLine($"El dato {dato} no existe en la lista.");
        }
    }
}