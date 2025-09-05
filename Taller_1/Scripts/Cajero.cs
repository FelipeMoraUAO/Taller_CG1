using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cajero : MonoBehaviour
{
    [SerializeField]
    public string nombre;
    public bool ocupado = false;
    public int clientesAtendidos = 0;
    public List<float> tiemposAtencion = new List<float>();

    private ColaManager manager;

    public void Init(ColaManager colaManager, string nombreCajero)
    {
        manager = colaManager;
        nombre = nombreCajero;
        StartCoroutine(RoutineAtender());
    }

    private IEnumerator RoutineAtender()
    {
        while (true)
        {
            if (!ocupado)
            {
                Cliente c = manager.TryDequeue();
                if (c != null)
                {
                    ocupado = true;
                    Debug.Log($"{nombre} atiende a {c.idCliente} ({c.tramite})");

                    yield return new WaitForSeconds(c.tiempoAtencion);

                    clientesAtendidos++;
                    tiemposAtencion.Add(c.tiempoAtencion);

                    Debug.Log($"{nombre} terminó con {c.idCliente} en {c.tiempoAtencion:0.0}s");

                    ocupado = false;
                }
            }
            yield return null;
        }
    }

    public float TiempoTotal()
    {
        float total = 0;
        foreach (float t in tiemposAtencion) total += t;
        return total;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
