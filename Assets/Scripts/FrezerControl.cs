using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrezerControl : MonoBehaviour
{
    private Vector3 posicion_player;
    Score score;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    bool desactivar = false;
    bool estaAtacando = false;
    bool retro = false;

    public Transform vegetras;
   
    private AudioSource audioSource;
    public AudioClip sonido_tranporte;
    public AudioClip sonido_disparo_nova;
    public AudioClip sonido_disparo;
    public AudioClip sonido_disparo_rayo;
    public AudioClip sonido_golpe_cuepo;

    public GameObject SuperNova;
    public GameObject Rayo_normal;
    public GameObject Rayo_cuerpo;

    private float cuentaBajo, cuentaBajoTeleport;
    private float tiempoTeleport=3,tiempoDetectar=3;

    private int animacion_quieto = 0;
    private int animacion_convierteSay= 2;
    private int animacion_golpe_cuerpo = 3;
    private int animacion_transporta= 4;
    private int animacion_super_nova= 5;
    private int animacion_rayo_normal = 6;
    private int animacion_rayos_cuerpo= 7;
    private int animacion_saya= 8;
   

    // Start is called before the first frame update
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
        
        if (score.saludActualPlayer>=1)
        {
            // SceneManager.LoadScene(4);
            score.ganadorEnemigos();
            desactivar = true;
          //  score.GuardarPartida();
        }

        if (estaAtacando==false)
        {
            changeAnimation(animacion_quieto);
        }
        changeAnimation(animacion_quieto);
        //ubicarPlayer();
        //Ataque();
        contador();
        ataque_cuerpo();
    }

    private void ataque_cuerpo()
    {
        if (transform.position.x - vegetras.transform.position.x < 3)
        {
            changeAnimation(animacion_golpe_cuerpo);
            // score.reducirVida_Player(0.01f);
            Debug.Log("ataque rapido");
        }
    }

    private void ubicarPlayer()
    {
         if (transform.position.x>vegetras.transform.position.x)
        {
            Debug.Log("esta al a izquierda");
            changeAnimation(animacion_saya);
            Ataque();
        }
        else
        {
            Debug.Log("esta a la derecha");
        }
    }

    private void contador()
    {
        cuentaBajoTeleport -= Time.deltaTime;
        cuentaBajo -= Time.deltaTime;

       // Debug.Log("contador"+cuentaBajo);
        if (cuentaBajo<=1f)
        {
            if (desactivar==false)
            {
                Ataque();
            }
            
            ubicarPlayer();
            cuentaBajo = tiempoDetectar;
           // cantSupernova--;
        }
        if (cuentaBajoTeleport<=0f)
        {
            ubicarPlayer();
            cuentaBajoTeleport = tiempoTeleport;
        }
    }

    private void Ataque()
    {
        var distancia=0.0f;
        if (transform.position.x > vegetras.transform.position.x)
        {
            distancia = transform.position.x - vegetras.transform.position.x;
           
        }
       // changeAnimation(animacion_convierteSay);
        Debug.Log("distancia: "+ distancia);

        if (distancia>0 && distancia<3)
        {
            estaAtacando = true;
            if (estaAtacando==true)
            {
                score.reducirVida_Player(0.05f);
                audioSource.PlayOneShot(sonido_golpe_cuepo);
                changeAnimation(animacion_golpe_cuerpo);
                Invoke("CuerpoxCuerpo", 1.0f / 2);
                Invoke("isAttak", 2f);
            }
           
        }
        if (distancia>3 && distancia<7)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(sonido_disparo_rayo);
                changeAnimation(animacion_rayos_cuerpo);
                Invoke("RayoCuerpo", 1.0f / 2);
                Invoke("isAttack", 1.5f);
            }
          
        }
        if (distancia>7 && distancia<10)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(sonido_disparo_rayo);
                changeAnimation(animacion_rayo_normal);
                Invoke("RayoMortal", 1.0f / 2);
                Invoke("isAttack", 1.5f);
            }
           
        }
        if (distancia>12 && distancia<16)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(sonido_disparo_nova);
                changeAnimation(animacion_super_nova);
                Invoke("supernova",1.0f/2);
                Invoke("isAttak", 1f);
            }
          
        }
        if (distancia>15 && distancia<16)
        {
            estaAtacando = true;
            if (estaAtacando == true)
            {
                audioSource.PlayOneShot(sonido_tranporte);
                changeAnimation(animacion_transporta);
                Tranporte();
                Invoke("isAttak", 1f/2);
               
            }
           
        }

        if (distancia>10 && distancia<12)
        {
            retro = true;
            posicion_player = vegetras.transform.position;
            pelar_player();
        }
    }

    private void pelar_player()
    {
        changeAnimation(animacion_golpe_cuerpo);
        transform.position = new Vector2(posicion_player.x+0.2f,posicion_player.y);
        score.reducirVida_Player(0.05f);

        if (retro==true)
        {
            rb.velocity = new Vector2(7, rb.velocity.y);
        }
    }

    void RayoCuerpo()
    {
        var position = new Vector2(transform.position.x, transform.position.y-2f);
        Instantiate(Rayo_cuerpo, position, Rayo_cuerpo.transform.rotation);
    }
    void RayoMortal()
    {
        var position = new Vector2(transform.position.x-8f,transform.position.y);
        Instantiate(Rayo_cuerpo,position,Rayo_cuerpo.transform.rotation);
    }
    void supernova()
    {
        var position = new Vector2(transform.position.x-2.5f,transform.position.y+.5f);
        Instantiate(SuperNova,position,SuperNova.transform.rotation);
    }
    public void CuerpoxCuerpo()
    {
        changeAnimation(animacion_golpe_cuerpo);
        rb.velocity = new Vector2(-2,rb.velocity.y);
    }

    public void Tranporte()
    {
        rb.velocity = new Vector2(-4,rb.velocity.y);
    }

    public void isAttak()
    {
        estaAtacando = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="disparos")
        {
            rb.velocity = new Vector2(4,rb.velocity.y);
            Debug.Log("retroceder");
        }
    }

    private void changeAnimation(int animacion)
    {
        animator.SetInteger("estado",animacion);
    }
}
