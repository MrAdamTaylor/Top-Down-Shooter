using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
	public class AudioSlider : MonoBehaviour
	{
		public string audioSourceHolderName;
		[Range(0, 1)]
		public int audioSourceIndex;

		void Start()
		{
			if (string.IsNullOrEmpty(audioSourceHolderName))
			{
				return;
			}

			GameObject auidoSourceHolder = GameObject.Find(audioSourceHolderName);
			if (auidoSourceHolder != null)
			{
				Slider slider = GetComponent<Slider>();
				if (slider != null)
				{
					AudioSource[] audioSources = auidoSourceHolder.GetComponents<AudioSource>();
					if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length)
					{
						slider.value = audioSources[audioSourceIndex].volume;
					}
				}
			}
			else
			{
				Debug.Log("AudioSources holder is not found");
			}
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
}
