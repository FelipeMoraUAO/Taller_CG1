using UnityEngine;

public class Cajero : MonoBehaviour
{
    string nombre;
    ColaManager manager;
    float tiempoRestante = 0;
    Cliente clienteActual = null;

    public void Init(ColaManager m, string n)
    {
        manager = m;
        nombre = n;
    }

    void Update()
    {
        // Si est� atendiendo, descontar tiempo
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                Debug.Log(nombre + " termin� con " + clienteActual.idCliente);
                clienteActual = null;
            }
        }
        else
        {
            // Si est� libre, pide cliente
            Cliente c = manager.TryDequeue();
            if (c != null)
            {
                clienteActual = c;
                tiempoRestante = c.tiempoAtencion;
                Debug.Log(nombre + " atiende a " + c.idCliente + " (" + c.tramite + ")");
            }
        }
    }
}
