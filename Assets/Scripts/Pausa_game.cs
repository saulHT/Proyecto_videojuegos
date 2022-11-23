using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa_game : MonoBehaviour
{

    [SerializeField]public GameObject boton_pausa;
    [SerializeField]public GameObject menu;
    

    public void Pausa()
    {
        Time.timeScale = 0f;
        boton_pausa.SetActive(false);
        menu.SetActive(true);
    }
    public void Reanudar()
    {
        Time.timeScale=1f;
        boton_pausa.SetActive(true);
        menu.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void cerrar()
    {
        Debug.Log("cerrarndo juego");
        Application.Quit();
    }
}
