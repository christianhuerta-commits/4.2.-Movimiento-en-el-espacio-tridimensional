using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class ControladorPelota : MonoBehaviour
{
    private Rigidbody rb;
    public float fuerzaInicial = 5f; // Fuerza con la que empezará a moverse
    private int contadorRebotes = 0;
    public TMP_Text textoRebotes; // Referencia al texto de la UI

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Dar un empujón inicial diagonal para que no caiga recta
        Vector3 direccionInicial = new Vector3(Random.Range(-1f, 1f), -1f, Random.Range(-1f, 1f)).normalized;
        rb.AddForce(direccionInicial * fuerzaInicial, ForceMode.Impulse);
    }

    // Esta función se ejecuta cuando la pelota choca con algo
    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si chocó con el plano (Suelo)
        // Nota: Asegúrate de que tu objeto Plano en la jerarquía tenga el nombre "Plane"
        if (collision.gameObject.name == "Plane")
        {
            contadorRebotes++;
            ActualizarTexto();
        }
    }

    void ActualizarTexto()
    {
        textoRebotes.text = "Rebotes: " + contadorRebotes;
    }
}