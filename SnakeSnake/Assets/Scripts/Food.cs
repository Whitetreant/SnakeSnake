using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public GameObject Snake;
    private Bounds bounds;

    private void Start(){
        RandomPos();
        bounds = this.gridArea.bounds;
    }

    private void RandomPos(){
        bool isSpawn = true;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        
        Vector3 newPos = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        SnakeController Snakebody = Snake.GetComponent<SnakeController>();

        for (int i = Snakebody.body.Count - 1; i > 0; i--){
            if ((Mathf.Round(Snakebody.body[i].position.x) == newPos.x) && (Mathf.Round(Snakebody.body[i].position.y) == newPos.y)){
                RandomPos();
                isSpawn = false;
            }
        }
        if (isSpawn == true){
            this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if (other.name == "Snake"){
            RandomPos();
        }
    }

    

}
