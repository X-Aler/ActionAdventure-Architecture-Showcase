using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LoadingManager : MonoBehaviour
    {
        public static LoadingManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Load(string sceneName)
        {
            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            AsyncOperation loadingScene = SceneManager.LoadSceneAsync("LoadingScene");
            loadingScene.allowSceneActivation = true;
        
            while (!loadingScene.isDone)
                yield return null;
        
            AsyncOperation targetScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            targetScene.allowSceneActivation = false;

            // Ждём прогресс загрузки
            while (!targetScene.isDone)
            {
                if (targetScene.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(5f);
                    targetScene.allowSceneActivation = true;
                }

                yield return null;
            }
        
            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(loadedScene);
        
            SceneManager.UnloadSceneAsync("LoadingScene");
        }
    }
}