using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Androide18Control : MonoBehaviour
{
    Score score;
    private Vector3 posicion_player;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public Transform vegeta;

    AudioSource audioSource;
    public AudioClip Sonido_patada;
    public AudioClip Sonido_disco;
    public AudioClip Sonido_disparo;
    public AudioClip Sonido_bola;

    public GameObject disparo;
    public GameObject disco;
    public GameObject bola;
    public GameObject poder_grande;

    private float cuentaBajo, cuentaBajoTeleport;
    private float tiempoTeleport = 3, tiempoDetectar = 3;
    bool estaAtacando = false;
    bool desactivar = false;
    bool retro = false;

    private int animacion_quieto = 0;
    private int animacion_cuerpo_cuerpo = 1;
    private int animacion_disparo_poder = 2;
    private int animacion_disparo_disco = 3;
    private int animacion_disparo_poder_grande = 4;
    private int animacion_disparo_bola = 5;

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
        if (cuentaBajo <= 1f)
        {
            if (desactivar == false)
            {
                Ataque();
            }
            //Ataque();
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
                audioSource.PlayOneShot(Sonido_patada);
                changeAnimation(animacion_cuerpo_cuerpo);
                Invoke("cuerpo_cuerpo", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia > 4 && distancia < 8)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(Sonido_disparo);
                changeAnimation(animacion_disparo_poder);
                Invoke("disparo_poder", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }
        if (distancia > 8 && distancia < 10)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(Sonido_disco);
                changeAnimation(animacion_disparo_disco);
                Invoke("disparo_disco", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia > 13 && distancia < 15)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(Sonido_bola);
                changeAnimation(animacion_disparo_bola);
                Invoke("disparo_bola", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia > 15 && distancia < 17)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(Sonido_disparo);
                changeAnimation(animacion_disparo_poder_grande);
                Invoke("disparo_grande", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia>10 && distancia<13)
        {
            retro = true;
            posicion_player = vegeta.transform.position;
            pelar_player();

        }
    }

    private void pelar_player()
    {
        transform.position = new Vector2(posicion_player.x + 0.2f, posicion_player.y);
        score.reducirVida_Player(0.01f);
        audioSource.PlayOneShot(Sonido_patada);
        changeAnimation(animacion_cuerpo_cuerpo);
        if (retro == true)
        {
            rb.velocity = new Vector2(7, rb.velocity.y);
        }
    }

    public void Cuerpo_cuerpo()
    {
        changeAnimation(animacion_cuerpo_cuerpo);
        rb.velocity = new Vector2(-5, rb.velocity.y);
    }
    public void disparo_poder()
    {
        var posission = new Vector2(transform.position.x - 2, transform.position.y);
        Instantiate(disparo, posission, disparo.transform.rotation);
    }
    public void disparo_disco()
    {
        var posission = new Vector2(transform.position.x - 2, transform.position.y);
        Instantiate(disco, posission, disco.transform.rotation);
        rb.velocity = new Vector2(-5, rb.velocity.y);
    }
    public void disparo_bola()
    {
        var posission = new Vector2(transform.position.x - 3, transform.position.y);
        Instantiate(bola, posission, bola.transform.rotation);
    }
    public void disparo_grande()
    {
        var posission = new Vector2(transform.position.x - 9, transform.position.y+0.5f);
        Instantiate(poder_grande, posission, poder_grande.transform.rotation);
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
