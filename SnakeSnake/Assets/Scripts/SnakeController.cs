using System.Collections.Generic;
using UnityEngine;
using System;


public class SnakeController : MonoBehaviour
{
    public static event Action onEat;
    public static event Action<List<Transform>> afterEat;
    public static event Action<List<Transform>> onStart;
    public static event Action<bool> onDeath;
    private Vector2 direction;
    public List<Transform> body;
    public GameObject bodyPrefab;

    public void Start(){
        body = new List<Transform>();
        body.Add(this.transform);
        direction = Vector2.zero;
        onStart?.Invoke(body);
    }
    
    private void Update()
    {
        // switch (Input.inputString.ToLower())
        // {
        //     case "w":
        //         direction = Vector2.up;
        //         break;
        //     case "s":
        //         direction = Vector2.down;
        //         break;
        //     case "a":
        //         direction = Vector2.left;
        //         break;
        //     case "d":
        //         direction = Vector2.right;
        //         break;
        // }
     
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
        GameObject segment = Instantiate(bodyPrefab);
        segment.transform.position = this.transform.position;
        body.Add(segment.transform);
    }
    
    private void ResetGrow(){
        for (int i = 1; i < body.Count; i++){
            Destroy(body[i].gameObject);
        }
        body.Clear();
        body.Add(this.transform);
        direction = Vector2.zero;
        this.transform.position = Vector3.zero;
    }

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

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Food"){
            Eat(other.gameObject);
        }
        else if(other.tag == "Player"||other.tag == "Wall"){
            Death();
        }
    }
}