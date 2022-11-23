using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audio : MonoBehaviour
{
    public AudioMixer music, efectos;

    public AudioSource musi, efecto;

    public static audio instance;
    public float masterbolen, efectoboleons;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MasterVolumen();
    }

    public void MasterVolumen()
    {
        efectos.SetFloat("sonido_bol",masterbolen);
    }

    public void Playaudio(AudioSource audio)
    {
        audio.Play();
    }


}
