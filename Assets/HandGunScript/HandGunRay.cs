using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ハンドガン用
/// Rayを飛ばし当たり判定を実装する為のスクリプト
/// </summary>
public class HandGunRay : MonoBehaviour
{
    /// <summary>見せかけの銃弾を飛ばす始点</summary>
    [SerializeField] GameObject m_bulletSpwan;　
    [SerializeField] GameObject m_muzzleFlash;
    /// <summary> 見せかけの銃弾 </summary>
    [SerializeField] GameObject m_BulletFake;
    [SerializeField] GameObject m_HandGun;
    /// <summary>  Reticleの取得 </summary>
    [SerializeField] Image m_ReticleUI;
    /// <summary> MuzzleFlashを消すため時間</summary>
    private float m_time = 0f;
    /// <summary>Scriptを参照する </summary>
    HandGunBulletCount BulletCount;
    /// <summary>発砲音を出すリソース</summary>
    private AudioSource Audio;
    public AudioClip Shooting_Sound; //発砲音

    [SerializeField] LayerMask m_mask; //Rayが当たったオブジェクトのレイヤーを取得 今回はEnemy

    [SerializeField] AimScript m_aimScript;
    
    // Start is called before the first frame update
    void Start()
    {
        BulletCount = m_HandGun.GetComponent<HandGunBulletCount>();//銃弾を減らす為にBulletCountスクリプトを取得
        Audio = gameObject.AddComponent<AudioSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        Fire();

        if (Input.GetKeyDown(KeyCode.Escape)) //カーソルを表示させる
        {
            Cursor.visible = true;
        }

        
    }

    private void Fire()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(m_ReticleUI.rectTransform.position + m_aimScript.vector); //カメラからRayを飛ばす
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.red, 5); //Scene内でRayをみれるようにする
        if (Physics.Raycast(ray, out hit, 20.0f, m_mask)) //Ray当たり判定を使い腰うち時のReticleUIの色を変える
        {
            m_ReticleUI.color = Color.red; //Rayが当たっていたら色を赤にする
        }
        else
        {
            m_ReticleUI.color = Color.white; //Rayが当たってなかったら色を白にする
        }

        if (Input.GetMouseButtonDown(0) && BulletCount.m_count != 0 && !BulletCount.m_reloadFlag) //左クリックしたら発砲する
        {
            Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.red, 5);
            Debug.Log(m_ReticleUI.rectTransform.position + m_aimScript.vector);
            Audio.PlayOneShot(Shooting_Sound); //発砲音
            m_muzzleFlash.SetActive(true);
            GameObject newBullet = Instantiate(m_BulletFake, this.gameObject.transform.position, this.gameObject.transform.rotation); //見せかけの銃弾をつくる
            newBullet.name = m_BulletFake.name;//見せかけの銃弾の名前を変える

            if (Physics.Raycast(ray, out hit, 100.0f, m_mask)) //当たり判定の処理を行う
            {
                var h = hit.collider.gameObject.GetComponent<target>();
                if (h)
                {
                    h.Hit();
                }
                Destroy(newBullet, 0.1f);//見せかけの銃弾を削除
            }
            BulletCount.m_count--;
        }

        if (m_time > 0.3f) // MuzzleFlashを見えなくする
        {
            m_muzzleFlash.SetActive(false);
            m_time = 0f;
        }

        if (m_time > 0.3f)
        {
            m_time = 0f;
        }
    }
}
