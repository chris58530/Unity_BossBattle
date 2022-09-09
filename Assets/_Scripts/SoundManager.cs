using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource BGSrc,effectSrc1, effectSrc2;
    [SerializeField] AudioClip BGMClip, BGMDie,hurt,dash,reload,playerDie;

    private void Awake()
    {
        instance = this;
        BGSrc = gameObject.AddComponent<AudioSource>();
        effectSrc1 = gameObject.AddComponent<AudioSource>();
        effectSrc2 = gameObject.AddComponent<AudioSource>();
    }
    public void BGM()
    {
        BGSrc.clip = BGMClip;
        BGSrc.loop = true;
        if(!BGSrc.isPlaying)
            BGSrc.Play();
    }
    public void ClipUI()
    {
        BGSrc.clip = BGMDie;
        BGSrc.loop = false;
        if (!BGSrc.isPlaying)
            BGSrc.Play();
    }
    public void Hurt()
    {
        effectSrc1.clip = hurt;
        effectSrc1.Play();
    }
    public void Dash()
    {
        effectSrc1.clip = dash;
        effectSrc1.Play();
    }
    public void PlayerDie()
    {
        effectSrc2.clip = playerDie;
        effectSrc2.Play();
    }
}
