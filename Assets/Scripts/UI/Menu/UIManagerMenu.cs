using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
	public class UIManagerMenu : MonoBehaviour
	{
		public GameObject panelCredits;
		public GameObject panelSound;
		public GameObject panelHelp;
		
		public void OpenPanelCredits()
		{
			panelCredits.SetActive(true);
		}

		public void ClosePanelCredits()
		{
			panelCredits.SetActive(false);
		}

		public void OpenPanelSound()
		{
			panelSound.SetActive(true);
		}

		public void ClosePanelSound()
		{
			panelSound.SetActive(false);
		}

		public void OpenPanelHelp()
		{
			panelHelp.SetActive(true);
		}

		public void ClosePanelHlp()
		{
			panelHelp.SetActive(false);
		}


		
	}
}
