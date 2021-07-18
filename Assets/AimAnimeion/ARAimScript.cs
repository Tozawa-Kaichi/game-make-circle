using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Aimする為のスクリプト
/// アニメーションを使いAimを再現した
/// </summary>
public class ARAimScript : MonoBehaviour
{
    [SerializeField] Image m_ReticleUI; //Reticleを取得
    public Vector3 vector;

    Animator m_anim;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_anim.SetBool("IsAim", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) //右クリックでエイム
        {
            vector = Vector3.zero;
            //float present_Location = (Time.deltaTime * m_Speed) / m_distanceTwo; //Lerpで使う自身の位置を求める
            //transform.position = Vector3.Lerp(this.gameObject.transform.position, m_AimPos.position, present_Location); Lerpを使い銃を滑らかに動かす
            m_anim.SetBool("IsAim", true);
            m_ReticleUI.gameObject.SetActive(false); //Reticleを消す
        }
        else
        {
            //腰うち時Rayがばらける処理を追加
            vector = new Vector3(Random.RandomRange(0, m_ReticleUI.rectTransform.sizeDelta.x), Random.RandomRange(0, m_ReticleUI.rectTransform.sizeDelta.y), 0) / 4.5f;
            m_ReticleUI.gameObject.SetActive(true); //Reticleを表示させる
            m_anim.SetBool("IsAim", false);
        }

    }

}
