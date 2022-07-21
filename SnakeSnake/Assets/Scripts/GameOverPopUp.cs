using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            this.gameObject.SetActive(false);
        }
    }
}
