﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject _gPlane;
    public GameObject _gPlayer;

    public GameObject _gEnmey;
    public GameObject _gPartner;
    public List<Vector3> _lEnmeyPos_List = new List<Vector3>();    //敵人所在的位置List
    public List<Vector3> _lPartnerPos_List = new List<Vector3>();   //自己人所在的位置List
    public List<Vector3> _lCan_Move_List = new List<Vector3>();     //能移動的位置List
    public List<int> _lMove_Weight_List = new List<int>();      //格子權重的List

    public int _iWalk_Steps; //可移動的數量
    public int _iList_Index = 0; //存入List用的引數
    public int _iList_Count = 0;   //取入List內容用的引數
    private bool m_Have_Something; //判斷有沒有東西
    private bool m_Have_Walked;     //判斷這格是不是有走過
    private Vector3 m_Tmp_Pos;  //暫存計算完的下一步

    //private int m_Move_Steps;               //能移動的步數
   

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
      
        Save_CharacterPos();
        Find_Way();
        
        for (int i = 0; i < _lCan_Move_List.Count; i++)
        {
            
            //Instantiate(_gPlane, _lCan_Move_List[i], _gPlane.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Find_Way()
    {
        int Tmp_Count = 0;        //用來看是不是第一次走
        _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
        //_lCan_Move_List.Add(gameObject.transform.position);
        // for(int i = 0; i<Walk_Steps;i++)
        while (true)
        {
            
            //第一輪，上下左右
            if (Tmp_Count == 0)
            {
                _lCan_Move_List.Insert(_iList_Index, _gPlayer.transform.position);
                _lMove_Weight_List.Insert(_iList_Index, _iWalk_Steps);
                _iList_Index++;
                //向上尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z + 1) == _lEnmeyPos_List[i].z) && (_gPlayer.transform.position.x == _lEnmeyPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z + 1) == _lPartnerPos_List[i].z) && (_gPlayer.transform.position.x == _lPartnerPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, 1);
                    Save_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }
                m_Have_Something = false;

                //向下尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z - 1) == _lEnmeyPos_List[i].z) && (_gPlayer.transform.position.x == _lEnmeyPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.z - 1) == _lPartnerPos_List[i].z) && (_gPlayer.transform.position.x == _lPartnerPos_List[i].x))
                    {
                        m_Have_Something = true;
                    }
                }
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(0, 0, -1);
                    Save_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }
                m_Have_Something = false;

                //向左尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x - 1) == _lEnmeyPos_List[i].x) && (_gPlayer.transform.position.z == _lEnmeyPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x - 1) == _lPartnerPos_List[i].x) && (_gPlayer.transform.position.z == _lPartnerPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(-1, 0, 0);
                    Save_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }
                m_Have_Something = false;

                //向右尋找
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x + 1) == _lEnmeyPos_List[i].x) && (_gPlayer.transform.position.z == _lEnmeyPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_gPlayer.transform.position.x + 1) == _lPartnerPos_List[i].x) && (_gPlayer.transform.position.z == _lPartnerPos_List[i].z))
                    {
                        m_Have_Something = true;
                    }
                }
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _gPlayer.transform.position + new Vector3(1, 0, 0);
                    Save_Position();
                    _iWalk_Steps = _gPlayer.GetComponent<Character>()._iWalk_Steps;
                }

                m_Have_Something = false;
                _iList_Count++;             //改變要取出來的List位置
                Tmp_Count++;
            }
            
            //第二~底，上下左右
            if (Tmp_Count != 0)
            {
                
                //向上找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].z + 1) == _lEnmeyPos_List[i].z) && (_lCan_Move_List[_iList_Count].x == _lEnmeyPos_List[i].x) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].z + 1) == _lPartnerPos_List[i].z) && (_lCan_Move_List[_iList_Count].x == _lPartnerPos_List[i].x) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                
                if (m_Have_Something == false)
                {
                    
                    m_Tmp_Pos = _lCan_Move_List[_iList_Count] + new Vector3(0, 0, 1);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iList_Count] > 0)
                    {
                        Save_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;
                //向下找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].z - 1) == _lEnmeyPos_List[i].z) && (_lCan_Move_List[_iList_Count].x == _lEnmeyPos_List[i].x) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].z - 1) == _lPartnerPos_List[i].z) && (_lCan_Move_List[_iList_Count].x == _lPartnerPos_List[i].x) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _lCan_Move_List[_iList_Count] + new Vector3(0, 0, -1);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iList_Count] > 0)
                    {
                        Save_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;

                //向左找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].x - 1) == _lEnmeyPos_List[i].x) && (_lCan_Move_List[_iList_Count].z == _lEnmeyPos_List[i].z) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].x - 1) == _lPartnerPos_List[i].x) && (_lCan_Move_List[_iList_Count].z == _lPartnerPos_List[i].z) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _lCan_Move_List[_iList_Count] + new Vector3(-1, 0, 0);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iList_Count] > 0)
                    {
                        Save_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;


                //向右找
                //
                for (int i = 0; i < _lEnmeyPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].x + 1) == _lEnmeyPos_List[i].x) && (_lCan_Move_List[_iList_Count].z == _lEnmeyPos_List[i].z) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                for (int i = 0; i < _lPartnerPos_List.Count; i++)
                {
                    if (((_lCan_Move_List[_iList_Count].x + 1) == _lPartnerPos_List[i].x) && (_lCan_Move_List[_iList_Count].z == _lPartnerPos_List[i].z) && (_lMove_Weight_List[_iList_Count] > 0))
                    {
                        m_Have_Something = true;
                    }
                }
                if (m_Have_Something == false)
                {
                    m_Tmp_Pos = _lCan_Move_List[_iList_Count] + new Vector3(1, 0, 0);
                    Check_Have_Walked();
                    if (m_Have_Walked == false && _lMove_Weight_List[_iList_Count] > 0)
                    {
                        Save_Position();
                    }
                }
                m_Have_Walked = false;
                m_Have_Something = false;
                _iList_Count++;             //改變要取出來的List位置
                
            }
            //如果算完後讓方格出現
            if (_iList_Count >= _iList_Index)
            {
                _gPlayer.GetComponent<Move>().Instant();
                break;
            }
                
        }
        

    }

    /// <summary>
    /// 判斷這一格是否有走過已存在List
    /// </summary>
    public void Check_Have_Walked()
    {
        for (int i = 0; i < _iList_Index; i++)
        {
            if (m_Tmp_Pos == _lCan_Move_List[i])
            {
                m_Have_Walked = true;
                break;
            }
        }
    }

    /// <summary>
    /// 存找到的位置進去陣列
    /// </summary>
    public void Save_Position()
    {
        _iWalk_Steps = _lMove_Weight_List[_iList_Count] - 1;        //行動數-1
        _lMove_Weight_List.Insert(_iList_Index, _iWalk_Steps);      //存格子的權重進去List
        _lCan_Move_List.Insert(_iList_Index, m_Tmp_Pos);            //把計算的下一步存進List裡
        _iList_Index++; //存陣列的引數+1
       
    }




    //存取場上所有角色的位置
    public void Save_CharacterPos()
    {
        for (int i = 0; i < _gEnmey.transform.childCount; i++)
        {
            _lEnmeyPos_List.Add(_gEnmey.transform.GetChild(i).position);
        }
        for (int i = 0; i < _gPartner.transform.childCount; i++)
        {
            _lPartnerPos_List.Add(_gPartner.transform.GetChild(i).position);
        }

    }

    /// <summary>
    /// 重製List
    /// </summary>
    public void Reset_List()
    {
        _lCan_Move_List.Clear();
        _lMove_Weight_List.Clear();
        _iList_Count = 0;
        _iList_Index = 0;
        _lPartnerPos_List.Clear();
        _lEnmeyPos_List.Clear();
        Save_CharacterPos();
    }


}
