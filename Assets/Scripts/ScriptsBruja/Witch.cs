using UnityEngine;
//CODIGO HECHO POR IA por los de diseño, NO POR JAIME, asi como la ilustracion fue hecha por ia, no se 
public class Witch : MonoBehaviour
{
    public GameObject jugador;
    public float distanciaDeseada = 5f;

    public float velocidadAcercarse = 2f;  
    public float velocidadAlejarse = 1f;   

    public float tiempoEntreAtaques = 2f;
    public float tiempoEntreDisparos = 3f;

    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public GameObject golpeObjeto; 
    public float tiempoGolpeActivo = 0.5f;

    public HP_Enemy _hpEnemy;
    public float TakeDamageTimer;
    public float TakeDamageCD;

    private float tiempoUltimoAtaque;
    private float tiempoUltimoDisparo;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 direccionMovimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (golpeObjeto != null)
        {
            golpeObjeto.SetActive(false); 
        }
    }

    void FixedUpdate()
    {
        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player");
        }

        float distanciaActual = Vector2.Distance(transform.position, jugador.transform.position);
        float velocidadActual = 0f;

        if (distanciaActual > distanciaDeseada + 0.5f)
        {
            direccionMovimiento = (jugador.transform.position - transform.position).normalized;
            velocidadActual = velocidadAcercarse;
        }
        else if (distanciaActual < distanciaDeseada - 0.5f)
        {
            direccionMovimiento = (transform.position - jugador.transform.position).normalized;
            velocidadActual = velocidadAlejarse;
        }
        else
        {
            direccionMovimiento = Vector2.zero;
        }

        rb.velocity = direccionMovimiento * velocidadActual;

        spriteRenderer.flipX = jugador.transform.position.x > transform.position.x;

        if (distanciaActual < 2f && Time.time > tiempoUltimoAtaque + tiempoEntreAtaques)
        {
            Golpear();
            tiempoUltimoAtaque = Time.time;
        }

        if (distanciaActual >= 2f && Time.time > tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }


    void Golpear()
    {
        Debug.Log("La bruja ataca cuerpo a cuerpo!");

        if (golpeObjeto != null)
        {
            golpeObjeto.SetActive(true); 
            Invoke(nameof(DesactivarGolpe), tiempoGolpeActivo);
        }
    }

    void DesactivarGolpe()
    {
        if (golpeObjeto != null)
        {
            golpeObjeto.SetActive(false);
        }
    }

    void Disparar()
    {
        Debug.Log("La bruja dispara un proyectil!");
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            ProyectilBruja scriptProyectil = proyectil.GetComponent<ProyectilBruja>();
            if (scriptProyectil != null)
            {
                Vector2 direccionDisparo = (jugador.transform.position - puntoDisparo.position).normalized;
                scriptProyectil.Inicializar(direccionDisparo);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            if (Time.time - TakeDamageTimer >= TakeDamageCD)
            {
                TakeDamage();
            }
        }
    }

    void TakeDamage()
    {
        _hpEnemy.TakeDamage(1);
        TakeDamageTimer = Time.time;
    }
}
