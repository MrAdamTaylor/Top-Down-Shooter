using UnityEngine;

namespace LoadTest
{
    public class SelfCreater : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            Debug.Log("BeforeSceneLoaded");
            var obj = Instantiate(Resources.Load("SelfCreater"));
            obj.name = obj.name.Replace("(Clone)", string.Empty);
            DontDestroyOnLoad(obj);
        }

        public void Awake()
        {
            Debug.Log($"Is created");
        }
    }
}
