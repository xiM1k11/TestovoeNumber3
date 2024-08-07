using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private void Start()
    {
        LoadScene();
    }
    private void LoadScene()
    {
        StartCoroutine(LoadSceneAsync(1));
    }
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex, UnityEngine.SceneManagement.LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {

            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
                Debug.Log("к загрузке готова");
            }

            yield return null;
        }

        Debug.Log("Scene loaded successfully!");
    }
}
