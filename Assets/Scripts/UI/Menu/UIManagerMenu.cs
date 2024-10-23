using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMenu : MonoBehaviour
{
	public GameObject panelCredits;
	public GameObject panelSound;
	public GameObject panelHelp;
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	// ������� ������
	public void OpenPanelCredits()
	{
		panelCredits.SetActive(true);
	}

	// ������� ������
	public void ClosePanelCredits()
	{
		panelCredits.SetActive(false);
	}

	public void OpenPanelSound()
	{
		panelSound.SetActive(true);
	}

	// ������� ������
	public void ClosePanelSound()
	{
		panelSound.SetActive(false);
	}

	public void OpenPanelHelp()
	{
		panelHelp.SetActive(true);
	}

	// ������� ������
	public void ClosePanelHlp()
	{
		panelHelp.SetActive(false);
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
