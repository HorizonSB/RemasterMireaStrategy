using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    [SerializeField]private float transitionTime = 1f;

    public void OpenScene(int _sceneIndex)
    {
        Time.timeScale = 1;
        StartCoroutine(LoaddLevel(_sceneIndex));
    }

    IEnumerator LoaddLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
