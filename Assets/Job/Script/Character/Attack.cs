﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject _gAttack_Grid; //生成的戰鬥格子
    public GameObject _gGrid_Group; //放格子的位置
    public Path path;        //路徑的Class

    public GameObject _gEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack_Grid_Instant()
    {
        for (int i = 1; i < path._lCan_Attack_List.Count; i++)
        {

            for (int j = 0; j < path._gPlane.transform.childCount; j++)
            {
                if ((path._gPlane.transform.GetChild(j).transform.position.x == path._lCan_Attack_List[i].x) && (path._gPlane.transform.GetChild(j).transform.position.z == path._lCan_Attack_List[i].z))
                {

                    Instantiate(_gAttack_Grid, new Vector3(path._lCan_Attack_List[i].x, 0.01f, path._lCan_Attack_List[i].z), _gAttack_Grid.transform.rotation, _gGrid_Group.transform);
                }

            }

        }
        //for (int i = 0; i < path._lCan_Attack_List.Count; i++)
        //{

        //    Instantiate(_gAttack_Grid, new Vector3(path._lCan_Attack_List[i].x, 0.01f, path._lCan_Attack_List[i].z), _gAttack_Grid.transform.rotation, _gGrid_Group.transform);
        //}
    }

    public void Destory_AttackGrid()
    {
        for (int i = 0; i < _gGrid_Group.transform.childCount; i++)
        {

            Destroy(_gGrid_Group.transform.GetChild(i).gameObject);
        }
    }

    public void Attack_Enmey(GameObject Enmey)
    {
        Debug.Log(Enmey.GetComponent<Character>().Chess.HP);
        Enmey.GetComponent<Character>().Chess.HP -= gameObject.GetComponent<Character>().Chess.Attack;
        Instantiate(_gEffect, Enmey.transform.position, _gEffect.transform.rotation);
    }
}