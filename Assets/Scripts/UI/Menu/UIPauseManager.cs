using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPauseManager : MonoBehaviour
{
    public GameObject panelSound;
    public GameObject panelPause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Переключаем активность панели
            panelPause.SetActive(!panelPause.activeSelf);
        }
    }

    public void OpenPanelSound()
    {
        panelSound.SetActive(true);
    }

    // Закрыть панель
    public void ClosePanelSound()
    {
        panelSound.SetActive(false);
    }
    public void LoadSceneMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSceneAgain()
    {
        SceneManager.LoadScene(4);
    }

    // Закрыть панель
    public void ClosePanelPause()
    {
        panelPause.SetActive(!panelPause.activeSelf);
    }

    public void ChangeMusicLevel(Slider slider)
    {
        if (slider == null)
        {
            return;
        }
        GameObject.Find("SoundManager").GetComponents<AudioSource>()[0].volume = slider.value;
    }

    public void ChangeEffectsLevel(Slider slider)
    {
        if (slider == null)
        {
            return;
        }
        GameObject.Find("SoundManager").GetComponents<AudioSource>()[1].volume = slider.value;
    }
}
