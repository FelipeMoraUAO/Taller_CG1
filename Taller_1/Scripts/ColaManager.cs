using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColaManager : MonoBehaviour
{
    [SerializeField]

    public Cajero[] cajeros;

    private Queue<Cliente> cola = new Queue<Cliente>();
    private Coroutine productor;
    private int consecutivo = 1;

    // ?? Iniciar el ingreso de clientes
    public void Iniciar()
    {
        if (productor == null)
            productor = StartCoroutine(GenerarClientes());
    }

    // ?? Detener el ingreso de clientes
    public void Detener()
    {
        if (productor != null)
        {
            StopCoroutine(productor);
            productor = null;
        }
    }

    // ?? Generar clientes cada segundo
    IEnumerator GenerarClientes()
    {
        var wait = new WaitForSeconds(1f);
        while (true)
        {
            int n = Random.Range(1, 4); // entre 1 y 3 clientes
            for (int i = 0; i < n; i++)
            {
                string id = $"C{consecutivo++}";
                Tramite tr = (Random.value < 0.5f) ? Tramite.Retirar : Tramite.Consignar;
                float tAt = Random.Range(2f, 5f);

                Cliente c = new Cliente(id, "Cliente " + id, id + "@mail.com", "Calle X", tr, tAt);
                cola.Enqueue(c);

                Debug.Log($"? Entra {c.idCliente} - {c.tramite} - {c.tiempoAtencion:0.0}s");
            }
            yield return wait;
        }
    }

    // ?? M�todo para que un cajero saque un cliente de la cola
    public Cliente TryDequeue()
    {
        if (cola.Count > 0)
            return cola.Dequeue();
        return null;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inicializar cajeros
        for (int i = 0; i < cajeros.Length; i++)
        {
            cajeros[i].Init(this, "Cajero " + (i + 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
