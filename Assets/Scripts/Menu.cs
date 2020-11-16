using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public AudioMixer mixer;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Appear()
    {
        GameObject.Find("Canvas/Menu/UI").SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetVolume(float vol)
    {
        mixer.SetFloat("Volume", vol);
    }
}
