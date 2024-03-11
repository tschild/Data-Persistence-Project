using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public TMP_InputField InputField;
    public GameObject BestScoreText;
    public GameObject ErrorTextGameObject;
    public bool NameInputted = false;
    public void OpenMainScene()
    {
        SaveName();
        CheckNameIsNotBlank();
        if(NameInputted == true)
        {
             SceneManager.LoadScene(1);
        }
    }

    public void Start()
    {
        ShowHighScore();
        InputField.text = DataPersistence.Instance.PlayerName;
    }

    public void ShowHighScore()
    {
        if(DataPersistence.Instance.HighScore > 0)
        {
            BestScoreText.GetComponent<TMP_Text>().text = $"Highscore: {DataPersistence.Instance.HighScore} by {DataPersistence.Instance.HighScoreHolder}";
            BestScoreText.SetActive(true);
        }
    }

    public void QuitGame()
    {
        DataPersistence.Instance.SaveHighScoreData();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit(); //original code to quit Unity build
        #endif
    }

    public void SaveName()
    {
        DataPersistence.Instance.PlayerName = InputField.text;
        Debug.Log(DataPersistence.Instance.PlayerName);
    }

    public void CheckNameIsNotBlank()
    {
        if(DataPersistence.Instance.PlayerName == "")
        {
            ErrorTextGameObject.SetActive(true);
        }
        else
        {
            NameInputted = true;
        }
    }

    public void SaveTopHighScore()
    {
        DataPersistence.Instance.SaveHighScoreData();
    }

    public void LoadTopHighScore()
    {
        DataPersistence.Instance.LoadHighScoreData();

    }

}
