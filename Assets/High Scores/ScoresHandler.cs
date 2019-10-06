using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresHandler : MonoBehaviour
{
    public Text NamesText;
    public Text ScoresText;

    // Start is called before the first frame update
    void Start()
    {
        var hsFile = Application.persistentDataPath + "/highscores.json";
        if (!File.Exists(hsFile))
        {
            var file = File.CreateText(hsFile);
            var hs = new HighScores();
            var json = JsonUtility.ToJson(hs);
            file.Write(json);
            file.Close();
        }

        HighScores highscores;
        {
            var file = File.OpenText(hsFile);
            var json = file.ReadToEnd();
            highscores = JsonUtility.FromJson<HighScores>(json);
            file.Close();
        }


        if (PlayerPrefs.HasKey("score"))
        {
            var score = PlayerPrefs.GetInt("score");
            var name = PlayerPrefs.GetString("name");

            var hs = new HighScore { playerName = name, score = score };
            highscores.highScores.Add(hs);
            highscores.highScores.Sort();
            highscores.highScores.Reverse();

            if (highscores.highScores.Count > 10)
                highscores.highScores.RemoveRange(10, highscores.highScores.Count - 10);

            var file = File.CreateText(hsFile);
            var json = JsonUtility.ToJson(highscores);
            file.Write(json);
            file.Close();

            PlayerPrefs.DeleteKey("score");
        }

        var names = "";
        var scores = "";
        foreach (var hs in highscores.highScores)
        {
            names += hs.playerName + Environment.NewLine;
            scores += hs.score + Environment.NewLine;
        }

        NamesText.text = names;
        ScoresText.text = scores;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}

[Serializable]
public class HighScores
{
    public List<HighScore> highScores;
}

[Serializable]
public class HighScore : IComparable
{
    public int score;
    public string playerName;

    public int CompareTo(object obj)
    {
        var hs = obj as HighScore;
        if (hs == null)
            return 0;

        return score.CompareTo(hs.score);
    }
}