using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo_grande_andoird18 : MonoBehaviour
{
    Rigidbody2D rd;
    float velocidad = 10;
    Score score;

    void Start()
    {
        score = FindObjectOfType<Score>();
        rd = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "vegeta")
        {
            score.reducirVida_Player(0.15f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "vegeta")
        {
            score.reducirVida_Player(0.15f);
            Destroy(gameObject);
        }
    }
}
