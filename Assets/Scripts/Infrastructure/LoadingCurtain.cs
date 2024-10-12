using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LoadingCurtain : MonoBehaviour
{
    [SerializeField] private CanvasGroup Curtain;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Curtain.alpha = 1;
    }

    public void Hide() => FadeInCurtainAsycn().Forget();

    private async UniTaskVoid FadeInCurtainAsycn()
    {
        while (Curtain.alpha > 0)
        {
            Curtain.alpha -= 0.03f;
            await UniTask.WaitForSeconds(0.03f);
        }
        gameObject.SetActive(false);
    }
}