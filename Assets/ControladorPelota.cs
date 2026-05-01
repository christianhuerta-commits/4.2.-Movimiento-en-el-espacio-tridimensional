using UnityEngine;
using TMPro;

public class ControladorPelota : MonoBehaviour
{
    private Rigidbody rb;
    public float fuerzaImpulso = 5f; 
    public float aumentoVelocidad = 1.5f; 
    
    private int contadorRebotes = 0;
    private int nivelActual = 1;

    public TMP_Text textoPrincipal; // El que sube 1, 2, 3...
    public TMP_Text textoNivel;     // El que dice "¡NIVEL SUPERADO!"

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(textoNivel != null) textoNivel.text = ""; // Empezar vacío
        ActualizarInterfaz();
        LanzarPelota();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica que tu suelo se llame "Plane" exactamente
        if (collision.gameObject.name == "Plane")
        {
            contadorRebotes++;
            
            // Lógica de cada 10 rebotes
            if (contadorRebotes % 10 == 0)
            {
                if(textoNivel != null) textoNivel.text = "¡NIVEL " + nivelActual + " SUPERADO!";
                
                nivelActual++;
                fuerzaImpulso += aumentoVelocidad; 
                
                // Borrar el mensaje tras 2 segundos
                Invoke("LimpiarMensaje", 2f);
            }

            rb.linearVelocity = Vector3.zero; 
            rb.AddForce(Vector3.up * fuerzaImpulso, ForceMode.Impulse);

            ActualizarInterfaz();
        }
    }

    void ActualizarInterfaz()
    {
        if(textoPrincipal != null) 
            textoPrincipal.text = "Rebotes: " + contadorRebotes;
    }

    void LimpiarMensaje()
    {
        if(textoNivel != null) textoNivel.text = "";
    }

    void LanzarPelota()
    {
        Vector3 direccion = new Vector3(Random.Range(-0.5f, 0.5f), -1f, Random.Range(-0.5f, 0.5f)).normalized;
        rb.AddForce(direccion * fuerzaImpulso, ForceMode.Impulse);
    }
}