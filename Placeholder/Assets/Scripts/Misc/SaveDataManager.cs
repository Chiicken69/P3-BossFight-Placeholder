using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveDataManager : MonoBehaviour
{
    [Header("Save data information")]
    [SerializeField] private string permSaveFileName = @"";
    [SerializeField] private string quickSaveFileName = @"";

    private string saveFolderPath = @"";
    private string permFullSavePath = @"";
    private string quickFullSavePath = @"";
    public SaveData saveDataObject;

    [Header("References (Should get auto-loaded, if they dont we got an issue)")]
   
    [Tooltip("Finds object with 'Player' tag")]
    [SerializeField] private GameObject player;
    [SerializeField] private int sceneIndex;

    //Player scripts with data, get from player object.

    [SerializeField] private PlayerAttack PlayerAttackScript;
    [SerializeField] private HealthSystem healthSystem;


    private static GameObject staticSDMobject;


    void Awake()
    {
        //Only one save data manager
        if(staticSDMobject == null)
        {
            staticSDMobject = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
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



        

    }

    private void Start()
    {
        GetReferenceScripts();

        //Load correct save file
        //There is a difference between if a player just died / is reloading from a save between sessions.
        try
        {

            if (File.Exists(quickFullSavePath)) //Prøver først at loade fra quick save, ift. f.eks. hvis man bare går ind på en ny scene
            {
                LoadSaveDataFromFile(quickFullSavePath);
                Debug.Log("Loaded save data from quick");
            }
            else if (File.Exists(permFullSavePath)) //Er der ingen quick save, er det fordi man måske fortsætter et spil fra en forrig session
            {
                LoadSaveDataFromFile(permFullSavePath);
                Debug.Log("Loaded save data from perm");
            }
            else                                      //Ellers starter man et nyt spil
            {
                //Hvilken fil skal skabes her?
                //Quick?
                //Perm skal vel kun laves ved "perm save points" osv så?
                Debug.Log("No save data file found on loading. Creating quick save file.");
                CreateSaveFile(quickFullSavePath);

            }
        }
        catch (IOException e)
        {
            Debug.Log("Error when loading save data." + e.ToString());
            throw;
        }

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

        /*[SerializeField]
        public Vector2 PlayerPos;
        Dont fuck with position for now, just use default pos defined in editor :>
        */


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
            //CreateSaveFile(savePath);
            Debug.Log("Auto-creation of save-files disabled. Creation aborted.");
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
                PushLoadedData();

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
        UpdatePlayerData();
        SaveDataToFile(permFullSavePath);
    }

    public void SaveDataToQuickFile()
    {
        UpdatePlayerData();
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
        saveDataObject.PlayerHP = healthSystem._currentHealth;
        saveDataObject.PlayerBullets = PlayerAttackScript._currentAmmoLoaded;
        saveDataObject.SceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    private void PushLoadedData()
    {
        //Loaded som data to save data object, now we push this to the actual game state.
        healthSystem._currentHealth = saveDataObject.PlayerHP;
        PlayerAttackScript._currentAmmoLoaded = saveDataObject.PlayerBullets;
        SceneManager.LoadScene(saveDataObject.SceneIndex);
    }

    private void GetReferenceScripts()
    {
        //This is the scripts data is pulled from / pushed to.
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerAttackScript = player.GetComponent<PlayerAttack>();
        healthSystem = player.GetComponent<HealthSystem>();
    }


    private void OnApplicationQuit()
    {
        //Gem og luk save data fil korrekt.
        //NVM perm data gemmes kun ved save points
        //SaveDataToFile(permFullSavePath);
        DeleteQuickSave();
    }

}
