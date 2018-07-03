using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {

    [SerializeField] GameObject m_BattleManager;
    [SerializeField] GameObject m_PhotonManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "BattleManager"))
        {
            m_BattleManager.SetActive(!m_BattleManager.activeSelf);
        }
        if (GUI.Button(new Rect(10, 150, 150, 100), "PhotonManager"))
        {
            m_PhotonManager.SetActive(!m_PhotonManager.activeSelf);
        }

    }

}
