using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Menu
{
	public class UIManagerMenu : MonoBehaviour
	{
		
		
		public GameObject PanelCredits;
		public GameObject panelSound;
		public GameObject panelHelp;
		public GameObject FadeObject;
		
		
		public void OpenPanelCredits()
		{
			PanelCredits.SetActive(true);
			ShowFadeObject();
		}

		public void ClosePanelCredits()
		{
			PanelCredits.SetActive(false);
			HideFadeObject();
		}

		public void OpenPanelSound()
		{
			panelSound.SetActive(true);
			ShowFadeObject();
		}

		public void ClosePanelSound()
		{
			panelSound.SetActive(false);
			HideFadeObject();
		}

		public void OpenPanelHelp()
		{
			panelHelp.SetActive(true);
			ShowFadeObject();
		}

		public void ClosePanelHlp()
		{
			panelHelp.SetActive(false);
			HideFadeObject();
		}

		private void ShowFadeObject()
		{
			FadeObject.SetActive(true);
		}

		private void HideFadeObject()
		{
			FadeObject.SetActive(false);
		}

	}
}
