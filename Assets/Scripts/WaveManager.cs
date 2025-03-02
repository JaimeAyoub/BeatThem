using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class WaveManager : MonoBehaviour
{
    [Header("Walls")] [SerializeField] private GameObject enterWall;
    [SerializeField] private GameObject exitWall;

    [Header("Enemy Configuration")] [SerializeField]
    private List<GameObject> enemyPrefabs;

    [Header("Wave Settings")] [SerializeField]
    private int numeroEnemigos;

    [SerializeField] private GameObject spawnPoint;

    public List<GameObject> enemiesInWave = new List<GameObject>(); // Enemigos de esta wave
    private bool waveStarted = false;
    private bool isFighting = false;

 

    void Start()
    {
        
        if (enterWall == null)
            enterWall = transform.Find("EnterWall")?.gameObject;

        if (exitWall == null)
            exitWall = transform.Find("ExitWall")?.gameObject;

        if (spawnPoint == null)
            spawnPoint = transform.Find("SpawnPoint")?.gameObject;

    }

    void Update()
    {
        if (enemiesInWave.Count == 0 && isFighting)
        {
            EndWave();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !waveStarted)
        {
            InitWave();
            waveStarted = true;
        }
    }

    private void InitWave()
    {
        
        Debug.Log("Iniciando la wave.");
        isFighting = true; // Estado local
        StartCoroutine(CloseWalls());
        StartCoroutine(SpawnWave(numeroEnemigos));
    }

    private void EndWave()
    {
        Debug.Log("Terminando la wave.");
        isFighting = false; // Estado local
        OpenWalls();
    }

    private IEnumerator SpawnWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            if (enemyPrefab != null)
            {
                GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position,
                    spawnPoint.transform.rotation, this.transform);
                spawnedEnemy.transform.localScale = new Vector3(-0.5f, 0.05f, 0.05f);

                enemiesInWave.Add(spawnedEnemy);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator CloseWalls()
    {
        yield return new WaitForSeconds(1);
        SpriteRenderer spriteRendererEnterWall = enterWall.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererExitWall = exitWall.GetComponent<SpriteRenderer>();
        

        if (enterWall)
        {
            spriteRendererEnterWall.DOFade(1, 2);
          
            //enterWall.layer = LayerMask.NameToLayer("ClosedWall");
           // enterWall.GetComponent<SpriteRenderer>().color = Color.black;
            enterWall.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (exitWall)
        {
            spriteRendererExitWall.DOFade(1, 2);
           // exitWall.layer = LayerMask.NameToLayer("ClosedWall");
           //exitWall.GetComponent<SpriteRenderer>().color = Color.black;
            exitWall.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        Debug.Log("Paredes cerradas.");
    }

    private void OpenWalls()
    {
        // if (enterWall)
        // {
        //     enterWall.GetComponent<BoxCollider2D>().isTrigger = true;
        //     enterWall.layer = LayerMask.NameToLayer("Default");
        //     enterWall.GetComponent<SpriteRenderer>().enabled = false;
        //     enterWall.GetComponent<Animator>().enabled = false;
        // }
        //
        // if (exitWall)
        // {
        //     exitWall.GetComponent<BoxCollider2D>().isTrigger = true;
        //     exitWall.layer = LayerMask.NameToLayer("Default");
        //     exitWall.GetComponent<SpriteRenderer>().enabled = false;
        //     exitWall.GetComponent<Animator>().enabled = false;
        // }
        Destroy(this.gameObject);
    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            return enemyPrefabs[randomIndex];
        }

        return null;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemiesInWave.Contains(enemy))
        {
            enemiesInWave.Remove(enemy); // Eliminar el enemigo de la lista
            Debug.Log($"Enemigo {enemy.name} eliminado de la ola.");
            // Verifica si la ola ha terminado
            if (enemiesInWave.Count == 0 && isFighting)
            {
                EndWave();
            }
        }
    }
}