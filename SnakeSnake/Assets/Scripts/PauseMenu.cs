using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //set onReset subject
    public static event Action<bool> onReset;
    //save gamestate
    private bool isPaused;
    //set pause menu gameobject to disable/enable
    public GameObject pauseMenuUI;

    //set initial value
    void Start(){
        isPaused = false;
    }
    //pause/resume when press escape
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
    //Disable pause menu, set timescale to normal, set isPaused state
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    //enable pause menu, set timescale to zero, set isPaused state
    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    //change difficult by change timestep and reset game
    public void changeDifficulty(float timeStep){
        Time.fixedDeltaTime = timeStep;
        onReset?.Invoke(false);
    }
}
