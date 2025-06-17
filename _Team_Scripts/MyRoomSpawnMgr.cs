using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRoomSpawnMgr : MonoBehaviour
{
    [HideInInspector] public Button[] m_SpawnBtn = null;
    [HideInInspector] public bool[] m_CheckPoint;
    public Button[] m_UserSellTower;
    public GameObject m_SellTower_Panel = null;
    public Button m_Done_Btn = null;
    public Button m_ReSet_Btn = null;

    int m_MaxSpawn = 0;
    int m_SpawnNum = 0;

    int m_BtnNum = 0;
    Image[] a_CheckImg = null;
    Image m_CheckImg = null;

    int m_SpawnBtnLenth = 0;
    UserMap m_UserMap = UserMap.MAP1;
    [HideInInspector] public bool OpenMapSet = false;
    // Start is called before the first frame update
    void Start()
    {
        m_SpawnBtn = this.transform.GetComponentsInChildren<Button>();
        m_SpawnBtnLenth = m_SpawnBtn.Length;
        CheckMapNumber(m_SpawnBtnLenth);

        m_CheckPoint = new bool[m_SpawnBtnLenth];
        m_MaxSpawn = m_SpawnBtnLenth / 2;

        Debug.Log(m_SpawnBtn.Length.ToString());

        for(int i = 0; i < m_CheckPoint.Length; i++)
        {
            m_CheckPoint[i] = false;
        }

        for (int i = 0; i < m_SpawnBtn.Length; i++)
        {
            int index = i;
            m_SpawnBtn[i].onClick.AddListener(() => 
            {
                BtnClick(index); 
            });

        }
        
        for (int i = 0; i < m_UserSellTower.Length; i++)
        {
            int index = i;
            m_UserSellTower[i].onClick.AddListener(() => 
            {
                SellTowerType((TowerType)index);
            });

        }

        if (m_Done_Btn != null)
            m_Done_Btn.onClick.AddListener(DoneBtn);

        if (m_ReSet_Btn != null)
            m_ReSet_Btn.onClick.AddListener(ReSetBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BtnClick(int _num)
    {
        if (m_SpawnNum == m_MaxSpawn && m_CheckPoint[_num] == false)
        {
            Debug.Log("더 이상 배치를 할 수 없습니다.");
            return;
        }

        if (OpenMapSet == false)
            return;

        m_BtnNum = _num;
        a_CheckImg = m_SpawnBtn[m_BtnNum].gameObject.transform.GetComponentsInChildren<Image>();
        m_CheckImg = a_CheckImg[1];
        if (m_CheckPoint[m_BtnNum] == false)
        {
            m_SellTower_Panel.SetActive(true);
        }

        else
        {
            m_CheckImg.enabled = false;
            m_CheckPoint[m_BtnNum] = false;
            m_SpawnNum--;
        }
    }

    public void DoneBtn()
    {
        if (OpenMapSet == false)
            return;

        GlobarValue.g_UserMap = m_UserMap;

        if (m_SpawnNum == 0)
        {
            Debug.Log("세팅된 맵이 없습니다.");
            return;
        }

        GlobarValue.g_MapList[(int)m_UserMap].m_SetMapCheck = true;
        this.gameObject.transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < m_CheckPoint.Length; i++)
        {
            GlobarValue.g_MapList[(int)m_UserMap].m_SpawnPoint[i] = m_CheckPoint[i];
        }
    }

    void ReSetBtn()
    {
        if (OpenMapSet == false)
            return;

        Debug.Log("작동");

        if (m_SpawnNum == 0)
        {
            Debug.Log("세팅된 맵이 없습니다.");
            return;
        }

        m_SpawnNum = 0;

        for (int i = 0; i < m_SpawnBtn.Length; i++)
        {
            a_CheckImg = m_SpawnBtn[i].gameObject.transform.GetComponentsInChildren<Image>();
            m_CheckImg = a_CheckImg[1];
            m_CheckImg.enabled = false;
        }

        for (int i = 0; i < m_CheckPoint.Length; i++)
        {
            m_CheckPoint[i] = false;
            GlobarValue.g_MapList[(int)m_UserMap].m_SpawnPoint[i] = m_CheckPoint[i];
        }

        GlobarValue.g_MapList[(int)m_UserMap].m_SetMapCheck = false;

    }

    void SellTowerType(TowerType _Type) 
    {
        if (m_SellTower_Panel == null && m_SellTower_Panel.activeSelf == false)
            return;

        if (m_CheckImg == null)
            return;

        if (OpenMapSet == false)
            return;

        m_SpawnNum++;
        m_CheckImg.enabled = true;
        m_CheckPoint[m_BtnNum] = true;
        GlobarValue.g_MapList[(int)m_UserMap].m_TowerType[m_BtnNum] = _Type; ////GlobarValue.g_TowerType[m_SpawnNum] = _Type;
        m_SellTower_Panel.SetActive(false);
    }

    void CheckMapNumber(int _index)
    {
        if (OpenMapSet == false)
            return;

        if (_index == 4)
        {
            m_UserMap = UserMap.MAP1;
        }
        else if(_index == 12)
        {
            m_UserMap = UserMap.MAP2;
        }
    }
}
