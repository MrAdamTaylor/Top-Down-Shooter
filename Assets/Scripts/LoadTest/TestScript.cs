using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public void TestButton()
    {
        SceneManager.LoadScene("Scripts/LoadTest/LoadingScene");
    }
}
