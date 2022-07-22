using System.Collections.Generic;
using UnityEngine;
using System;


public class SnakeController : MonoBehaviour
{
    //set subject for observer
    public static event Action onEat;
    public static event Action<List<Transform>> afterEat;
    public static event Action<List<Transform>> onStart;
    public static event Action<bool> onDeath;
    //declare snake direction
    private Vector2 direction;
    //save snakebody position
    public List<Transform> body;
    //set body prefab to Instantiate when snake eat food
    public GameObject bodyPrefab;

    //set initial value, invoke onstart subject
    public void Start(){
        body = new List<Transform>();
        body.Add(this.transform);
        direction = Vector2.zero;
        onStart?.Invoke(body);
    }
    //control snake from KeyCode WSAD
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
    //Snake move relate to timestep and head position
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
    //Instantiate new snake body when snake eat food
    private void Grow(){
        GameObject segment = Instantiate(bodyPrefab);
        segment.transform.position = this.transform.position;
        body.Add(segment.transform);
    }
    //Clear snake body when death or reset game, reset snake position and direction
    private void ResetGrow(){
        for (int i = 1; i < body.Count; i++){
            Destroy(body[i].gameObject);
        }
        body.Clear();
        body.Add(this.transform);
        direction = Vector2.zero;
        this.transform.position = Vector3.zero;
    }
    //merge function/invoke to new function for easy to use
    private void Eat(GameObject target){
        Grow();
        Destroy(target);
        afterEat?.Invoke(body);
        onEat?.Invoke();
    }

    public void Death(){
        ResetGrow();
        onDeath?.Invoke(true);   
    }
    //check if snake is collide with food,obstacle or wall and call a function
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Food"){
            Eat(other.gameObject);
        }
        else if(other.tag == "Player"||other.tag == "Wall"){
            Death();
        }
    }
}