using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARRecoilScript : MonoBehaviour
{
    [SerializeField] GameObject m_akm;
    ARBulletCount m_arbulletCounScript;

    float sam = 0f;
    // Start is called before the first frame update
    void Start()
    {
        m_arbulletCounScript = m_akm.GetComponent<ARBulletCount>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //if (m_arbulletCounScript.m_count > 0) //弾数をがゼロかゼロより大きかったら実行
        //{
        //    if (Input.GetMouseButton(0)) //左クリックされたら実行
        //    {
        //        this.gameObject.transform.Rotate(0.1f, 0f, 0f);
        //        sam += 0.1f;
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    this.gameObject.transform.Rotate(-sam, 0f, 0f);
        //    sam = 0f;
        //}
    }
}
