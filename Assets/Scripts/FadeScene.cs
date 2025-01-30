using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{

    public Image fadeImg;
    public int sceneToLoad;  // 0 es MenuTest, 1 es la escena de pruena de scripts
    public float fadeTime = 1.0f;

    void Start()
    {
        fadeImg.color = Color.black;
        FadeOut();
    }


    void Update()
    {

    }
    //1 es opaco, 0 es transparente
    public void FadeIn()
    {
        fadeImg.DOFade(1, fadeTime).OnComplete(ChangeScene);
    }
    public void FadeOut()
    {
        fadeImg.DOFade(0, fadeTime);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
