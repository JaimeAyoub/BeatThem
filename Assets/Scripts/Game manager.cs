using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{
    public Player player;
    public List<GameObject> enemiesInScene;
    public CameraShake cameraShake;
    public CanvasGroup pauseCanvasGroup;
    public static Gamemanager instance;
    public float PauseTransitionTime;
    public CinemachineVirtualCamera virtualCamera;
    public PolygonCollider2D colliderLvl1Camera;
    public Camera mainCamera;
    public GameObject spawnPoint;


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

        if (player == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
                player = playerObject.GetComponent<Player>();
            else
                Debug.Log("No se encontro al player");
        }

        if (cameraShake == null)
        {
            GameObject cameraShakeObject = GameObject.Find("CameraShake");
        }

        if (pauseCanvasGroup == null)
        {
            GameObject pauseCanvasGroupObject = GameObject.Find("Pause Canvas");
        }
        Reset();
    }
    void OnEnable()
    {
       
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void Start()
    {
        Reset();
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

//Escena 0 Main Menu
//Escena 1 Game
//Escena 2 Win
//Escena 3 GameOver
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
            GameObject spawnedEnemy =
                Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

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
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(freezeTime);
        Time.timeScale = 1;
    }

    public void UnloadScene(int index)
    {
        SceneManager.UnloadSceneAsync(index);
    }

    public void ChargeScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void Reset()
    {
        player.gameObject.SetActive(true);
        player.transform.position = spawnPoint.transform.position;
        player.GetComponent<HP_Player>().currentHp = player.GetComponent<HP_Player>().HpMax;
        virtualCamera.transform.position = player.transform.position;
        CinemachineConfiner2D confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
        mainCamera.GetComponent<Camera>().transform.position = player.transform.position;
        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
        confiner.m_BoundingShape2D = colliderLvl1Camera;

        confiner.InvalidateCache();
        player.GetComponent<HP_Player>().UpdateHpSlider();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Escena cargada: " + scene.name);

       
        if (scene.name == "Inicio") 
        {
            
            Reset(); 
        }
       
    }
}