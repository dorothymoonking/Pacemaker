using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UserMap
{
    MAP1,
    MAP2,
    MAP3,
}

public enum TowerType
{
    MachineGun_Tower,
    Missile_Tower,
    Emp_Tower,
    Super_MachineGun_Tower,
    None
}

public enum UnitType
{
    MBT,
    Armored,
    Heavy_Tank,
    Light_Tank
}

public class UnitTank
{
    public UnitType m_UnitType = UnitType.MBT;
    public string m_UnitName = "";
    public int m_UnitHP = 0;
    public float m_UnitAttackRate = 0.0f;
    public Sprite m_UnitSpr = null;

    public string SetName(UnitType _tpye)
    {
        string a_Name = "";
        if (_tpye == UnitType.MBT)
            a_Name = "T-80U";
        else if (_tpye == UnitType.Heavy_Tank)
            a_Name = "IS-2";
        else if (_tpye == UnitType.Armored)
            a_Name = "BMP-3";
        else if (_tpye == UnitType.Light_Tank)
            a_Name = "2S25";

        return a_Name;
    }

    public int SetHP(UnitType _tpye)
    {
        int a_HP = 0;
        if (_tpye == UnitType.MBT)
            a_HP = 200;
        else if (_tpye == UnitType.Heavy_Tank)
            a_HP = 400;
        else if (_tpye == UnitType.Armored)
            a_HP = 150;
        else if (_tpye == UnitType.Light_Tank)
            a_HP = 100;

        return a_HP;
    }

    public float SetAttackRate(UnitType _tpye)
    {
        float a_AttackRate = 0.0f;
        if (_tpye == UnitType.MBT)
            a_AttackRate = 10.0f;
        else if (_tpye == UnitType.Heavy_Tank)
            a_AttackRate = 20.0f;
        else if (_tpye == UnitType.Armored)
            a_AttackRate = 5.0f;
        else if (_tpye == UnitType.Light_Tank)
            a_AttackRate = 3.0f;

        return a_AttackRate;
    }

    public Sprite SetSprite(UnitType _type)
    {
        Sprite a_Spr = null;
        if (_type == UnitType.MBT)
            a_Spr = Resources.Load<Sprite>("Tower1");
        else if (_type == UnitType.Heavy_Tank)
            a_Spr = Resources.Load<Sprite>("Tower2");
        else if (_type == UnitType.Armored)
            a_Spr = Resources.Load<Sprite>("Tower3");
        else if (_type == UnitType.Light_Tank)
            a_Spr = Resources.Load<Sprite>("Tower4");

        return a_Spr;
    }
}

public class MapSetting
{
    public bool m_SetMapCheck = false;
    public bool[] m_SpawnPoint;
    public TowerType[] m_TowerType;
    public UserMap m_UserMap = UserMap.MAP1;

    public void SetSpawnPoint(UserMap _map)
    {
        m_UserMap = _map;
        if (_map == UserMap.MAP1)
        {
            m_SetMapCheck = false;
            m_TowerType = new TowerType[4];
            m_SpawnPoint = new bool[4];
        }

        else if (_map == UserMap.MAP2)
        {
            m_SetMapCheck = false;
            m_TowerType = new TowerType[12];
            m_SpawnPoint = new bool[12];
        }
    }
}

public class GlobarValue
{
    //-------------------------맵정보
    public static UserMap g_UserMap = UserMap.MAP1;
    public static List<MapSetting> g_MapList = new List<MapSetting>();
    //-------------------------맵정보

    //-------------------------유닛정보(임시용)
    public static List<UnitTank> g_UnitList = new List<UnitTank>();
    //-------------------------유닛정보(임시용)


    //-------------------------유닛함수(진진원)
    public static void MakeUnit()
    {
        UnitTank a_Unit;
        for (int i = 0; i < 4; i++)
        {
            a_Unit = new UnitTank();
            a_Unit.m_UnitType = (UnitType)i;
            a_Unit.m_UnitName = a_Unit.SetName((UnitType)i);
            a_Unit.m_UnitHP = a_Unit.SetHP((UnitType)i);
            a_Unit.m_UnitAttackRate = a_Unit.SetAttackRate((UnitType)i);
            a_Unit.m_UnitSpr = a_Unit.SetSprite((UnitType)i);
            g_UnitList.Add(a_Unit);
        }
    }
    //-------------------------유닛함수(진진원)

    public static void MakeMapSave()
    {
        for (int i = 0; i < 3; i++)
        {
            MapSetting a_MapSetting = new MapSetting();
            a_MapSetting.SetSpawnPoint((UserMap)i);
            g_MapList.Add(a_MapSetting);
        }
    }

    public static void UserMapListSend() //새롭게 저장된 유저의 맵정보를 DB에 보낸다.
    {

    }

    public static void MakeMapLoad() //DB에 저장된 맵정보를 가져온다.
    {
        
    }
}