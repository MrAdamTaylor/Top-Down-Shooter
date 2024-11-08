using System;
using System.Collections;
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
            Hide();
            StartCoroutine(SelfDestruct());
        }

        private void Hide() => FadeInCurtainAsync().Forget();

        private async UniTaskVoid FadeInCurtainAsync()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= FADE_DURATION;
                await UniTask.WaitForSeconds(FADE_DURATION);
            }
        }
        
        IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}