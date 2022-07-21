using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static event Action<bool> onReset;
    private bool isPaused;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Start(){
        isPaused = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void changeDifficulty(float timeStep){
        Time.fixedDeltaTime = timeStep;
        onReset?.Invoke(false);
    }
}
