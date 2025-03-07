using System.Collections;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    [Header("Movimiento")]
    public float tiempoInmovilInicial = 2f;
    public float distanciaMovimiento = 3f;
    public float velocidadMovimiento = 2f;
    public float velocidadMovimientoFinal = 5f;
    public float tiempoEsperaDespuesPrimerMovimiento = 2f;
    public float tiempoMovimientoFinal = 3f;

    [Header("Referencias")]
    public Transform puntoReferencia;

    private Vector3 posicionInicial;
    private bool estaMoviendose = false;
    private int faseActual = 0;
    private float tiempoFase = 0f;
    private bool siguiendoReferencia = true;
    private bool haReiniciado = false;
    private Vector3 direccionMovimiento;
    private float tiempoMovimientoActual = 0f;

    void Start()
    {
        posicionInicial = transform.position;
        direccionMovimiento = transform.right;
    }

    void Update()
    {
        switch (faseActual)
        {
            case 0: 
                tiempoFase += Time.deltaTime;
                if (tiempoFase >= tiempoInmovilInicial)
                {
                    faseActual = 1;
                    estaMoviendose = true;
                    haReiniciado = false;
                }
                break;

            case 1: 
                if (siguiendoReferencia && puntoReferencia != null)
                {
                    Vector3 targetPosition = puntoReferencia.position + (direccionMovimiento * distanciaMovimiento);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, velocidadMovimiento * Time.deltaTime);
                }

                if (Vector3.Distance(posicionInicial, transform.position) >= distanciaMovimiento)
                {
                    StartCoroutine(EsperarDespuesPrimerMovimiento());
                }
                break;

            case 2: 
                if (siguiendoReferencia)
                {
                    siguiendoReferencia = false;
                }
                if (estaMoviendose)
                {
                    MoverEnemigoFinal();
                }
                break;
        }
    }

    private void MoverEnemigoFinal()
    {
        transform.position += direccionMovimiento * velocidadMovimientoFinal * Time.deltaTime;
        tiempoMovimientoActual += Time.deltaTime;

        if (tiempoMovimientoActual >= tiempoMovimientoFinal && !haReiniciado)
        {
            estaMoviendose = false;
            Invoke("TeletransportarAPuntoReferencia", 0.5f);
        }
    }

    private void TeletransportarAPuntoReferencia()
    {
        if (puntoReferencia != null)
        {
            transform.position = puntoReferencia.position;
            ReiniciarCiclo();
        }
        else
        {
            Debug.LogWarning("Punto de referencia no asignado.");
        }
    }

    private void ReiniciarCiclo()
    {
        if (haReiniciado) return;
        haReiniciado = true;
        faseActual = 0;
        tiempoFase = 0f;
        estaMoviendose = false;
        siguiendoReferencia = true;
        tiempoMovimientoActual = 0f;
    }

    private IEnumerator EsperarDespuesPrimerMovimiento()
    {
        yield return new WaitForSeconds(tiempoEsperaDespuesPrimerMovimiento);
        faseActual = 2;
        estaMoviendose = true;
    }
}
