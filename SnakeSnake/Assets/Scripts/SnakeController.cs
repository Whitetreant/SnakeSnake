using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    public List<Transform> body;
    private int score;
    public Transform bodyPrefab;
    public Text showText;
    private void Start(){
        body = new List<Transform>();
        body.Add(this.transform);
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && (direction!=Vector2.down)){
            direction = Vector2.up;
        }
        else if(Input.GetKeyDown(KeyCode.S) && (direction!=Vector2.up)){
            direction = Vector2.down;
        }
        else if(Input.GetKeyDown(KeyCode.A) && (direction!=Vector2.right)){
            direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.D) && (direction!=Vector2.left)){
            direction = Vector2.right;
        }
    }
    
    private void FixedUpdate()
    {
        for (int i = body.Count - 1; i > 0; i--){
            body[i].position = body[i-1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + (direction.x),
            Mathf.Round(this.transform.position.y) + (direction.y),
            0.0f
        );
    }

    private void Grow(){
        Transform segment = Instantiate(bodyPrefab);
        segment.position = this.transform.position;

        body.Add(segment);
         
    }
    
    public void ResetGame(){
        for (int i = 1; i < body.Count; i++){
            Destroy(body[i].gameObject);
        }

        body.Clear();
        body.Add(this.transform);

        this.transform.position = Vector3.zero;
        score = 0;
        showText.text = "Score: " + score.ToString();
        
    }

    private void increaseScore(){
        score += 1;
        showText.text = "Score: " + score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.name == "Food"){
            Grow();
            increaseScore();
        }
        else if(other.tag == "Player"){
            ResetGame();
        }
        else if(other.tag == "Wall"){
            ResetGame();
        }
    }
}