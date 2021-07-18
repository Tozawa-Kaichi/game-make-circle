using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ハンドガン用
/// Rayを飛ばし当たり判定を実装する為のスクリプト
/// </summary>
public class ARRay : MonoBehaviour
{
    /// <summary>見せかけの銃弾を飛ばす始点</summary>
    [SerializeField] GameObject m_bulletSpwan;
    [SerializeField] GameObject m_muzzleFlash;
    /// <summary> 見せかけの銃弾 </summary>
    [SerializeField] GameObject m_BulletFake;
    //[SerializeField] GameObject m_HandGun;
    /// <summary>  Reticleの取得 </summary>
    [SerializeField] Image m_ReticleUI;
    /// <summary> MuzzleFlashを消すため時間</summary>
    private float m_time = 0f;
    /// <summary>Scriptを参照する </summary>
    ARBulletCount m_arbulletCountScript;
    /// <summary>発砲音を出すリソース</summary>
    private AudioSource Audio;
    public AudioClip Shooting_Sound; //発砲音

    [SerializeField] float m_in = 0.15f;

    [SerializeField] LayerMask m_mask; //Rayが当たったオブジェクトのレイヤーを取得 今回はEnemy

    // Start is called before the first frame update
    void Start()
    {
        m_arbulletCountScript = this.gameObject.GetComponent<ARBulletCount>();//銃弾を減らす為にBulletCountスクリプトを取得
        Audio = gameObject.AddComponent<AudioSource>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //IEnumerator Fire1()
    //{
    //    while (true)
    //    {
    //        Audio.PlayOneShot(Shooting_Sound); //発砲音
    //        m_arbulletCountScript.m_count--;
    //        yield return new WaitForSeconds(m_in);
    //    }
    //}

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
        Ray ray = Camera.main.ScreenPointToRay(m_ReticleUI.rectTransform.position); //カメラからRayを飛ばす
        //Ray ray = new Ray(m_bulletSpwan.transform.position, m_bulletSpwan.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red, 5); //Scene内でRayをみれるようにする
        if (Physics.Raycast(ray, out hit, 200.0f, m_mask)) //Ray当たり判定を使い腰うち時のReticleUIの色を変える
        {
            m_ReticleUI.color = Color.red; //Rayが当たっていたら色を赤にする
        }
        else
        {
            m_ReticleUI.color = Color.white; //Rayが当たってなかったら色を白にする
        }

        if (Input.GetMouseButton(0)&& m_arbulletCountScript.m_count != 0 && !m_arbulletCountScript.m_reloadFlag) //左クリックしたら発砲する
        {
            //StartCoroutine(Fire1());

            if (m_time > 0.1f)
            {
                Audio.PlayOneShot(Shooting_Sound); //発砲音
                m_arbulletCountScript.m_count--;
                m_time = 0f;
            }

            if (m_time > 0.1f)
            {
                m_time = 0f;
            }
            m_muzzleFlash.SetActive(true);
            //GameObject newBullet = Instantiate(m_BulletFake, this.gameObject.transform.position, this.gameObject.transform.rotation); //見せかけの銃弾をつくる
            //newBullet.name = m_BulletFake.name;//見せかけの銃弾の名前を変える

            if (Physics.Raycast(ray, out hit, 150.0f, m_mask)) //当たり判定の処理を行う
            {
                var h = hit.collider.gameObject.GetComponent<target>();
                if (h)
                {
                    h.Hit();
                }
                //Destroy(newBullet, 0.1f);//見せかけの銃弾を削除
            }
            
        }
        //else if(Input.GetMouseButtonUp(0))
        //{
        //    StopCoroutine(Fire1());
        //}

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
