using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ColaManager : MonoBehaviour
{
    public Cajero[] cajeros;
    public Button BtnIniciar;  // Botón de iniciar
    public Button BtnDetener;  // Botón de detener

    Queue<Cliente> cola = new Queue<Cliente>(); //Crea la cola
    bool generando = false; //Genera los clientes (inicia en false para poder dar inicio con los btones
    int consecutivo = 1; //Contador de clientes en la cola

    void Start()
    {
        // Conectar botones con unity
        BtnIniciar.onClick.AddListener(Iniciar); //onClick (cuando haga click) y addLis... (ejecutar metodo x)
        BtnDetener.onClick.AddListener(Detener);//onClick (cuando haga click) y addLis... (ejecutar metodo x)

        // Dar nombres a los cajeros
        for (int i = 0; i < cajeros.Length; i++) //Recorre a todos los cajeros. El Length es la cantidad total del arreglo
        {
            cajeros[i].Init(this, "Cajero " + (i + 1));
        }
    }

    void Update()
    {
        // Si está activado, generar clientes cada cierto tiempo
        if (generando && Time.frameCount % 120 == 0) // cada ~2 segundos (120 frames = 2 seg si el juego esta a 60 frames/seg
        {
            string id = "Cliente" + consecutivo++; //Crea un nombre para cada cliente "Cliente 1" etc
            Tramite tr = (Random.value < 0.5f) ? Tramite.Retirar : Tramite.Consignar; // Genera valor aleatorio ente 0 y 1 (x<0,5 retira y x>0,5 consigna
            float tAt = Random.Range(2f, 5f); //Crea un valor que determina el tiempo de atencion al cliente entre 2 a 5 seg

            Cliente c = new Cliente(id, "Cliente " + id, id + "@mail.com", "Calle X", tr, tAt); //Se instacion nuevo cliente con sus datos
            cola.Enqueue(c); //Se mete el cliente a la cola

            Debug.Log("Entra " + c.idCliente + " - " + tr + " - " + tAt.ToString("0.0") + "s"); //Mensaje de consola
        }
    }

    // Encender generación
    public void Iniciar()
    {
        generando = true;
    }

    // Apagar generación
    public void Detener()
    {
        generando = false;
    }

    // Cajero pide un cliente
    public Cliente TryDequeue()
    {
        if (cola.Count > 0)
            return cola.Dequeue();
        return null;
    }
}
