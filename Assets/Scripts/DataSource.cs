using UnityEngine;

public class DataSource 
{
    static public void PrepareNewSession()
    {
        DataSource.winsInSession = 0;
        DataSource.topScoreInSession = 0;
    }

    static public int nextSpriteIndex
    {
        get
        {
            return PlayerPrefs.GetInt("nextSpriteIndex", 0);
        }

        set
        {
            PlayerPrefs.SetInt("nextSpriteIndex", value);
            PlayerPrefs.Save();
        }
    }

    static public int difficulty
    {
        get
        {
            return PlayerPrefs.GetInt("difficulty", 0);
        }

        set
        {
            PlayerPrefs.SetInt("difficulty", value);
            PlayerPrefs.Save();
        }
    }

    static public int winsInSession
    {
        get
        {
            return PlayerPrefs.GetInt("winsInSession", 0);
        }

        set
        {
            PlayerPrefs.SetInt("winsInSession", value);
            PlayerPrefs.Save();
        }
    }

    static public void addNewWinInSession()
    {
        winsInSession++;
    }

    static public void updateTopScores(int score)
    {
        if (topScoreInSession < score)
        {
            topScoreInSession = score;
            if (topScore < score)
            {
                topScore = score;
            }
        }
    }
    static public int topScoreInSession
    {
        get
        {
            return PlayerPrefs.GetInt("topScoreInSession", 0);
        }

        set
        {
            PlayerPrefs.SetInt("topScoreInSession", value);
            PlayerPrefs.Save();
        }
    }

    static public int topScore
    {
        get
        {
            return PlayerPrefs.GetInt("topScore", 0);
        }

        set
        {
            PlayerPrefs.SetInt("topScore", value);
            PlayerPrefs.Save();
        }
    }
}
/*
{
    [SerializeField] private PotionData _PotionData = new PotionData();

    public void SaveIntoJson()
    {
        string potion = JsonUtility.ToJson(_PotionData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PotionData.json", potion);
    }
}

[System.Serializable]
public class PotionData
{
    public string potion_name;
    public int value;
    public List<Effect> effect = new List<Effect>();
}

[System.Serializable]
public class Effect
{
    public string name;
    public string desc;
}

*/