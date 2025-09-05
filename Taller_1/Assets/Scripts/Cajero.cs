using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // 👈 agregado para poder mostrar estado en UI (si lo necesitas después)

public class Cajero : MonoBehaviour
{
    [SerializeField]
    public string nombre;
    public bool ocupado = false;
    public int clientesAtendidos = 0;
    public List<float> tiemposAtencion = new List<float>();

    private ColaManager manager;

    // 👇 agregado: referencia opcional a un texto UI para mostrar estado
    public Text estadoTexto;

    public void Init(ColaManager colaManager, string nombreCajero)
    {
        manager = colaManager;
        nombre = nombreCajero;
        StartCoroutine(RoutineAtender());

        // 👇 agregado: si hay texto, mostrar estado inicial
        if (estadoTexto != null)
            estadoTexto.text = $"{nombre} (Libre)";
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

                    if (estadoTexto != null)
                        estadoTexto.text = $"{nombre} (Ocupado)";

                    yield return new WaitForSeconds(c.tiempoAtencion);

                    clientesAtendidos++;
                    tiemposAtencion.Add(c.tiempoAtencion);

                    Debug.Log($"{nombre} terminó con {c.idCliente} en {c.tiempoAtencion:0.0}s");

                    ocupado = false;

                    if (estadoTexto != null)
                        estadoTexto.text = $"{nombre} (Libre)";
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

    void Start()
    {
        // nada por ahora
    }

    void Update()
    {
        // nada por ahora
    }
}
