using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField] public GameObject boton_pausa;
    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject ganador_vegeta;
    [SerializeField] public GameObject ganador_enemigo;

    public Text nombreTex;
  //  public InputField display;

    public Image imageplayer;
    public Image imageEnemigo;
    float vidaPlayer=1;
    float vidaEnemigo=1;

   public float saludActualEnemigo;
    public float saludActualPlayer;

    private string nombre_jugador;
    private int puntaje_duelos;
    private float puntaje_revivir;
    private string duelo_1;
    private string duelo_2;
    private string duelo_3;
    private string duelo_4;
    private string duelo_5;
    private string duelo_6;
    private string duelo_7;

    void Start()
    {
       // nombreTex.GetComponent<Text>().text = PlayerPrefs.GetString("jugador1");


        CargarPartida();
    }

    public void GuardarPartida()
    {
        var filePath = Application.persistentDataPath + "/saves.dat";
        FileStream file;
        if (File.Exists(filePath))
        {
            file = File.OpenWrite(filePath);
        }
        else
        {
            file = File.Create(filePath);
        }

        Game_data data = new Game_data();
        data.jugador=nombre_jugador;
        data.puntaje=puntaje_duelos;
        data.vida_goku = puntaje_revivir;
        data.duelo1=duelo_1;
        data.duelo2=duelo_2;
        data.duelo3=duelo_3;
        data.duelo4=duelo_4;
        data.duelo5=duelo_5;
        data.duelo6=duelo_6;
        data.duelo7=duelo_7;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file,data);
        file.Close();
    }

    public void CargarPartida()
    {
        var filePath = Application.persistentDataPath + "/saves.dat";
        FileStream file;
        if (File.Exists(filePath))
        {
            file = File.OpenRead(filePath);
        }
        else
        {
            Debug.Log("no se encontrado archivo");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        Game_data data = (Game_data)bf.Deserialize(file);
        file.Close();
        nombre_jugador = data.jugador;
        puntaje_duelos=data.puntaje;
        puntaje_revivir=data.vida_goku;
        duelo_1=data.duelo1;
        duelo_2=data.duelo2;
        duelo_3=data.duelo3;
        duelo_4=data.duelo4;
        duelo_5=data.duelo5;
        duelo_6=data.duelo6;
        duelo_7=data.duelo7;
       // mostar_nombre();
    }
    public string getNombre()
    {
        return this.nombre_jugador;
    }
    public void jugador_nombre()
    {
       // nombre_jugador = display.text;
        
       // GuardarPartida();
        //nombre_jugador = nombre;
       // Debug.Log("nombre: " + nombre_jugador);
        
        
       // mostar_nombre();
    }

    private void mostar_nombre()
    {
       // nombreTex.text = "JUGADOR: " + nombre_jugador;
      // Debug.Log("nombre: "+nombre_jugador);
    }

    public void puntaje_peleas(int puntaje)
    {

    }
    public void duelo_frezz()
    {

    }


    public void reducirVida_Player(float golpe)
    {
        saludActualPlayer += golpe;
        imageplayer.fillAmount = vidaPlayer - saludActualPlayer;

    }

 
    public void reducirVida_Enemigo(float golpe)
    {
        saludActualEnemigo += golpe;
        imageEnemigo.fillAmount = vidaEnemigo-saludActualEnemigo;
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        boton_pausa.SetActive(false);
        menu.SetActive(true);
        
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
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
    public void regresar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(4);
    }
    public void jugar()
    {
        SceneManager.LoadScene(3);
    }

    public void principal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void ganadorVegeta()
    {
      //  Time.timeScale = 0f;
        boton_pausa.SetActive(false);
        ganador_vegeta.SetActive(true);
    }
    public void ganadorEnemigos()
    {
        boton_pausa.SetActive(false);
        ganador_enemigo.SetActive(true);
    }

    public void pelea1()
    {
        Time.timeScale = 1f;
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
    public void finalesena()
    {
        SceneManager.LoadScene(12);
    }

    public void escenaInicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
