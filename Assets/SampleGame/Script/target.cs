using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    Animator m_anim = default;
    bool flag;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (gameObject)
        {
            flag = true;
        }

    }

    public void Hit()
    {
        //flag = true;
        m_anim.Play("Hit");
    }
}