using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //destroy gameover popup when press any key
        if(Input.anyKey){
            this.gameObject.SetActive(false);
        }
    }
}
