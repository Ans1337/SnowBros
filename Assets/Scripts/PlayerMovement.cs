using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private int lives;

    [SerializeField] Text txtlives;
    
    private float Vertical;
    [SerializeField] Transform PlayerFace;
    Vector2 Facing;
    Vector3 SnowBallStartOffSet;

    [SerializeField] GameObject Ball;
    //end here
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;


    private float horizontal;
    private Rigidbody2D rb;
    private Animator anim;


    bool jmp = false;
    bool isColliding = true;
    bool moveright = true;
    bool moveleft = false;
    
    // Start is called before the first frame update
    void Start()
    {
        lives  = 3;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Velocity is : " + Mathf.Abs(rb.velocity.y).ToString());
        if(Mathf.Abs(rb.velocity.y) < 0.0001)
        {
            if(Input.GetKeyDown(KeyCode.Space) && isColliding == true)
            {
                    isColliding = false;
                    rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
                    anim.SetTrigger("jmp"); 
            }      
        }
        horizontal = Input.GetAxis("Horizontal");
        //here
        if(Input.GetButtonDown("Fire1")){
              //Debug.Log(Facing.x.ToString());
            anim.SetTrigger("throwit");
          if(Facing.x==1){
              SnowBallStartOffSet.x=Mathf.Abs(SnowBallStartOffSet.x);
          }
          else if(Facing.x==-1){
              SnowBallStartOffSet.x=Mathf.Abs(SnowBallStartOffSet.x)*-1;            
          }
         Instantiate(Ball,(PlayerFace.position)+SnowBallStartOffSet,Quaternion.identity,this.transform);
        }  
        Vector3 PlayerRotation=PlayerFace.rotation.eulerAngles; //Rotating Player
              if(horizontal>0){
                   PlayerRotation.y=0;
                   //Debug.Log(PlayerRotation.y.ToString());
               }
               else if(horizontal<0){
                   PlayerRotation.y=180;
                   //Debug.Log(PlayerRotation.y.ToString());
               }

         PlayerFace.rotation=Quaternion.Euler(PlayerRotation);
         Facing.x=(PlayerFace.rotation.y==0 ? 1 : -1);
         //ends here
        if(horizontal > 0.01){
            moveright = true;
            moveleft = false;
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
        else if(horizontal < -0.01){
            moveright = false;
            moveleft = true;
            transform.localRotation = Quaternion.Euler(0,180,0);
        }
        if(isColliding){
            anim.SetFloat("walking" , horizontal);
        }
        else{
            anim.SetFloat("walking" , 0);

        }

        float xForce = horizontal * 10*Time.fixedDeltaTime*movementSpeed;
        rb.velocity = new Vector2(xForce,rb.velocity.y);  

    }
    private void FixedUpdate(){
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
        {
                if(collision.gameObject.CompareTag("Ground"))
                {
                    //Debug.Log("Colliding");
                    isColliding = true;
                    jmp = false;
                }
                // if (collision.gameObject.CompareTag("enemy"))
                // {
                //     anim.SetTrigger("destroyball");
                //     Destroy(this.gameObject,1f);
                // }
                if(collision.gameObject.CompareTag("enemy"))
                {
                    //Destroy(this.gameObject,1f);
                    Physics.IgnoreLayerCollision (9,8, true);
                    lives--;
                    txtlives.text = "Lives:  " + lives.ToString();
                    anim.SetTrigger("destroyball");
                    //Instantiate(Player,new Vector3(0,-5,0), Quaternion.identity);
                     if(lives == 0)
                    {
                        SceneManager.LoadSceneAsync(2);
                    }
                }
        }

    public Vector2 GetFacingDirection(){
        return Facing;
    }
}


