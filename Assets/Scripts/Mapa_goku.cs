using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mapa_goku : MonoBehaviour
{
   // public Image imagen_goku;
    //float vida = 1f;

    //public GameObject esfera1;
    //public GameObject esfera2;
    //public GameObject esfera3;
    //public GameObject esfera4;
    //public GameObject esfera5;
    //public GameObject esfera6;
    //public GameObject esfera7;

    bool activar = false;
  //  public float vidas_goku=0;
    void Start()
    {
        // imagen_goku.fillAmount = vidas_goku;
        
    }
    void Update()
    {
       // revivir_goku( esfera);
    }

    // Update is called once per frame

    public void revivir_goku(float esfera)
    {
        //Debug.Log("revicir: "+vidas_goku);
        //vidas_goku += esfera;

        //imagen_goku.fillAmount =vida-vidas_goku;
    }


    public void eliminar_esfera1()
    {
       // Destroy(esfera1);
    }

    public void pelea1()
    {
        SceneManager.LoadScene(3);
    }

    public void pelea2()
    {
        SceneManager.LoadScene(5);
    }

    public void pelea3()
    {
        SceneManager.LoadScene(6);
    }

    public void pelea4()
    {
        SceneManager.LoadScene(7);
    }

    public void pelea5()
    {
        SceneManager.LoadScene(8);
    }

    public void pelea6()
    {
        SceneManager.LoadScene(9);
    }

    public void pelea7()
    {
        SceneManager.LoadScene(10);
    }
}
