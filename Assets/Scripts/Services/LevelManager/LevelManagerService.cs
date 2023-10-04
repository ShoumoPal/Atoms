using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelStatus
{
    LOCKED,
    UNLOCKED,
    COMPLETED
}

[Serializable]
public class Level
{
    public string Name;
    public Vector3 StartPosition;
}

public class LevelManagerService : GenericMonoSingleton<LevelManagerService>
{
    public Level[] Levels;

    private void Start()
    {
        SoundManager.Instance.PlayBG1(SoundType.Background1);
        SetLevelStatus(Levels[0].Name, LevelStatus.UNLOCKED);
    }

    public void SetLevelStatus(string _levelName, LevelStatus _status)
    {
        PlayerPrefs.SetInt(_levelName, (int)_status);
    }

    public LevelStatus GetLevelStatus(string _levelName)
    {
        return (LevelStatus)PlayerPrefs.GetInt(_levelName);
    }

    public void SetCurrentLevelComplete()
    {
        SetLevelStatus(SceneManager.GetActiveScene().name, LevelStatus.COMPLETED);

        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % (Levels.Length + 1);
        string nextLevelName = GetLevelNameFromIndex(nextSceneIndex);

        SetLevelStatus(nextLevelName, LevelStatus.UNLOCKED);
    }

    public Vector3 GetSpawnPointFromLevelName(string name)
    {
        Level level = Array.Find(Levels, i => i.Name == name);
        return level.StartPosition;
    }

    public string GetLevelNameFromIndex(int index) {

        string path = SceneUtility.GetScenePathByBuildIndex(index);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf(".");
        return name.Substring(0, dot);
    }

    public void LoadScene(string levelName)
    {
        StartCoroutine(LoadSceneWithTransition(levelName));
    }

    private IEnumerator LoadSceneWithTransition(string levelName)
    {
        CrossfadeService.Instance.FadeIn(levelName);
        yield return new WaitForSeconds(CrossfadeService.Instance.fadeTime);

        SceneManager.LoadScene(levelName);
        yield return new WaitForSeconds(CrossfadeService.Instance.fadeTime);

        CrossfadeService.Instance.FadeOut();
        yield return new WaitForSeconds(CrossfadeService.Instance.fadeTime);
    }
}
