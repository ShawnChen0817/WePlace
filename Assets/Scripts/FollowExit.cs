using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FollowExit : MonoBehaviour
{
    public Animator anim;
    private Button cross;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    public void Exit() {
        anim.SetBool("Exit",true); 
    }
}
