using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //set Text components for show current score
    public Text displayCurrentScore;
    //save current score
    public int currentScore;
    //set spawn area for food
    public BoxCollider2D gridArea;
    private Bounds bounds;
    //set food prefab for Instantiate new food when snake eat food
    public GameObject foodPrefab;
    //set gameover popup when snake deaded
    public GameObject gameOverPopup;
    //set Text components for show final score
    public Text displayFinalScore;
    //save snake Head to List
    private List<Transform> baseBody;
    
    //set observer
    private void OnEnable(){
        SnakeController.onStart += spawnFood;
        SnakeController.onStart += initalBody;
        SnakeController.onDeath += resetGame;
        SnakeController.onEat += increaseScore;
        SnakeController.afterEat += spawnFood;
        PauseMenu.onReset += resetGame;
    }

    //set default timestep(difficult), get max min area, assign score value
    private void Start(){
        Time.fixedDeltaTime = 0.06f;
        bounds = gridArea.bounds;
        currentScore = 0;
    }
    //save snake head for parse to spawnFood when reset game
    private void initalBody(List<Transform> startBody){
        baseBody = startBody;
    }
    //increase current score and show current score
    private void increaseScore(){
        currentScore += 1;
        displayCurrentScore.text = "Score: " + currentScore.ToString();
    }
    //spawn food which food will not overlap with snake
    private void spawnFood(List<Transform> snakeBody){
        bool isSpawn = true;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector3 newPos = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

        for (int i = snakeBody.Count - 1; i > 0; i--){
            if ((Mathf.Round(snakeBody[i].position.x) == newPos.x) && (Mathf.Round(snakeBody[i].position.y) == newPos.y)){
                spawnFood(snakeBody);
                isSpawn = false;
            }
        }
        if (isSpawn == true){
            GameObject spawnedFood = Instantiate(foodPrefab);
            spawnedFood.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        }   
    }
    //clear all food GameObject in screen when reset
    private void clearFood(){
        GameObject[] food;
        food = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject foodItem in food){
            Destroy(foodItem);
        }
    }
    //show Gameover popup and score when snake deaded, reset current score, clear&spawn new food
    private void resetGame(bool isGameOver){
        if (isGameOver){
            gameOverPopup.SetActive(true);
            displayFinalScore.text = "Score: " + currentScore.ToString();
        }
        currentScore = 0;
        displayCurrentScore.text = "Score: " + currentScore.ToString();
        clearFood();
        spawnFood(baseBody);
        
    }
    //disable observer
    private void OnDisable(){
        SnakeController.onStart -= spawnFood;
        SnakeController.onStart -= initalBody;
        SnakeController.onDeath -= resetGame;
        SnakeController.onEat -= increaseScore;
        SnakeController.afterEat -= spawnFood;
        PauseMenu.onReset -= resetGame;
    }
}
