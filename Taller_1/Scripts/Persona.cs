using UnityEngine;

public class Persona : MonoBehaviour
{
    [SerializeField]
    public string nombre;
    public string correo;
    public string direccion;

    public Persona(string nombre, string correo, string direccion)
    {
        this.nombre = nombre;
        this.correo = correo;
        this.direccion = direccion;
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
