using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SessionManager : MonoBehaviour
{
    public Text BestScoreText;

    public string PlayerName;
    public string SavedName;
    public int BestScore = 0;
    public static SessionManager Instance;

    private string path = "";

    void Awake()
    {
        path = Application.persistentDataPath + "/savefile.json";

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();

        if (BestScoreText)
            BestScoreText.text += $" {SavedName}: {BestScore}";
    }

    [System.Serializable]
    class SaveData
    {
        public string SavedName;
        public int BestScore;
    }

    public void Save()
    {
        SaveData data = new SaveData
        {
            SavedName = PlayerName,
            BestScore = BestScore
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(path, json);
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            SavedName = data.SavedName;
            BestScore = data.BestScore;
        }
    }
}
