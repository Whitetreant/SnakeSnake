using UnityEngine;

public class DifficultSetting : MonoBehaviour
{
    public GameObject Snake;
    public void changeTimeStep(float timestep){
        SnakeController Snakebody = Snake.GetComponent<SnakeController>();
        Snakebody.ResetGame();
        Time.fixedDeltaTime = timestep;
    }
}
