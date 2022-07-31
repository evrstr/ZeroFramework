using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace ZeroFramework.Entity
{
    public class EnemyInfo
    {
        public int monsterid;   //怪物id
        public string name; //怪物名字
        public Vector2 spawnPoint;  //出生点
    }

    public class EnemyMgr : SingletonBase<EnemyMgr>
    {
        List<EnemyInfo> allEnemy = new List<EnemyInfo>();
        //关卡，敌人列表。
        Dictionary<string, List<EnemyInfo>> enemyDict = new Dictionary<string, List<EnemyInfo>>();


        public void SpawnEnemy<T>(int id, Vector3 pawnPoint, UnityAction<T> callback = null) where T : UnityEngine.Object
        {
            GameObject enemy = PoolMgr.Instance.PopObj("LuckyOrc_0");
            // if (pawnPoint != Vector3.zero)
            // {
            //     enemy.transform.position = pawnPoint;
            //     (enemy.GetComponent<T>() as Monster).MonsterParm.moveSpeed *= Random.Range(1f, 1.05f);
            //     (enemy.GetComponent<T>() as Monster).FsmParm.ReactCenterOffSet = new Vector2(2, 0);
            //     (enemy.GetComponent<T>() as Monster).FsmParm.ReactSize = new Vector2(4, 1.2f);
            // }
            callback?.Invoke(enemy as T);

        }

        protected override void OnInit()
        {

        }
    }

}
