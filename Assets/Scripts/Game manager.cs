using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public  Player player;
    public Enemy enemy;

    public static Gamemanager instance;
    void Awake()
    {
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
       if(player == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
                player = playerObject.GetComponent<Player>();
            else
                Debug.Log("No se encontro al player");
        }
        if (enemy == null)
        {
            GameObject enemyObject = GameObject.Find("Enemy");
            if (enemyObject != null)
                enemy = enemyObject.GetComponent<Enemy>();
            else
                Debug.Log("No se encontro al Enemy");
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
        CheckEnemyHealth();
        ChangeScene();
    }
    void CheckEnemyHealth()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
          //  Debug.Log("El enemigo tiene: " + enemy.life + "de vida"); 
        }
    }
}
