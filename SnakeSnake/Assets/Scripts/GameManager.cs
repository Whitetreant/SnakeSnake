using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text displayCurrentScore;
    public int score;
    public BoxCollider2D gridArea;
    private Bounds bounds;
    public GameObject foodPrefab;
    public GameObject gameOverPopup;
    public Text displayFinalScore;
    private List<Transform> baseBody;
    
    private void OnEnable(){
        SnakeController.onStart += spawnFood;
        SnakeController.onStart += initalBody;
        SnakeController.onDeath += resetGame;
        SnakeController.onEat += increaseScore;
        SnakeController.afterEat += spawnFood;
        PauseMenu.onReset += resetGame;
    }

    private void Start(){
        Time.fixedDeltaTime = 0.06f;
        bounds = gridArea.bounds;
        score = 0;
    }
    
    private void initalBody(List<Transform> startBody){
        baseBody = startBody;
    }

    private void increaseScore(){
        score += 1;
        displayCurrentScore.text = "Score: " + score.ToString();
    }

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

    private void clearFood(){
        GameObject[] food;
        food = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject foodItem in food){
            Destroy(foodItem);
        }
    }

    private void resetGame(bool isGameOver){
        if (isGameOver){
            gameOverPopup.SetActive(true);
            displayFinalScore.text = "Score: " + score.ToString();
        }
        score = 0;
        displayCurrentScore.text = "Score: " + score.ToString();
        clearFood();
        spawnFood(baseBody);
        
    }

    private void OnDisable(){
        SnakeController.onStart -= spawnFood;
        SnakeController.onStart -= initalBody;
        SnakeController.onDeath -= resetGame;
        SnakeController.onEat -= increaseScore;
        SnakeController.afterEat -= spawnFood;
        PauseMenu.onReset -= resetGame;
    }
}
