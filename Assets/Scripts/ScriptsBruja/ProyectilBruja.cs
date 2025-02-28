using UnityEngine;

public class ProyectilBruja : MonoBehaviour
{
    public float velocidad = 5f;
    private Vector2 direccion;

    public void Inicializar(Vector2 direccionObjetivo)
    {
        direccion = direccionObjetivo.normalized; // Guardamos la dirección fija
    }

    void Update()
    {
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruir el proyectil al impactar
        }
        else if (collision.CompareTag("Limite")) // Evita que el proyectil atraviese paredes
        {
            Destroy(gameObject);
        }
    }
}
