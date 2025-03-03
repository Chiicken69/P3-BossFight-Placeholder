using UnityEngine;

public class SceneDataThing : MonoBehaviour
{
    private SaveDataManager _saveDataManager;
    [SerializeField] private bool _changeScene = false;
    [SerializeField] private bool _saveDataToQuickFileOnLoad = false;


    void Start()
    {
        _saveDataManager = GameObject.FindGameObjectWithTag("SDM").GetComponent<SaveDataManager>(); 
        _saveDataManager.PushLoadedData(_changeScene);
        if(_saveDataToQuickFileOnLoad)
        {
            _saveDataManager.SaveDataToQuickFile();
        }
        
    
    }
    
    


}
