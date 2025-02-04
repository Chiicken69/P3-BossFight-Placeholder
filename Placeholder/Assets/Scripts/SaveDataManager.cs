using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    [Header("Save data information")]
    [SerializeField] private string saveFileName = @"";

    private string saveFolderPath = @"";
    private string fullSavePath = @"";
    public SaveData saveDataObject;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private int sceneIndex;



    void Awake()
    {
        saveDataObject = new SaveData();
        saveFolderPath = Application.persistentDataPath;
        fullSavePath = Path.Combine(saveFolderPath, saveFileName);
        Debug.Log("Full save data path: " + fullSavePath);

        LoadSaveDataFromFile(fullSavePath);
        Debug.Log("Loaded save data from file / created new file.");

    }


    [System.Serializable]
    public class SaveData
    {
        [SerializeField]
        public int PlayerHP;

        [SerializeField]
        public int PlayerBullets;

        [SerializeField]
        public int SceneIndex;

        [SerializeField]
        public Vector2 PlayerPos;



    }
    /// <summary>
    /// Creates a blank file for saving data at savePath with name saveFileName;
    /// </summary>
    /// <param name="savePath"></param>
    private void CreateSaveFile(string savePath)
    {
        if (!File.Exists(savePath))
        {
            try
            {
                using FileStream fs = File.Create(savePath);
                Debug.Log("Created Save data file");
                fs.Close();
            }
            catch (IOException e)
            {
                Debug.Log("IOException when creating sava data file: " + e.ToString());
            }
        }
        else
        {
            Debug.Log("Error when creating sava data file: " + savePath + " already exists.");
        }
    }

    private void LoadSaveDataFromFile(string savePath)
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Highscore save data file doesnt exist, attempting creation.");
            CreateSaveFile(savePath);
        }
        else
        {
            try
            {
                Debug.Log("Attempting to read sava data from: " + savePath + " and update variables.");
                string readText = File.ReadAllText(savePath);
                Debug.Log("Overwriting current save data: " + JsonUtility.ToJson(saveDataObject).ToString() + " with the following imported saves: " + readText);
                saveDataObject = JsonUtility.FromJson<SaveData>(readText);
                Debug.Log("Save data is now: " + JsonUtility.ToJson(saveDataObject).ToString());

            }
            catch (IOException e)
            {
                Debug.Log("IOException when importing save data from file: " + e.ToString());
            }
        }

    }
    /// <summary>
    /// Function to save data from other scripts
    /// </summary>
    public void SaveDataToFile()
    {
        SaveDataToFile(fullSavePath);
    }

    //Saves save data to file at savePath.
    private void SaveDataToFile(string savePath)
    {
        if (File.Exists(savePath))
        {
            try
            {
                string jsonSaveString = JsonUtility.ToJson(saveDataObject).ToString();

                //Empty save file
                Debug.Log("Emptying save file");
                File.WriteAllText(savePath, string.Empty);

                Debug.Log("Attempting to write the following to save file: " + jsonSaveString);
                File.WriteAllText(savePath, jsonSaveString);

            }
            catch (IOException IOe)
            {
                Debug.Log("IOException when attempting to save data to file: " + IOe.ToString());

            }
        }
        else
        {
            CreateSaveFile(savePath);
            SaveDataToFile(savePath);
        }

    }

    private void OnApplicationQuit()
    {
        //Gem og luk save data fil korrekt.
        SaveDataToFile(fullSavePath);
    }

}
