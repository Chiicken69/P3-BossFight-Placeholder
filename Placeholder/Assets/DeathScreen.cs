using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public VideoPlayer deathVideo; 
    public GameObject deathScreen; 

    void Start()
    {
        deathScreen.SetActive(false); 
        deathVideo.loopPointReached += OnVideoEnd; 
    }

    private void Update()
    {

        if (GetComponent<HealthSystem>()._currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        deathScreen.SetActive(true); 
        deathVideo.Play(); 
        Time.timeScale = 0f;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
