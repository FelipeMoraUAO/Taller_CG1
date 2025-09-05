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
}

