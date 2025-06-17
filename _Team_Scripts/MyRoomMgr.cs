using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using SimpleJSON;

public class MyRoomMgr : MonoBehaviour
{
    [Header("====== Button ======")]
    public Button m_UnitBtn = null;
    public Button m_DefenseBtn = null;
    public Button m_TowerSetBtn = null;

    [Header("====== UnitPanel ======")]
    public GameObject m_UnitPanel = null;
    public Button m_TankBtn = null;
    public Button m_PlaneBtn = null;
    public Button m_Missile = null;
    public Transform m_Content = null;
    public Transform m_UnitAttackContent = null;
    public GameObject m_UnitNode = null;
    public GameObject m_UnitAttackSettingNode = null;

    public Button m_UnitSaveBtn = null;

    [Header("====== DefensePanel ======")]
    public GameObject m_DefensePanel = null;
    public Button m_DefenseSaveBtn = null;

    [Header("====== TowerSetPanel ======")]
    public GameObject m_TowerSetPanel = null;
    public Button m_TowerSetSaveBtn = null;

    [Header("====== GameStart ======")]
    public Button m_StartBtn = null;
    public GameObject m_StartUserSellMapPanel = null;
    public Button[] m_StartUserSellMapBtn = null;
    public Button m_StartUserSellMapPanelCloseBtn = null;

    // Start is called before the first frame update
    void Start()
    {
        GlobarValue.MakeUnit();
        GlobarValue.MakeMapSave();
        MakeNode();
        MakeAttackSettingNode();
        //MakeAttackSettingNode(); //밑에 함수 완성후 주석 풀어주시면 됩니다.

        if (m_UnitBtn != null)
            m_UnitBtn.onClick.AddListener(() =>
            {
                m_UnitPanel.SetActive(true);
                //ToDo Load Data
            });

        if (m_DefenseBtn != null)
            m_DefenseBtn.onClick.AddListener(() =>
            {
                m_DefensePanel.SetActive(true);
                //ToDo Load Data
            });

        if (m_TowerSetBtn != null)
            m_TowerSetBtn.onClick.AddListener(() =>
            {
                m_TowerSetPanel.SetActive(true);
                //ToDo Load Data
            });

        //------------------------임시용
        if (m_StartBtn != null)
            m_StartBtn.onClick.AddListener(() =>
            {
                if (MapSetCheck() == true)
                    m_StartUserSellMapPanel.SetActive(true);
                else
                    Debug.Log("모든 맵에 타워 배치가 되어 있지 않습니다.");
            });
        
        if (m_StartUserSellMapPanelCloseBtn != null)
            m_StartUserSellMapPanelCloseBtn.onClick.AddListener(() =>
            {
                if(m_StartUserSellMapPanel.activeSelf == true)
                {
                    m_StartUserSellMapPanel.SetActive(false);
                }
            });

        for (int i = 0; i < m_StartUserSellMapBtn.Length; i++)
        {
            int index = i;

            if (m_StartUserSellMapBtn[i] != null)
            {
                m_StartUserSellMapBtn[i].onClick.AddListener(() => 
                {
                    StartBtnClick(index);
                });
            }
        }
        //------------------------임시용

        if (m_UnitSaveBtn != null)
            m_UnitSaveBtn.onClick.AddListener(() =>
            {
                m_UnitPanel.SetActive(false);
                //ToDo Save Data
            });

        if (m_DefenseSaveBtn != null)
            m_DefenseSaveBtn.onClick.AddListener(() =>
            {
                m_DefensePanel.SetActive(false);
                //ToDo Save Data
            });

        if (m_TowerSetSaveBtn != null)
            m_TowerSetSaveBtn.onClick.AddListener(() =>
            {
                m_TowerSetPanel.SetActive(false);
                //ToDo Save Data
            });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeNode()
    {
        for (int i = 0; i < GlobarValue.g_UnitList.Count; i++)
        {
            GameObject a_Node = Instantiate(m_UnitNode);
            UnitNode a_UnitNode = a_Node.GetComponent<UnitNode>();
            a_UnitNode.m_UnitNumber = i;
            a_UnitNode.m_NameText.text = GlobarValue.g_UnitList[i].m_UnitName;
            a_Node.transform.SetParent(m_Content, false);
        }
    }

    void MakeAttackSettingNode() //
    {
        for (int i = 0; i < GlobarValue.g_UnitList.Count; i++)
        {
            GameObject a_Node = Instantiate(m_UnitAttackSettingNode);
            UnitAttackSetting_Node a_UnitNode = a_Node.GetComponent<UnitAttackSetting_Node>();
            a_UnitNode.m_UnitNumber = i;
            a_UnitNode.m_UnitName.text = GlobarValue.g_UnitList[i].m_UnitName;
            a_UnitNode.SetItem(GlobarValue.g_UnitList[i].m_UnitSpr);
            a_Node.transform.SetParent(m_UnitAttackContent, false);
        }
    }

    void StartBtnClick(int _index)//임시 함수
    {
        if (GlobarValue.g_MapList[_index].m_SetMapCheck == false)
        {
            Debug.Log("선택한 맵은 타워세팅이 되어 있지 않습니다.");
            return;
        }

        if (_index == (int)UserMap.MAP1)
        {
            GlobarValue.g_UserMap = (UserMap)_index;
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
        else if (_index == (int)UserMap.MAP2)
        {
            GlobarValue.g_UserMap = (UserMap)_index;
            UnityEngine.SceneManagement.SceneManager.LoadScene("InGameMap2");
        }
    }

    bool MapSetCheck()
    {
        bool a_Check = false;

        for (int i = 0; i < GlobarValue.g_MapList.Count; i++)
        {
            if (GlobarValue.g_MapList[i].m_SetMapCheck == true)
            {
                a_Check = true;
            }
        }

        return a_Check;
    }
}
