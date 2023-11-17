using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Xml;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;     //Shared across all instances as static.

    public TMP_InputField inputField;
    public string name;
    public string highName;
    public int highScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        LoadHighScore();

        if (Instance != null)      //If there is already a UI manager instance, destroy the duplicate (this happens when change scene) and end.
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);    
    }

    public void StartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateName()
    {
        name = inputField.text;
    }

    public void StoreHighScore(int _score, string _name)
    {
        highScore = _score;
        highName = _name;
    }

    [System.Serializable]

    class SaveData
    {
        public string name;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.name = highName;
        string json = JsonUtility.ToJson(data);      //Transorms the data to json format.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);     //File path and json data to store.
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);    //Reads contents from file path.
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highName = data.name;
            highScore = data.highScore;

        }
    }



}
