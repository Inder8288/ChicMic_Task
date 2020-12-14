using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChicMicTask1
{

    public class EnemyCharacter : Character
    {
        public EnemyPointer pointer;
        private bool isDead = false;
        public int ind;


        // Start is called before the first frame update
        void Start()
        {
            EventManager.RegisterEnemyPointerIdx.Invoke(ind);
            GetIndexFromList();
            StartCoroutine(ShootProjectileRandomly());

        }


        // Update is called once per frame
        void Update()
        {
            Vector3 resultant = GameManager.Instance.player.transform.position - transform.position;
        }

        protected override void Move()
        {

        }

        protected override void Shoot()
        {
            base.Shoot();
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), projInst.GetComponent<SphereCollider>());
            projInst.GetComponent<Rigidbody>().AddForce((GameManager.Instance.player.transform.position - transform.position).normalized * projectileSpeed);
            Destroy(projInst.gameObject, 4f);
        }

        IEnumerator ShootProjectileRandomly()
        {
            //rand=random seconds
            //active pointer= active the pointer before actually run shoot
            float rand;
            float waitTimeToactivePointer = 5f;
            //Shoot until this enemy isnt dead
            //Currently die logic isnt required, so shoot repeatedly after random no of seconds
            while (!isDead)
            {
                rand = UnityEngine.Random.Range(5f, 10f);
                yield return new WaitForSeconds(rand - waitTimeToactivePointer);
                EventManager.OnEnemyShoot.Invoke(ind);
                yield return new WaitForSeconds(2f);
                Shoot();
            }
        }




        void GetIndexFromList()
        {
            for (int i = 0; i < GameManager.Instance.enemyList.Count; i++)
            {
                if (GameManager.Instance.enemyList[i] == this)
                {
                    ind = i;
                }
            }
        }






    }
}

