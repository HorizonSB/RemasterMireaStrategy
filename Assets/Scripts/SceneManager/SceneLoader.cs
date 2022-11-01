using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OpenScene(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }
}
