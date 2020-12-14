using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChicMicTask1
{
    public abstract class Character : MonoBehaviour
    {
        // Start is called before the first frame update
        public float moveSpeed = 5f;
        public GameObject projectilePrefab;
        public float projectileSpeed = 3f;
        protected GameObject projInst;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        protected abstract void Move();
        protected virtual void Shoot()
        {
            projInst = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

    }
}
