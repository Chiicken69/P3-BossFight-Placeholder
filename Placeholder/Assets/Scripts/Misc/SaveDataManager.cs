using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    [Header("Save data information")]
    [SerializeField] private string permSaveFileName = @"";
    [SerializeField] private string quickSaveFileName = @"";

    private string saveFolderPath = @"";
    private string permFullSavePath = @"";
    private string quickFullSavePath = @"";
    public SaveData saveDataObject;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private int sceneIndex;

    private PlayerAttack PlayerAttackScript;

    private static SaveDataManager staticSDM;


    void Awake()
    {
        //Only one save data manager
        if(staticSDM == null)
        {
            staticSDM = this;
        } else
        {
            Destroy(this.gameObject);
        }


        saveDataObject = new SaveData();
        saveFolderPath = Application.persistentDataPath;
       
        //Long term saves, between sessions
        permFullSavePath = Path.Combine(saveFolderPath, permSaveFileName);
        Debug.Log("Full save data path: " + permFullSavePath);

        //Short term saves, for quick-respawn after dying to a boss
        quickFullSavePath = Path.Combine(saveFolderPath, quickSaveFileName);
        Debug.Log("Quick respawn data path: " + quickFullSavePath);

        //Load correct save file
        //There is a difference between if a player just died / is reloading from a save between sessions.


        LoadSaveDataFromFile(permFullSavePath);
        Debug.Log("Loaded save data from file / created new file.");

    }

    private void Start()
    {
        PlayerAttackScript = player.GetComponent<PlayerAttack>();
        
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
        UpdatePlayerData();

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
    public void SaveDataToPermFile()
    {
        SaveDataToFile(permFullSavePath);
    }

    public void SaveDataToQuickFile()
    {
        SaveDataToFile(quickFullSavePath);
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

    public void DeleteQuickSave()
    {
        if (File.Exists(quickFullSavePath))
        {
            try
            {
                File.Delete(quickFullSavePath);
                Debug.Log("Deleted quick-save");
            }
            catch (IOException e)
            {
                Debug.Log("IOException when deleting quick-save: " + e);
            }
        }
        else
        {
            Debug.Log("Quick-save file was not found when attempting to delete");
        }

    }
    private void UpdatePlayerData()
    {
        //Update actual information in save data object.
    }


    private void OnApplicationQuit()
    {
        //Gem og luk save data fil korrekt.
        SaveDataToFile(permFullSavePath);
        DeleteQuickSave();
    }

}
