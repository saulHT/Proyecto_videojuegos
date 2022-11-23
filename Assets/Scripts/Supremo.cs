using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Supremo : MonoBehaviour
{
    Score score;
    private Vector3 posicion_player;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public Transform vegeta;

    AudioSource audioSource;
    public AudioClip sonido_golpe;
    public AudioClip sonido_poder;
    public AudioClip sonido_poder_mano;

    public GameObject poder_dedo_;
    public GameObject dos_mano;

    private float cuentaBajo, cuentaBajoTeleport;
    private float tiempoTeleport = 3, tiempoDetectar = 3;
    bool estaAtacando = false;
    bool desactivar = false;
    bool retro = false;

    private int animacion_quieto = 0;
    private int animacion_cuerpo_cuerpo = 1;
    private int animacion_golpe = 2;
    private int animacion_patada= 3;
    private int animacion_puños = 4;
    private int animacion_poder_dedo = 6;
    private int animacion_poder_dos_mano = 7;

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
            changeAnimation(animacion_puños);
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

        if (distancia > 0 && distancia < 1)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                audioSource.PlayOneShot(sonido_golpe);
                changeAnimation(animacion_cuerpo_cuerpo);
                Invoke("cuerpo_cuerpo", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia > 1 && distancia < 1.5)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                score.reducirVida_Player(0.01f);
                audioSource.PlayOneShot(sonido_golpe);
                changeAnimation(animacion_patada);
                Invoke("patada", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia>1.5 && distancia <2)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                score.reducirVida_Player(0.02f);
                audioSource.PlayOneShot(sonido_golpe);
                changeAnimation(animacion_puños);
                Invoke("puños", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }

        if (distancia>2 && distancia<2.5)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                score.reducirVida_Player(0.02f);
                audioSource.PlayOneShot(sonido_golpe);
                changeAnimation(animacion_golpe);
                Invoke("golpe", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }
        if (distancia>2.5 && distancia<9)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                audioSource.PlayOneShot(sonido_poder);
                changeAnimation(animacion_poder_dedo);
                Invoke("poder_dedo", 1.0f / 2);
                Invoke("isAttak", 1.5f);
            }
        }
        if (distancia>13 && distancia<16)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                audioSource.PlayOneShot(sonido_poder_mano);
                changeAnimation(animacion_poder_dos_mano);
                Invoke("poder_manos", 1.0f / 2);
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
        score.reducirVida_Player(0.02f);

        if (retro == true)
        {
            rb.velocity = new Vector2(7, rb.velocity.y);
        }
    }

    public void poder_dedo()
    {
        var posission = new Vector2(transform.position.x - 2, transform.position.y);
        Instantiate(poder_dedo_, posission, poder_dedo_.transform.rotation);
    }
    public void poder_manos()
    {
        var posission = new Vector2(transform.position.x - 2, transform.position.y);
        Instantiate(dos_mano, posission, dos_mano.transform.rotation);
    }
    public void isAttak()
    {
        estaAtacando = false;
    }
    private void changeAnimation(int animacion)
    {
        animator.SetInteger("estado", animacion);
    }
}
