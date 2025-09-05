using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ColaManager : MonoBehaviour
{
    public Cajero[] cajeros;   // Arrastrar en el Inspector
    public Button BtnIniciar;  // Botón iniciar
    public Button BtnDetener;  // Botón detener

    Queue<Cliente> cola = new Queue<Cliente>();
    bool generando = false;
    int consecutivo = 1;

    void Start()
    {
        // Conectar botones
        BtnIniciar.onClick.AddListener(Iniciar);
        BtnDetener.onClick.AddListener(Detener);

        // Dar nombres a los cajeros
        for (int i = 0; i < cajeros.Length; i++)
        {
            cajeros[i].Init(this, "Cajero " + (i + 1));
        }
    }

    void Update()
    {
        // Si está activado, generar clientes cada cierto tiempo
        if (generando && Time.frameCount % 120 == 0) // cada ~2 segundos
        {
            string id = "C" + consecutivo++;
            Tramite tr = (Random.value < 0.5f) ? Tramite.Retirar : Tramite.Consignar;
            float tAt = Random.Range(2f, 5f);

            Cliente c = new Cliente(id, "Cliente " + id, id + "@mail.com", "Calle X", tr, tAt);
            cola.Enqueue(c);

            Debug.Log("➡ Entra " + c.idCliente + " - " + tr + " - " + tAt.ToString("0.0") + "s");
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
