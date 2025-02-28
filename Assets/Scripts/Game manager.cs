using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class Gamemanager : MonoBehaviour
{
    public Player player;
    public List<GameObject> enemiesInScene;
    public CameraShake cameraShake;
    public CanvasGroup pauseCanvasGroup;
    public static Gamemanager instance;
    public float PauseTransitionTime;
    
    public GameObject enemiPrefab;

    public bool isPaused = false;
    
    public List<WaveManager> waves = new List<WaveManager>();

    void Awake()
    {
        enemiesInScene = new List<GameObject>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
                player = playerObject.GetComponent<Player>();
            else
                Debug.Log("No se encontro al player");
        }
    }

    void ChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(0);
        }
    }


    void Update()
    {
        ChangeScene();
        CheckInputs();
        GetEnemiesInScene();
    }


    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(!isPaused);
        }
    }

    public void Pause(bool state)
    {
        float PauseCanvasAlphaValue = isPaused ? 1 : 0f;
        Time.timeScale = isPaused ? 0 : 1;
        pauseCanvasGroup.DOFade(PauseCanvasAlphaValue, PauseTransitionTime).SetUpdate(true);
        isPaused = state;
    }

    public void addEnemy(GameObject enemy)
    {
        enemiesInScene.Add(enemy);
    }


    public void SpawnEnemy(GameObject spawnPoint, int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Obtener el prefab del enemigo
            GameObject enemyPrefab = GetEnemyPrefab();

            // Instanciar el enemigo
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            // Registrar el enemigo instanciado a la lista de enemigos activos
            addEnemy(spawnedEnemy);
        }
    }


    private GameObject GetEnemyPrefab()
    {
        return enemiPrefab;
    }


    public void RemoveEnemy(GameObject enemy)
    {
        enemiesInScene.Remove(enemy);
    }

    public int GetEnemiesInScene()
    {
        return enemiesInScene.Count;
    }

    public IEnumerator FreezeFrame(float freezeTime)
    {
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(freezeTime);
        Time.timeScale = 1;
    }
}