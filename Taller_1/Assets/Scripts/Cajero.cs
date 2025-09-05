using UnityEngine;
using TMPro;

public class Cajero : MonoBehaviour
{
    string nombre;
    ColaManager manager; // Trae los clientes de la cola
    float tiempoRestante = 0;
    Cliente clienteActual = null; // Identifica si está atendiendo a algún cliente

    public TMP_Text estadoTexto; // Texto en la UI

    public void Init(ColaManager m, string n) // Llama con el Init al script ColaManager y lo pone a controlar al cajero y luego le asigna un nombre
    {
        manager = m;
        nombre = n;

        if (estadoTexto != null)
            estadoTexto.text = nombre + " Libre"; // Estado inicial
    }

    void Update()
    {
        // Si está atendiendo, descontar tiempo
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                Debug.Log(nombre + " terminó con " + clienteActual.idCliente);
                clienteActual = null;

                if (estadoTexto != null)
                    estadoTexto.text = nombre + " Libre";
            }
        }
        else
        {
            // Si no está atendiendo, intenta sacar un cliente de la cola
            Cliente c = manager.TryDequeue();
            if (c != null)
            {
                clienteActual = c;
                tiempoRestante = c.tiempoAtencion;
                Debug.Log(nombre + " atiende a " + c.idCliente + " (" + c.tramite + ")");

                if (estadoTexto != null)
                    estadoTexto.text = nombre + " Ocupado";
            }
        }
    }
}
