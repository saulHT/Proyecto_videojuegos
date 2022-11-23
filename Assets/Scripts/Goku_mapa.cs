using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goku_mapa : MonoBehaviour
{
    Mapa_goku mapa_Goku;
    Vector2 direccion;
    Rigidbody2D rd;
    SpriteRenderer sprite;
    float velocidad = 5;
    bool activar_scena = true;

   private AudioSource audioSource;
    public AudioClip sonido_radar;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        mapa_Goku = FindObjectOfType<Mapa_goku>();
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.PlayOneShot(sonido_radar,1f);
        float x = Input.GetAxis("Horizontal");
        float y= Input.GetAxis("Vertical");
        direccion = new Vector2(x,y);
        run();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rd.velocity = new Vector2(rd.velocity.x,velocidad);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rd.velocity = new Vector2(rd.velocity.x,-velocidad);
        }
    }

    private void run()
    {
        rd.velocity = new Vector2(direccion.x*velocidad,rd.velocity.y);
        sprite.flipX = direccion.x < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "1")
        {
                SceneManager.LoadScene(3);
            mapa_Goku.revivir_goku(0.15f);
            mapa_Goku.eliminar_esfera1();
        }
        if (collision.gameObject.tag == "2")
        {
                SceneManager.LoadScene(5);
            mapa_Goku.revivir_goku(0.15f);
        }
        if (collision.gameObject.tag == "3")
        {
                SceneManager.LoadScene(6);
            mapa_Goku.revivir_goku(0.15f);
        }
        if (collision.gameObject.tag == "4")
        {
                SceneManager.LoadScene(7);
            mapa_Goku.revivir_goku(0.15f);
        }
        if (collision.gameObject.tag == "5")
        {
                SceneManager.LoadScene(8);
            mapa_Goku.revivir_goku(0.15f);
        }
        if (collision.gameObject.tag == "6")
        {
                SceneManager.LoadScene(9);
        }
        if (collision.gameObject.tag == "7")
        {
                SceneManager.LoadScene(10);
            mapa_Goku.revivir_goku(0.15f);
        }
    }
    
}
