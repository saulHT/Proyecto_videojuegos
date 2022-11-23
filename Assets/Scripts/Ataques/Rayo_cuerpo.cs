using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo_cuerpo : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rd;
    float velocida = -10f;
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
        rd.velocity = new Vector2(velocida, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="vegeta")
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
