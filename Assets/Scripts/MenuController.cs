
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MenuController2D : MonoBehaviour
{
    public Transform personaje;
    public Transform[] botones; // Botones en la escena (GameObjects con SpriteRenderer)
    public string[] escenas; // Nombres de escenas correspondientes a los botones
    public LineRenderer lineRenderer;
    public GameObject Fadein;

    public Image fadeImg;
    public int sceneToLoad;  // 0 es MenuTest, 1 es la escena de pruena de scripts
    public float fadeTime = 1.0f;

    private int selectedIndex = 0;

    void Start()
    {
        fadeImg.color = Color.black;
        FadeOut();
        UpdateSelection();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + botones.Length) % botones.Length;
            UpdateSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % botones.Length;
            UpdateSelection();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            FadeIn();
        }
    }

    void UpdateSelection()
    {
        foreach (Transform btn in botones)
        {
            btn.GetComponent<SpriteRenderer>().color = Color.white;
        }

        botones[selectedIndex].GetComponent<SpriteRenderer>().color = Color.green;
        UpdateLaser(botones[selectedIndex].position);
    }

    void UpdateLaser(Vector3 targetPosition)
    {
        lineRenderer.SetPosition(0, personaje.position);
        lineRenderer.SetPosition(1, targetPosition);
    }

    public void FadeIn()
    {
        fadeImg.DOFade(1, fadeTime).OnComplete(CambiarEscena);
    }
    public void FadeOut()
    {
        fadeImg.DOFade(0, fadeTime);
    }

    void CambiarEscena()
    {
        if (selectedIndex >= 0 && selectedIndex < escenas.Length)
        {
            SceneManager.LoadScene(escenas[selectedIndex]);
        }
    }
}
