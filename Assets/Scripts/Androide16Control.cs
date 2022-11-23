using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Androide16Control : MonoBehaviour
{
    Score score;
    private Vector3 posicion_player;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public Transform vegeta;

    AudioSource audioSource;
    public AudioClip sonido_cuerpo;
    public AudioClip sonido_mano;
    public AudioClip sonido_poder;
    public AudioClip sonido_manos;

    public GameObject disparo_mano;
    public GameObject disparo_manos;
    public GameObject disparo_poder;

    private float cuentaBajo, cuentaBajoTeleport;
    private float tiempoTeleport = 3, tiempoDetectar = 3;
    bool estaAtacando = false;
    bool desactivar = false;
    bool retro = false;

    private int animacion_quieto = 1;
    private int animacion_cuerpo_cuerpo = 2;
    private int animacion_disparo_mano = 3;
    private int animacion_poder = 4;
    private int animacion_disparo_manos = 5;

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
           // Ataque();
            ubicarPlayer();
            cuentaBajo = tiempoDetectar;

        }
        if (cuentaBajoTeleport <= 0f)
        {
            ubicarPlayer();
            cuentaBajoTeleport = tiempoTeleport;
        }
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

    private void Ataque()
    {
        var distancia = 0.0f;
        if (transform.position.x > vegeta.transform.position.x)
        {
            distancia = transform.position.x - vegeta.transform.position.x;

        }

        if (distancia > 0 && distancia < 3)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                audioSource.PlayOneShot(sonido_cuerpo);
                changeAnimation(animacion_cuerpo_cuerpo);
                Invoke("cuerpo_cuerpo", 1.0f / 2);
                Invoke("isAttak", 2f);
            }
        }

        if (distancia > 3 && distancia < 9)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(sonido_poder);
                changeAnimation(animacion_poder);
                Invoke("poder", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia > 13 && distancia < 15)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(sonido_mano);
                changeAnimation(animacion_disparo_mano);
                Invoke("dispara_mano", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia > 15 && distancia < 17)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(sonido_manos);
                changeAnimation(animacion_disparo_manos);
                Invoke("disparos_manos", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia>9 && distancia<13)
        {
            retro = true;
            posicion_player = vegeta.transform.position;
            pelar_player();
        }
    }

    private void pelar_player()
    {
        changeAnimation(animacion_cuerpo_cuerpo);
        transform.position = new Vector2(posicion_player.x + 0.2f, posicion_player.y);
        score.reducirVida_Player(0.05f);

        if (retro == true)
        {
            rb.velocity = new Vector2(7, rb.velocity.y);
        }
    }

    public void poder()
    {
        var posission = new Vector2(transform.position.x - 2, transform.position.y);
        Instantiate(disparo_poder, posission, disparo_poder.transform.rotation);
        rb.velocity = new Vector2(-5, rb.velocity.y);
    }
    public void dispara_mano()
    {
        var posission = new Vector2(transform.position.x - 2, transform.position.y);
        Instantiate(disparo_mano, posission, disparo_mano.transform.rotation);
    }
    public void disparos_manos()
    {
        var posission = new Vector2(transform.position.x - 2, transform.position.y);
        Instantiate(disparo_manos, posission, disparo_manos.transform.rotation);
    }
    public void isAttak()
    {
        estaAtacando = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "vegeta")
        {
            changeAnimation(animacion_cuerpo_cuerpo);
            score.reducirVida_Player(0.01f);
        }
    }

    private void changeAnimation(int animacion)
    {
        animator.SetInteger("estado", animacion);
    }

   
}
