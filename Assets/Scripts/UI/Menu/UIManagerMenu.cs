using UnityEngine;

namespace UI.Menu
{
	public class UIManagerMenu : MonoBehaviour
	{
		public GameObject panelCredits;
		public GameObject panelSound;
		public GameObject panelHelp;
		public GameObject FadeObject;

		

		public void OpenPanelCredits()
		{
			panelCredits.SetActive(true);
			ShowFadeObject();
		}

		public void ClosePanelCredits()
		{
			panelCredits.SetActive(false);
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
