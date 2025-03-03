using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadTest
{
    public class TestScript : MonoBehaviour
    {
        public void TestButton()
        {
            SceneManager.LoadScene("Scripts/LoadTest/LoadingScene");
        }
    }
}
