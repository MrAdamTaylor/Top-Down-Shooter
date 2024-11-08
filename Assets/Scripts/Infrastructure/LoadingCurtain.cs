using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        private const float FADE_DURATION = 0.03f;
        [SerializeField] private CanvasGroup _curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() => FadeInCurtainAsync().Forget();

        private async UniTaskVoid FadeInCurtainAsync()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= FADE_DURATION;
                await UniTask.WaitForSeconds(FADE_DURATION);
            }
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}