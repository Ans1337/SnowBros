using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveup : MonoBehaviour
{
    bool Onground = false;
    float distance;
    [SerializeField] Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Onground == false && distance < 1f){
            Vector3 newPosition = transform.position;
            newPosition.y = newPosition.y + (0.05f);
            transform.position = newPosition;
            distance = distance + 0.05f;
        }
        else{
            this.transform.position = enemy.position;
            distance = 0;
            Onground = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("TOUCHINGIGNIGN");
                Onground = true;
            }
    }

    public bool ongroundcheck(){
        return Onground;
    }
}
