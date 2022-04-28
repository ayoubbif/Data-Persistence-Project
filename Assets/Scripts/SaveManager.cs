using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public int highScore;
    public string bestplayerName;
    public Text bestplayerText;

    private void Awake()
    {
        highScore = 0;
        bestplayerName = "N/n";

        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ScoreCompare(int score)
    {
        if(score > highScore)
        {
            highScore = score;

            SceneManager.LoadScene(1);
        }
    }

    [System.Serializable]
    class HighScore
    {
        public int score;
        public string name;
    }

    public void SaveScore()
    {
        HighScore s_highScore = new HighScore();

        s_highScore.name = bestplayerName;
        s_highScore.score = highScore;

        string json = JsonUtility.ToJson(s_highScore);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Debug.Log(path);

            HighScore s_highScore = JsonUtility.FromJson<HighScore>(json);

            highScore = s_highScore.score;
            bestplayerName = s_highScore.name;
        }
    }
}
