using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
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


    public SlicedFilledImage HPHealthBar;
    
    public CameraShake cameraShake;
    
    
    protected override void Start()
    { 
        base.Start();
       
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
        cameraShake.CmrShake(0.75f, 0.5f); 
        sprite.DOColor(damageColor, (damageTweenTime / effectLoop)).SetLoops(effectLoop, LoopType.Yoyo);
        UpdateHpSlider();
        AudioManager.instance.PlaySFX(AudioManager.instance.TakeDamageSFX);
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
        switch (base.currentHp)
        {
            case 4:
                HPHealthBar.fillAmount = 0.80f;
                break;
            case 3:
                HPHealthBar.fillAmount = 0.60f;
                break;
            case 2:
                HPHealthBar.fillAmount = 0.40f;
                break;
            case 1:
                HPHealthBar.fillAmount = 0.20f;
                break;
            default:
                break;
        }
    }
}
