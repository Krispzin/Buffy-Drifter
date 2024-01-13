using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadingScreen;
    public Slider loadingbar;
   public void LoadScene(int levelIndex)
    {
        StartCoroutine(LoadSceneAsynchronously(levelIndex));
        
    }
    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone) {
            loadingbar.value = operation.progress;
            yield return null;
        }
    }
}
