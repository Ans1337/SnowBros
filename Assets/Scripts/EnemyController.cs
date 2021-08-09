using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    // [SerializeField] float strikingDistance;
    // [SerializeField] Transform player;
    // [SerializeField] Transform selfEnemy;

    [SerializeField] Collider2D enemyCollider;
    [SerializeField] float strikingDistance;
    [SerializeField] Transform player;
    [SerializeField] Transform selfEnemy;
    [SerializeField] Animator anime;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float jumpForce;
    [SerializeField] float movespeed;
    
    [SerializeField] GameObject child;
    bool Onground = false;
    
    private bool check;
    bool touching;

    Collider2D m_Collider;
    Collider2D childcollider;
    // Start is called before the first frame update
    void Start()
    {

        m_Collider = GetComponent<BoxCollider2D>();
        childcollider = child.GetComponent<CircleCollider2D>();
        //enemyRGB.AddForce(startDir*startForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if((player.position-selfEnemy.position).magnitude > strikingDistance)
        {
            // do nothing
            Debug.Log("chilling");
        }
        else
        {
            if(null != player.GetComponent<MyPlatform>().standingOnPlatform && null != selfEnemy.GetComponent<MyPlatform>().standingOnPlatform)
            {    
                    if(player.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name == selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name)
                    {

                        // if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y ==
                        // selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y)

                            if(player.position.x < selfEnemy.position.x)
                            {
                                // move left
                                Vector3 newPosition = selfEnemy.position;
                               newPosition.x = newPosition.x - (movespeed);
                               selfEnemy.position = newPosition;
                              // Debug.Log("need to move left");
                               transform.localRotation= Quaternion.Euler(0,0,0);
                                Debug.Log("need to move left");
                                
                            }
                            else
                            {
                                // move right
                                Debug.Log("need to move right");
                                Vector3 newPosition = selfEnemy.position;
                                newPosition.x = newPosition.x + (movespeed);
                                selfEnemy.position = newPosition;
                                transform.localRotation= Quaternion.Euler(0,180,0);
                            }
                        }
                        else if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y <
                        selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y)
                        {
                            // jump down
                            //m_Collider.enabled = !m_Collider.enabled;
                            m_Collider.enabled = false;
                            childcollider.enabled = false;
                            StartCoroutine(waiter());
                            Debug.Log("need to jump down");
                            StartCoroutine(waitboi());

                        }
                        else
                        {
                            Debug.Log("standing on: " + selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name);
                            if(null!=selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>() &&
                            selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x <
                            selfEnemy.position.x)
                            {
                                Vector3 newPosition = selfEnemy.position;
                               newPosition.x = newPosition.x - (movespeed);
                               selfEnemy.position = newPosition;
                              // Debug.Log("need to move left");
                               transform.localRotation= Quaternion.Euler(0,0,0);
                                if(Mathf.Abs(rigid.velocity.y) == 0f && child.GetComponent<moveup>().ongroundcheck())
                                { 
                                    //this.head.GetComponent<moveup>().ongroundcheck()
                                    // if(Onground == true){
                                    rigid.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
                                    StartCoroutine(waitboi());
                                    //     Onground = false;
                                    // }
                                }
                                // jump up
                                Debug.Log("need to jump up, after moving left");
                            }
                            else if(null!=selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>() &&
                            selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x >
                            selfEnemy.position.x)
                            {
                                Debug.Log("need to move right");
                                Vector3 newPosition = selfEnemy.position;
                                newPosition.x = newPosition.x + (movespeed);
                                selfEnemy.position = newPosition;
                                transform.localRotation= Quaternion.Euler(0,180,0);
                               if(Mathf.Abs(rigid.velocity.y) == 0f && child.GetComponent<moveup>().ongroundcheck())
                                { 
                                    //this.head.GetComponent<moveup>().ongroundcheck()
                                    // if(Onground == true){
                                    rigid.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
                                    StartCoroutine(waitboi());
                                    //     Onground = false;
                                    // }
                                }
                                // jump up
                                Debug.Log("need to jump up, after moving right");
                            }
                        }
                    }
                    else
                    {
                        if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y ==
                        selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y)
                        {
                            if(player.position.x < selfEnemy.position.x)
                            {
                                // move left to fall down
                                Debug.Log("need to move left, to fall down");
                            }
                            else
                            {
                                // move right to fall down
                                Debug.Log("need to move right, to fall down");
                            }
                        }

                    }
            }
        }        

    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
            {
                Onground = true;
            }
        if(other.gameObject.CompareTag("ball"))
        {
            Destroy(this.gameObject,0.6f);
        }
    }
    IEnumerator waiter()
    {
        
        yield return new WaitForSeconds(0.35f);
        m_Collider.enabled = true;
        childcollider.enabled = true;
       // Debug.Log("wait over");
    }

    IEnumerator waitboi()
    {
        
        yield return new WaitForSeconds(0.2f);
       // Debug.Log("wait over");
    }
}

