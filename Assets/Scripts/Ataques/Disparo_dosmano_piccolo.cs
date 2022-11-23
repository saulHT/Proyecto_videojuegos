using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo_dosmano_piccolo : MonoBehaviour
{

    Rigidbody2D rd;
    float velocidad = 10;
    Score score;

    void Start()
    {
        score = FindObjectOfType<Score>();
        rd = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rd.velocity = new Vector2(-velocidad, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "vegeta")
        {
            score.reducirVida_Player(0.1f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "vegeta")
        {
            score.reducirVida_Player(0.1f);
            Destroy(gameObject);
        }
    }
}
