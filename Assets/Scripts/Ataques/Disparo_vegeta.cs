using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo_vegeta : MonoBehaviour
{
    Rigidbody2D rd;
    float velocidad=10;
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
        if (collision.gameObject.tag=="enemigo")
        {
            score.reducirVida_Enemigo(0.1f);
            Destroy(gameObject);
        }
    }
}
