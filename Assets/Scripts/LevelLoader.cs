using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Transform levelSpawnPosition;

    [SerializeField] private Level[] levels;

    public Level CurrentLevel { get; private set; }

    private int levelIndex = 0;

    public void LoadLevel()
    {
        if (CurrentLevel != null)
            Destroy(CurrentLevel.gameObject);

        CurrentLevel = Instantiate(levels[levelIndex], levelSpawnPosition.position, Quaternion.identity);
    }

    public void IncrementLevelIndex()
    {
        levelIndex++;

        levelIndex %= levels.Length;
    }
}
