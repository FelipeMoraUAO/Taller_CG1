using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ColaManager : MonoBehaviour
{
    public Cajero[] cajeros;
    public Button BtnIniciar;  // Botón de iniciar
    public Button BtnDetener;  // Botón de detener

    Queue<Cliente> cola = new Queue<Cliente>(); // Crea la cola
    bool generando = false; // Controla si se están generando clientes (inicia en false para arrancar con los botones)
    int consecutivo = 1; // Contador de clientes en la cola

    public TMP_Text estadoCola; // Texto UI para mostrar clientes en cola

    void Start()
    {
        // Conectar botones con Unity
        BtnIniciar.onClick.AddListener(Iniciar);
        BtnDetener.onClick.AddListener(Detener);

        // Dar nombres a los cajeros
        for (int i = 0; i < cajeros.Length; i++)
        {
            cajeros[i].Init(this, "Cajero " + (i + 1));
        }

        // Estado inicial de la cola
        ActualizarTextoCola();
    }

    void Update()
    {
        // Si está activado, generar clientes cada cierto tiempo
        if (generando && Time.frameCount % 120 == 0) // cada ~2 segundos
        {
            string id = "Cliente " + consecutivo++;
            Tramite tr = (Random.value < 0.5f) ? Tramite.Retirar : Tramite.Consignar;
            float tAt = Random.Range(2f, 5f);

            Cliente c = new Cliente(id, id, id + "@mail.com", "Calle X", tr, tAt);
            cola.Enqueue(c);

            Debug.Log("Entra " + c.idCliente + " - " + tr + " - " + tAt.ToString("0.0") + "s");

            // Actualizar cantidad en el panel
            ActualizarTextoCola();
        }
    }

    // Actualizar el texto con la cantidad de clientes
    public void ActualizarTextoCola()
    {
        if (estadoCola == null) return;

        int cantidad = cola.Count;

        if (cantidad == 0)
            estadoCola.text = "Cola vacia";
        else
            estadoCola.text = "Clientes en cola: " + cantidad;
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
        {
            Cliente clienteAtendido = cola.Dequeue();
            ActualizarTextoCola(); // Refrescar después de sacar un cliente
            return clienteAtendido;
        }
        return null;
    }
}
