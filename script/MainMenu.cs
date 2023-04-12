using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject canvas_menu_settings;
    public GameObject canvas_menu;
    public AudioSource menuMusic;

    void Start()
    {
        GameObject canvas_menu_settings = GameObject.FindGameObjectWithTag("Menu_Settings");
        GameObject canvas_menu = GameObject.FindGameObjectWithTag("Menu_Canvas");   
        menuMusic.Play(); 
    }

    public void CloseCanvas()
    {
        canvas_menu.SetActive(true);
        canvas_menu_settings.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings(){
        Debug.Log("Beállitasok megnyomva");
        canvas_menu_settings.SetActive(true);
    }

    public void Quit(){
        Application.Quit();
        Debug.Log("A játekos kilépett a játékból.");
    }
}
