using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetT.OpenUI(PanelName.BeginGamePanel);
       // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
