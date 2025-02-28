using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Enemy : HP
{
    public SpriteRenderer sprite;
    public Color damageColor;
    public int effectLoop;
    public float damageTweenTime;
    public WaveManager waveManager;

    protected override void Start()
    {
        base.Start();
        waveManager = GetComponentInParent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (base.currentHp > 0)
        {
            StartCoroutine(Gamemanager.instance.FreezeFrame(0.25f));
            sprite.DOColor(damageColor, (damageTweenTime / effectLoop)).SetLoops(effectLoop, LoopType.Yoyo);
            CameraShake.instance.CmrShake(0.75f, 0.5f); //Intensidad y Tiempo de duracion del efecto
        }

        if (base.currentHp <= 0)
        {
            StopAllCoroutines();
            DOTween.KillAll();
            

            if (waveManager != null)
            {
                waveManager.RemoveEnemy(this.gameObject);
            }
        }
    }
}