using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    Collider2D m_Collider;
    private Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim=GetComponent<Animator>();
        StartCoroutine(waiter());
        m_Collider = GetComponent<BoxCollider2D>();
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);
        m_Collider.enabled = !m_Collider.enabled;
       // Debug.Log("wait over");
    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log("Collider.enabled = " + m_Collider.enabled);
    }

    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("tag : " + other.gameObject.tag.ToString());
        if(other.gameObject.tag == "Ground")
        {
           Anim.SetBool("front",true);
           Anim.SetBool("back",false);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground"){
            Anim.SetBool("front",false);
            Anim.SetBool("back",true);
        }
    }
}
