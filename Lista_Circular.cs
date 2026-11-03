using System;
using System.Collections.Generic;

// ==========================================
// 1. LISTA CIRCULAR: CAPITULOS DE LA NOVELA
// ==========================================

class ListaCircular<T>
{
    private class Nodo
    {
        public T Dato;
        public Nodo Siguiente;

        public Nodo(T dato)
        {
            Dato = dato;
        }
    }

    private Nodo ultimo;

    public void Insertar(T dato)
    {
        Nodo nuevo = new Nodo(dato);

        if (ultimo == null)
        {
            ultimo = nuevo;
            nuevo.Siguiente = nuevo;
        }
        else
        {
            nuevo.Siguiente = ultimo.Siguiente;
            ultimo.Siguiente = nuevo;
            ultimo = nuevo;
        }
    }

    public void Eliminar(T dato)
    {
        if (ultimo == null) return;

        Nodo actual = ultimo.Siguiente;
        Nodo anterior = ultimo;

        do
        {
            if (EqualityComparer<T>.Default.Equals(actual.Dato, dato))
            {
                if (actual == ultimo && actual.Siguiente == ultimo)
                {
                    ultimo = null;
                }
                else
                {
                    anterior.Siguiente = actual.Siguiente;

                    if (actual == ultimo)
                        ultimo = anterior;
                }

                Console.WriteLine("Capitulo eliminado.");
                return;
            }

            anterior = actual;
            actual = actual.Siguiente;

        } while (actual != ultimo.Siguiente);

        Console.WriteLine("Capitulo no encontrado.");
    }

    public void Mostrar()
    {
        if (ultimo == null)
        {
            Console.WriteLine("No hay capitulos.");
            return;
        }

        Nodo actual = ultimo.Siguiente;

        do
        {
            Console.WriteLine("- " + actual.Dato);
            actual = actual.Siguiente;

        } while (actual != ultimo.Siguiente);
    }

    public void MostrarSiguiente()
    {
        if (ultimo == null)
        {
            Console.WriteLine("No hay capitulos.");
            return;
        }

        ultimo = ultimo.Siguiente;
        Console.WriteLine("Siguiente capitulo: " + ultimo.Dato);
    }

    public void Buscar(T dato)
    {
        if (ultimo == null) return;

        Nodo actual = ultimo.Siguiente;

        do
        {
            if (EqualityComparer<T>.Default.Equals(actual.Dato, dato))
            {
                Console.WriteLine("Capitulo encontrado: " + actual.Dato);
                return;
            }

            actual = actual.Siguiente;

        } while (actual != ultimo.Siguiente);

        Console.WriteLine("Capitulo no encontrado.");
    }
}

// ==========================================
// 2. DICCIONARIO: PERSONAJES
// ==========================================

class Personajes
{
    private Dictionary<string, string> personajes =
        new Dictionary<string, string>();

    public void Agregar(string clave, string descripcion)
    {
        if (personajes.ContainsKey(clave))
        {
            Console.WriteLine("El personaje ya existe.");
            return;
        }

        personajes.Add(clave, descripcion);
        Console.WriteLine("Personaje agregado.");
    }

    public void Buscar(string clave)
    {
        if (personajes.TryGetValue(clave, out string descripcion))
            Console.WriteLine(clave + ": " + descripcion);
        else
            Console.WriteLine("Personaje no encontrado.");
    }

    public void Eliminar(string clave)
    {
        if (personajes.Remove(clave))
            Console.WriteLine("Personaje eliminado.");
        else
            Console.WriteLine("No existe ese personaje.");
    }

    public void Actualizar(string clave, string descripcion)
    {
        if (personajes.ContainsKey(clave))
        {
            personajes[clave] = descripcion;
            Console.WriteLine("Personaje actualizado.");
        }
        else
            Console.WriteLine("Personaje no encontrado.");
    }

    public void MostrarTodos()
    {
        foreach (var personaje in personajes)
        {
            Console.WriteLine(personaje.Key + " - " + personaje.Value);
        }
    }
}

// ==========================================
// 3. GRAFO: RELACIONES ENTRE PERSONAJES
// ==========================================

class Grafo
{
    private Dictionary<string, List<string>> adyacencia =
        new Dictionary<string, List<string>>();

    public void AgregarPersonaje(string personaje)
    {
        if (!adyacencia.ContainsKey(personaje))
            adyacencia[personaje] = new List<string>();
    }

    public void AgregarConexion(string origen, string destino)
    {
        AgregarPersonaje(origen);
        AgregarPersonaje(destino);

        if (!adyacencia[origen].Contains(destino))
            adyacencia[origen].Add(destino);

        if (!adyacencia[destino].Contains(origen))
            adyacencia[destino].Add(origen);

        Console.WriteLine("Conexion agregada.");
    }

