using DG.Tweening;
using UnityEngine;

public class HP_Enemy : HP
{
    public SpriteRenderer sprite;
    public Color damageColor;
    public int effectLoop;
    public float damageTweenTime;
    public WaveManager waveManager;
    public Rigidbody2D rb;
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
        AudioManager.instance.PlaySFX(AudioManager.instance.HitSFX);
        rb = GetComponent<Rigidbody2D>();
        if (transform.localScale.x > 0)  
        {
            transform.DOMove(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), 1);
        }
        else if (transform.localScale.x < 0)
        {
            transform.DOMove(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), 1);
        }
        

        if (base.currentHp > 0)
        {
            StartCoroutine(Gamemanager.instance.FreezeFrame(0.15f));
            sprite.DOColor(damageColor, (damageTweenTime / effectLoop)).SetLoops(effectLoop, LoopType.Yoyo);
            CameraShake.instance.CmrShake(0.75f, 0.5f); //Intensidad y Tiempo de duracion del efecto
        }

        if (base.currentHp <= 0)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.DieSFX);
            StopAllCoroutines();
            DOTween.KillAll();


            if (waveManager != null)
            {
                waveManager.RemoveEnemy(this.gameObject);
            }
        }
    }
}