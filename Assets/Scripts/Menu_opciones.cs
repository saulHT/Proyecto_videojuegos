using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_opciones : MonoBehaviour
{
    public InputField inputText;
    public Text text_nombre;
    public GameObject boton_aceptar;

    private void Update()
    {
        //if (text_nombre.text.Length<4)
        //{
        //    boton_aceptar.SetActive(false);
        //}
        //if (text_nombre.text.Length>=4)
        //{
        //    boton_aceptar.SetActive(true);
        //}
    }

    public void aceptar_boton_nomb()
    {
        PlayerPrefs.SetString("jugador1",inputText.text);
        SceneManager.LoadScene(12);
    }

    public void Botonjugar()
    {
        SceneManager.LoadScene(2);
    }
    public void BotonOpciones()
    {
        SceneManager.LoadScene(0);
    }
    public void BotonAyuda()
    {
        SceneManager.LoadScene(11);
    }
    public void BotonSalir()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonMapa()
    {
        SceneManager.LoadScene(4);
    }
    public void BotonPrincipal()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonJugar_peleas()
    {
        SceneManager.LoadScene(3);
    }
}