    public void EliminarConexion(string origen, string destino)
    {
        if (adyacencia.ContainsKey(origen) &&
            adyacencia.ContainsKey(destino))
        {
            adyacencia[origen].Remove(destino);
            adyacencia[destino].Remove(origen);
            Console.WriteLine("Conexion eliminada.");
        }
        else
            Console.WriteLine("Conexion no encontrada.");
    }

    public void MostrarAdyacentes(string personaje)
    {
        if (adyacencia.ContainsKey(personaje))
        {
            Console.WriteLine("Relaciones de " + personaje + ":");

            foreach (string amigo in adyacencia[personaje])
                Console.WriteLine("- " + amigo);
        }
        else
            Console.WriteLine("Personaje no encontrado.");
    }

    public void BuscarCamino(string origen, string destino)
    {
        if (!adyacencia.ContainsKey(origen) ||
            !adyacencia.ContainsKey(destino))
        {
            Console.WriteLine("Personaje no encontrado.");
            return;
        }

        Queue<string> pendientes = new Queue<string>();
        Dictionary<string, string> anterior =
            new Dictionary<string, string>();

        HashSet<string> visitados = new HashSet<string>();

        pendientes.Enqueue(origen);
        visitados.Add(origen);

        while (pendientes.Count > 0)
        {
            string actual = pendientes.Dequeue();

            if (actual == destino)
            {
                List<string> camino = new List<string>();
                string paso = destino;

                while (paso != null)
                {
                    camino.Add(paso);
                    paso = anterior.ContainsKey(paso)
                        ? anterior[paso]
                        : null;
                }

                camino.Reverse();

                Console.WriteLine("Camino encontrado: " +
                    string.Join(" -> ", camino));
                return;
            }

            foreach (string vecino in adyacencia[actual])
            {
                if (!visitados.Contains(vecino))
                {
                    visitados.Add(vecino);
                    anterior[vecino] = actual;
                    pendientes.Enqueue(vecino);
                }
            }
        }

        Console.WriteLine("No existe un camino entre ellos.");
    }

    public void MostrarGrafo()
    {
        foreach (var personaje in adyacencia)
        {
            Console.WriteLine(personaje.Key + " -> " +
                string.Join(", ", personaje.Value));
        }
    }
}

// ==========================================
// 4. COLA: ACTIVIDADES UNIVERSITARIAS
// ==========================================

class Actividades
{
    private Queue<string> actividades = new Queue<string>();

    public void Encolar(string actividad)
    {
        actividades.Enqueue(actividad);
        Console.WriteLine("Actividad agregada a la lista.");
    }

    public void Desencolar()
    {
        if (actividades.Count > 0)
            Console.WriteLine("Actividad realizada: " +
                actividades.Dequeue());
        else
            Console.WriteLine("No hay actividades pendientes.");
    }

    public void VerPrimero()
    {
        if (actividades.Count > 0)
            Console.WriteLine("Siguiente actividad: " +
                actividades.Peek());
        else
            Console.WriteLine("No hay actividades.");
    }

    public void MostrarTodas()
    {
        if (actividades.Count == 0)
        {
            Console.WriteLine("No hay actividades pendientes.");
            return;
        }

        foreach (string actividad in actividades)
            Console.WriteLine("- " + actividad);
    }

    public bool EstaVacia()
    {
        return actividades.Count == 0;
    }
}

// ==========================================
// 5. PILA: RECUERDOS Y SECRETOS
// ==========================================

class Recuerdos
{
    private Stack<string> recuerdos = new Stack<string>();

    public void Push(string recuerdo)
    {
        recuerdos.Push(recuerdo);
        Console.WriteLine("Recuerdo guardado.");
    }

    public void Pop()
    {
        if (recuerdos.Count > 0)
            Console.WriteLine("Recuerdo eliminado: " +
                recuerdos.Pop());
        else
            Console.WriteLine("No hay recuerdos.");
    }

    public void Peek()
    {
        if (recuerdos.Count > 0)
            Console.WriteLine("Ultimo recuerdo: " +
                recuerdos.Peek());
        else
            Console.WriteLine("No hay recuerdos.");
    }

    public void MostrarTodos()
    {
        foreach (string recuerdo in recuerdos)
            Console.WriteLine("- " + recuerdo);
    }

    public bool EstaVacia()
    {
        return recuerdos.Count == 0;
    }
}

// ==========================================
// PROGRAMA PRINCIPAL: NOVELA AUDIOVISUAL
// ==========================================

class Program
{
    static ListaCircular<string> capitulos = new ListaCircular<string>();
    static Personajes personajes = new Personajes();
    static Grafo relaciones = new Grafo();
    static Actividades actividades = new Actividades();
    static Recuerdos recuerdos = new Recuerdos();

