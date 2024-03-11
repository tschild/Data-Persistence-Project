using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using System.IO;

public class DataPersistence : MonoBehaviour
{
    public static DataPersistence Instance;

    public int Score;
    public int HighScore;
    public string PlayerName;
    public string HighScoreHolder;

    private void Awake()
    {
        LoadHighScoreData();

        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    [System.Serializable]
class SaveData
{
	public int TopHighScore;
    public string TopHighScoreHolder;
}

public void SaveHighScoreData()
{
    SaveData data = new SaveData();
    data.TopHighScore = HighScore;
    data.TopHighScoreHolder = HighScoreHolder;

    string json = JsonUtility.ToJson(data);

    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
}
   
public void LoadHighScoreData()
{
    string path = Application.persistentDataPath + "/savefile.json";
    if(File.Exists(path))
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        HighScore = data.TopHighScore;
        HighScoreHolder = data.TopHighScoreHolder;
    }
}


}
