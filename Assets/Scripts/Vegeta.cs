using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vegeta : MonoBehaviour
{
    Mapa_goku mapa_Goku;
    Score score;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject disco;
    public GameObject disparo;
    public GameObject disparo_mano;

    public GameObject enemigo;

    private AudioSource audioSource;
    public AudioClip sonido_disco;
    public AudioClip sonido_disapro;
    public AudioClip sonido_disapro_largo;
    public AudioClip salto;
    public AudioClip tranf;
    public AudioClip cuerpoxcuerpo;

    public float JumFor = 30;

    bool activar = false;
    internal object instance;
    bool activar_disparos=false;

    private const int animation_convertir = 1;
    private const int animation_quieto_tranformado = 2;
    private const int animation_disparo_disco = 3;
    private const int animation_golpe_cuerpo = 4;
    private const int animation_disparo = 5;
    private const int animation_disparo_mano = 6;
    private const int animation_patada = 7;
    private const int animation_mover = 8;
    private const int animation_volar = 9;
    private const int animation_cae = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        score = FindObjectOfType<Score>();
        audioSource = GetComponent<AudioSource>();
        mapa_Goku = FindObjectOfType<Mapa_goku>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            score.Pausa();
        }

        if (score.saludActualEnemigo >= 1)
        {
            //  SceneManager.LoadScene(4);
            //  mapa_Goku.revivir_goku(0.15f);
           // Time.timeScale = 0f;
            score.ganadorVegeta();
            Destroy(enemigo);
        }

        Debug.Log("muerto-vegeta: "+score.saludActualPlayer);
        

        if (activar==true)
        {
            changeAnimation(animation_quieto_tranformado);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, JumFor),ForceMode2D.Impulse);
            changeAnimation(animation_volar);
            audioSource.PlayOneShot(salto);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(6,rb.velocity.y);
            spriteRenderer.flipX = false;
            changeAnimation(animation_mover);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-6, rb.velocity.y);
            spriteRenderer.flipX = true;
            changeAnimation(animation_mover);
        }

        if (Input.GetKey(KeyCode.A))
        {
            changeAnimation(animation_convertir);
            activar_disparos = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            audioSource.PlayOneShot(tranf);
            changeAnimation(animation_quieto_tranformado);
               activar = true;
        }
       
        if (Input.GetKeyUp(KeyCode.Q)&& activar_disparos==true)
        {
            changeAnimation(animation_disparo);
            var posision = new Vector2(transform.position.x, transform.position.y);
            Instantiate(disparo, posision, disco.transform.rotation);
            audioSource.PlayOneShot(sonido_disapro);
        }
       

        if (Input.GetKeyUp(KeyCode.W) && activar_disparos==true)
        {
            changeAnimation(animation_disparo_disco);

            var posision = new Vector2(transform.position.x,transform.position.y+0.8f);
            Instantiate(disco,posision,disco.transform.rotation);
            audioSource.PlayOneShot(sonido_disco);
        }
        if (Input.GetKeyUp(KeyCode.E) && activar_disparos == true)
        {
            changeAnimation(animation_disparo_mano);
            var posis = new Vector2(transform.position.x+8,transform.position.y+0.5f);
            Instantiate(disparo_mano,posis,disparo_mano.transform.rotation);
            audioSource.PlayOneShot(sonido_disapro_largo);
        }
        if (Input.GetKey(KeyCode.S) && activar_disparos == true)
        {
           // audioSource.PlayOneShot(cuerpoxcuerpo);
            changeAnimation(animation_patada);
        }
        if (Input.GetKey(KeyCode.D) && activar_disparos == true)
        {
            changeAnimation(animation_golpe_cuerpo);
            //  score.reducirVida_Enemigo(0.001f);
           // audioSource.PlayOneShot(cuerpoxcuerpo);
        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="enemigo")
        {
             audioSource.PlayOneShot(cuerpoxcuerpo);
            score.reducirVida_Enemigo(0.05f);
            Debug.Log("reducir enemigo");
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="golpe")
        {
            Debug.Log("retroceder");
            Invoke("retroceder",0.1f);
        }
    }

    public void retroceder()
    {
        changeAnimation(animation_cae);
        rb.velocity = new Vector2(-5, rb.velocity.y);
    }
    private void changeAnimation(int animation)
    {
        animator.SetInteger("estado",animation);
    }
}
