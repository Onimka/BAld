using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class EntryPointBaseScreen : MonoBehaviour
{
    private int _numNextScene;
    private AsyncOperation _level;

    private void Start()
    {
        //защита от зацикливания или перехода на несуществующую сцену при условии что существует первая сцена
        if (_numNextScene == 0 || _numNextScene > SceneManager.sceneCount)
            _numNextScene = 1;

        // Инициализация сервисов

        _level = SceneManager.LoadSceneAsync(_numNextScene);

    }
}