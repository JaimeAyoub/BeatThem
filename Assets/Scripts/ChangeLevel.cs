using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

public class ChangeLevel : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public GameObject playerSpawn;
    public Image image;
    public PolygonCollider2D colliderLvl2Camera;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Sequence sequence = DOTween.Sequence();

           
            sequence.Append(image.DOFade(1f, 1f))  
                .AppendCallback(() => MovePlayer(collision.gameObject))
                .Append(image.DOFade(0f, 0.5f)); 
        }
    }

    private void MovePlayer(GameObject player)
    {
        player.transform.position = playerSpawn.transform.position;
        VirtualCamera.transform.position = playerSpawn.transform.position;
        CinemachineConfiner2D confiner = VirtualCamera.GetComponent<CinemachineConfiner2D>();
        confiner.m_BoundingShape2D = colliderLvl2Camera;
       
        confiner.InvalidateCache(); 
    }
    
}