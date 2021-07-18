using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestartandend : MonoBehaviour
{
    [SerializeField] Canvas gameStartCanvas;
    // Start is called before the first frame update
    void Start()
    {
        gameStartCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
