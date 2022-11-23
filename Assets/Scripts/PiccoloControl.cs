using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PiccoloControl : MonoBehaviour
{
    Score score;
    private Vector3 posicion_player;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public Transform vegeta;

    public GameObject disapro;
    public GameObject disapro_manos;
    public GameObject disapro_dedo;

    private AudioSource audioSource;
    public AudioClip sonido_patada;
    public AudioClip sonido_disparo;
    public AudioClip sonido_disparo_manos;
    public AudioClip sonido_disparo_dedo;

    private float cuentaBajo, cuentaBajoTeleport;
    private float tiempoTeleport = 3, tiempoDetectar = 3;
    bool estaAtacando = false;
    bool desactivar = false;
    bool retro = false;

    private int animacion_quieto = 0;
    private int animacion_cuerpo_cuerpo = 1;
    private int animacion_disapro_poder = 2;
    private int animacion_disapro_dosmano = 3;
    private int animacion_disparo_dedo = 4;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        score = FindObjectOfType<Score>();

    }

    // Update is called once per frame
    void Update()
    {
        if (score.saludActualPlayer >= 1)
        {
            // SceneManager.LoadScene(4);
            score.ganadorEnemigos();
            desactivar = true;
        }

        if (estaAtacando == false)
        {
            changeAnimation(animacion_quieto);
        }

        changeAnimation(animacion_quieto);
        contador();
        ataque_cuerpo();
    }

    private void ataque_cuerpo()
    {
        if (transform.position.x - vegeta.transform.position.x < 3)
        {
            changeAnimation(animacion_cuerpo_cuerpo);
            // score.reducirVida_Player(0.01f);
            Debug.Log("ataque rapido");
        }
    }

    private void contador()
    {
        cuentaBajoTeleport -= Time.deltaTime;
        cuentaBajo -= Time.deltaTime;
        if (cuentaBajo <= 0f)
        {
            if (desactivar == false)
            {
                Ataque();
            }
          //  Ataque();
            ubicarPlayer();
            cuentaBajo = tiempoDetectar;
            
        }
        if (cuentaBajoTeleport <= 0f)
        {
            ubicarPlayer();
            cuentaBajoTeleport = tiempoTeleport;
        }
    }

    private void Ataque()
    {
        var distancia = 0.0f;
        if (transform.position.x > vegeta.transform.position.x)
        {
            distancia = transform.position.x - vegeta.transform.position.x;

        }

        if (distancia > 0 && distancia < 4)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                score.reducirVida_Player(0.01f);
                audioSource.PlayOneShot(sonido_patada);
                changeAnimation(animacion_cuerpo_cuerpo);
                Invoke("CuerpoxCuerpo", 1.0f / 2);
                Invoke("isAttak", 2f);
            }
        }

        if (distancia>4 && distancia<9)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                audioSource.PlayOneShot(sonido_disparo);
                changeAnimation(animacion_disapro_poder);
                Invoke("disparo_poder", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia>12 && distancia<14)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                audioSource.PlayOneShot(sonido_disparo_manos);
                changeAnimation(animacion_disapro_dosmano);
                Invoke("disparo_dos_mano", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia>14 && distancia<16)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                audioSource.PlayOneShot(sonido_disparo_dedo);
                changeAnimation(animacion_disparo_dedo);
                Invoke("disparo_poder_dedo", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia>9 && distancia<12)
        {
            retro = true;
            posicion_player = vegeta.transform.position;
            pelea_player();
        }
    }

    private void pelea_player()
    {
        audioSource.PlayOneShot(sonido_patada);
        changeAnimation(animacion_cuerpo_cuerpo);
        transform.position = new Vector2(posicion_player.x+0.2f,posicion_player.y);
       
        if (retro == true)
        {
            rb.velocity = new Vector2(7, rb.velocity.y);
        }
    }

    public void disparo_poder_dedo()
    {
        var posission = new Vector2(transform.position.x-4,transform.position.y+1);
        Instantiate(disapro_dedo,posission,disapro_dedo.transform.rotation);
    }
    public void disparo_dos_mano()
    {
        var posission = new Vector2(transform.position.x-2, transform.position.y);
        Instantiate(disapro_manos, posission, disapro_manos.transform.rotation);
    }
    public void disparo_poder()
    {
        var posission = new Vector2(transform.position.x-2, transform.position.y);
        Instantiate(disapro, posission, disapro.transform.rotation);
    }
    public void CuerpoxCuerpo()
    {
        changeAnimation(animacion_cuerpo_cuerpo);
        rb.velocity = new Vector2(-5, rb.velocity.y);
    }
    public void isAttak()
    {
        estaAtacando = false;
    }
    private void ubicarPlayer()
    {
        if (transform.position.x > vegeta.transform.position.x)
        {
            Debug.Log("esta al a izquierda");
            
        }
        else
        {
            Debug.Log("esta a la derecha");
        }
    }

    private void changeAnimation(int animacion)
    {
        animator.SetInteger("estado", animacion);
    }
}
