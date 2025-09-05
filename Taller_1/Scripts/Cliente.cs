using UnityEngine;

public enum Tramite { Retirar, Consignar }
[System.Serializable]

public class Cliente : Persona
{
    public string idCliente;
    public Tramite tramite;
    public float tiempoAtencion;

    public Cliente(string idCliente, string nombre, string correo, string direccion, Tramite tramite, float tiempoAtencion)
        : base(nombre, correo, direccion)
    {
        this.idCliente = idCliente;
        this.tramite = tramite;
        this.tiempoAtencion = tiempoAtencion;
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
