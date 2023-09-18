using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class EntryPointBaseScreen : MonoBehaviour
{
    private int _numNextScene;
    private AsyncOperation _level;

    private void Start()
    {
        //������ �� ������������ ��� �������� �� �������������� ����� ��� ������� ��� ���������� ������ �����
        if (_numNextScene == 0 || _numNextScene > SceneManager.sceneCount)
            _numNextScene = 1;

        // ������������� ��������

        _level = SceneManager.LoadSceneAsync(_numNextScene);

    }
}