    static void Main()
    {
        Inicializar();

        Console.WriteLine("==================================");
        Console.WriteLine("   ENTRE CLASES Y CORAZONES");
        Console.WriteLine("==================================");

        Console.WriteLine("\nBienvenida a la universidad.");
        Console.WriteLine("Tu historia esta a punto de comenzar...");

        Historia();

        int opcion;

        do
        {
            Console.WriteLine("\n========== MENU PRINCIPAL ==========");
            Console.WriteLine("1. Ver capitulos");
            Console.WriteLine("2. Avanzar capitulo");
            Console.WriteLine("3. Consultar personajes");
            Console.WriteLine("4. Ver relaciones");
            Console.WriteLine("5. Ver actividades universitarias");
            Console.WriteLine("6. Ver recuerdos");
            Console.WriteLine("0. Salir");
            Console.Write("Selecciona una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
                opcion = -1;

            switch (opcion)
            {
                case 1:
                    capitulos.Mostrar();
                    break;

                case 2:
                    capitulos.MostrarSiguiente();
                    break;

                case 3:
                    personajes.MostrarTodos();
                    break;

                case 4:
                    relaciones.MostrarGrafo();
                    break;

                case 5:
                    actividades.MostrarTodas();
                    break;

                case 6:
                    recuerdos.MostrarTodos();
                    break;

                case 0:
                    Console.WriteLine("Gracias por jugar. Fin de la historia.");
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

        } while (opcion != 0);
    }

    static void Inicializar()
    {
        // Lista circular
        capitulos.Insertar("Capitulo 1: Un nuevo comienzo");
        capitulos.Insertar("Capitulo 2: Un encuentro inesperado");
        capitulos.Insertar("Capitulo 3: Entre dos corazones");
        capitulos.Insertar("Capitulo 4: Una decision dificil");

        // Diccionario
        personajes.Agregar("P001", "Protagonista: estudiante universitaria");
        personajes.Agregar("P002", "Alex: divertido y extrovertido");
        personajes.Agregar("P003", "Daniel: reservado e inteligente");
        personajes.Agregar("P004", "Sofia: mejor amiga de la protagonista");

        // Grafo
        relaciones.AgregarConexion("Protagonista", "Alex");
        relaciones.AgregarConexion("Protagonista", "Daniel");
        relaciones.AgregarConexion("Protagonista", "Sofia");
        relaciones.AgregarConexion("Alex", "Sofia");
        relaciones.AgregarConexion("Daniel", "Sofia");

        // Cola
        actividades.Encolar("Inscripcion universitaria");
        actividades.Encolar("Clase de programacion");
        actividades.Encolar("Estudiar con Daniel");
        actividades.Encolar("Salir a la cafeteria con Alex");

        // Pila
        recuerdos.Push("Primer dia en la universidad.");
        recuerdos.Push("Alex me regalo una flor.");
        recuerdos.Push("Daniel me ayudo con mi proyecto.");
    }

    static void Historia()
    {
        Console.WriteLine("\nCAPITULO 1: UN NUEVO COMIENZO");
        Console.WriteLine("Llegas a la universidad y conoces a dos chicos.");

        Console.WriteLine("\nAlex: ¡Hola! ¿Eres nueva por aqui?");
        Console.WriteLine("Daniel: Si necesitas ayuda, puedo mostrarte la biblioteca.");

        Console.WriteLine("\n¿A quien decides acercarte?");
        Console.WriteLine("1. Alex");
        Console.WriteLine("2. Daniel");

        Console.Write("Tu decision: ");
        string decision = Console.ReadLine();

        if (decision == "1")
        {
            Console.WriteLine("\nElegiste a Alex.");
            Console.WriteLine("Alex te invita a conocer la cafeteria.");
            recuerdos.Push("Mi primera salida con Alex.");
            actividades.Encolar("Cita en la cafeteria con Alex");
            Console.WriteLine("Tu relacion con Alex comienza a crecer.");
        }
        else if (decision == "2")
        {
            Console.WriteLine("\nElegiste a Daniel.");
            Console.WriteLine("Daniel te invita a estudiar en la biblioteca.");
            recuerdos.Push("Mi primera tarde de estudio con Daniel.");
            actividades.Encolar("Estudiar en la biblioteca con Daniel");
            Console.WriteLine("Tu relacion con Daniel comienza a crecer.");
        }
        else
        {
            Console.WriteLine("\nNo tomaste una decision.");
            Console.WriteLine("La historia continua...");
        }

        Console.WriteLine("\nFIN DEL PRIMER CAPITULO.");
    }
}