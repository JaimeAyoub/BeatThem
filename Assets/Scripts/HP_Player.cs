using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class HP_Player : HP
{
    public SpriteRenderer sprite;
    [Header("Variables Tweens")]
    public int effectLoop;
    public float damageTweenTime;

    public Color damageColor;
    public PostProcessVolume postProcessVolume;
    public Vignette vignette;
    public float vignetteIntensity;

    public Slider hpSlider;
    protected override void Start()
    { 
        base.Start();
        hpSlider.maxValue = base.HpMax;
        hpSlider.value = base.currentHp;
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        sprite.DOColor(damageColor, (damageTweenTime / effectLoop)).SetLoops(effectLoop, LoopType.Yoyo);
        UpdateHpSlider();
        DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, vignetteIntensity, 0.5f).SetId("VignetteTween").OnComplete(() =>
        {

            DOTween.To(() => vignette.intensity.value,
                       x => vignette.intensity.value = x,
                       0f,
                       0.5f)
                   .SetId("VignetteTween");
        });

    }

    void UpdateHpSlider()
    {
        hpSlider.value = base.currentHp;
    }
}
