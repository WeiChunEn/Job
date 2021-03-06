﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Move : MonoBehaviour
{


    [SerializeField]
    private int m_Tmp_Weight;  //暫存權重的
    [SerializeField]
    private int m_ShortRoad_Index; //來存最短路徑的Index


    public List<Vector3> _lShort_Road = new List<Vector3>(); //最短路徑的List
    public Path path;        //路徑的Class
    public GameObject _gMove_Grid; //生成移動的格子

    public GameObject _gGameManager; //遊戲管理器
    public GameObject _gGrid_Group; //放格子的位置
    public GameObject _gMove_Pos;  //移動到位置



    private int Short_Road_Count;       //最短路徑的格數
    private bool _bMove_Finish;         //是否移動完成
    public bool _bCan_Move;             //是否可以開始移動
    public bool _bIs_Moving;            //是否正在移動
    public Button _bMove_Btn;       //移動的按鈕
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




        if (_bMove_Finish == true)
        {
            Character_Move();
        }
    }



    /// <summary>
    /// 計算移動的最短路徑
    /// </summary>
    public void Cal_Road()
    {
        _bCan_Move = false;

        Vector3 NowPos = _gMove_Pos.transform.position; //儲存點擊的位置
        for (int i = 0; i < path._iMove_List_Count; i++)
        {

            //判斷點擊的位置是否有在可以移動的陣列裡面
            if ((NowPos.x == path._lCan_Move_List[i].x) && (NowPos.z == path._lCan_Move_List[i].z)  )
            {

                m_Tmp_Weight = path._lMove_Weight_List[i];
                _lShort_Road.Insert(m_ShortRoad_Index, path._lCan_Move_List[i]); //把這格存到最短路徑裡面
                m_ShortRoad_Index++; //存取用的Index+1
                _bCan_Move = true;
            }


        }
        while ((NowPos.x != path._lCan_Move_List[0].x || NowPos.z != path._lCan_Move_List[0].z) && _bCan_Move == true)
        {
            for (int i = 0; i < path._iMove_List_Count; i++)
            {
                //向上比
                if ((NowPos.z + 1 == path._lCan_Move_List[i].z) && (NowPos.x == path._lCan_Move_List[i].x))
                {
                    Cmp_Weight(i);
                }

                //向下比
                if ((NowPos.z - 1 == path._lCan_Move_List[i].z) && (NowPos.x == path._lCan_Move_List[i].x))
                {
                    Cmp_Weight(i);
                }

                //向左比
                if ((NowPos.x - 1 == path._lCan_Move_List[i].x) && (NowPos.z == path._lCan_Move_List[i].z))
                {
                    Cmp_Weight(i);
                }

                //向右比
                if ((NowPos.x + 1 == path._lCan_Move_List[i].x) && (NowPos.z == path._lCan_Move_List[i].z))
                {
                    Cmp_Weight(i);
                }
            }
            NowPos = _lShort_Road[m_ShortRoad_Index];
            m_ShortRoad_Index++;
        }
        Short_Road_Count = _lShort_Road.Count - 1;

        _bMove_Finish = true;
        for (int i = 0; i < _gGrid_Group.transform.childCount; i++)
        {

            Destroy(_gGrid_Group.transform.GetChild(i).gameObject);
        }

    }

    /// <summary>
    /// 比較四個方向的權重值
    /// </summary>
    /// <param name="Road_Weight">格子的權重值</param>
    public void Cmp_Weight(int Road_Weight)
    {
        if (path._lMove_Weight_List[Road_Weight] > m_Tmp_Weight) //如果下一步的權重大於現在位置的權重
        {
            m_Tmp_Weight = path._lMove_Weight_List[Road_Weight]; //交換
            _lShort_Road.Insert(m_ShortRoad_Index, path._lCan_Move_List[Road_Weight]); //把權重存進最短路徑
        }
    }


    /// <summary>
    /// 角色移動
    /// </summary>
    public void Character_Move()
    {


        Set_Rotate();
        _bIs_Moving = true;
        transform.position = Vector3.MoveTowards(transform.position, _lShort_Road[Short_Road_Count], 2.5f * Time.deltaTime);
        if (transform.position == _lShort_Road[Short_Road_Count])
        {

            Short_Road_Count--;
        }
        if (Short_Road_Count < 0)
        {
            _bIs_Moving = false;
            _gGameManager.GetComponent<GameManager>().In_And_Out();
            if (gameObject.tag == "A")
            {
                _gGameManager.GetComponent<GameManager>()._gWhole_UI.SetActive(true);
            }
            else if(gameObject.tag=="B")
            {
                _gGameManager.GetComponent<GameManager>()._gWhole_UI.SetActive(true);
            }
            //_gGameManager.GetComponent<GameManager>()._gMove_UI.SetActive(true);
            _bMove_Finish = false;
            OnTransfer_Area();
            Reset_Data();
            path.Reset_List();
            path.Save_CharacterPos();
            

          

        }


    }
    
    /// <summary>
    /// 轉向
    /// </summary>
    public void Set_Rotate()
    {
        
        Vector3 distance = _lShort_Road[Short_Road_Count]-transform.position;
        distance.Normalize();
        if(gameObject.tag=="A")
        {
            //右
            if (distance.x > 0.1f)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
            }


            //左
            if (distance.x < -0.1f)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }

            //上
            if (distance.z > 0.9f)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            //下
            if (distance.z < -0.9f)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if(gameObject.tag=="B")
        {  //右
            if (distance.x > 0.1f)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }


            //左
            if (distance.x < -0.1f)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
            }

            //上
            if (distance.z > 0.9f)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            //下
            if (distance.z < -0.9f)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
       
    }
    /// <summary>
    /// 重製陣列
    /// </summary>
    public void Reset_Data()
    {
        _lShort_Road.Clear();
        m_ShortRoad_Index = 0;
        m_Tmp_Weight = 0;
        //算完格子開始移動時刪掉格子
        for (int i = 0; i < _gGrid_Group.transform.childCount; i++)
        {

            Destroy(_gGrid_Group.transform.GetChild(i).gameObject);
        }

    }

    //生成移動的格子
    public void Move_Grid_Instant()
    {
        for (int i = 1; i < path._lCan_Move_List.Count; i++)
        {
           
            for (int j = 0; j < path._gPlane.transform.childCount; j++)
            {
                if ((path._gPlane.transform.GetChild(j).transform.position.x == path._lCan_Move_List[i].x)&& (path._gPlane.transform.GetChild(j).transform.position.z == path._lCan_Move_List[i].z))
                {
                   
                    Instantiate(_gMove_Grid, new Vector3(path._lCan_Move_List[i].x, 0.01f, path._lCan_Move_List[i].z), _gMove_Grid.transform.rotation, _gGrid_Group.transform);
                }

            }

        }
      
    }


    /// <summary>
    /// 在轉職區的話
    /// </summary>
    private void OnTransfer_Area()
    {
        for(int i = 0; i<path._lTransfer_A.Count;i++)
        {
            if(tag=="A"&&gameObject.transform.position.x== path._lTransfer_A[i].x&& gameObject.transform.position.z == path._lTransfer_A[i].z&&gameObject.GetComponent<Character>().Chess.Job=="Minion")
            {
                _gGameManager.GetComponent<GameManager>()._gTranfer_UI.SetActive(true);
            }
        }
        for (int i = 0; i < path._lTransfer_B.Count; i++)
        {
            if (tag == "B" && gameObject.transform.position.x == path._lTransfer_B[i].x && gameObject.transform.position.z == path._lTransfer_B[i].z&&gameObject.GetComponent<Character>().Chess.Job == "Minion")
            {
                _gGameManager.GetComponent<GameManager>()._gTranfer_UI.SetActive(true);
            }
        }

    }
    


}
