using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo_mortal : MonoBehaviour
{
    Rigidbody2D rd;
    float velocida = -10f;
    Score score;
    // Start is called before the first frame update
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
            score.reducirVida_Player(0.2f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "vegeta")
        {
            score.reducirVida_Player(0.2f);
            Destroy(gameObject);
        }
    }
}
