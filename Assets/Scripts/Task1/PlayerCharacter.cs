using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChicMicTask1
{
    public class PlayerCharacter : Character
    {


        public float turnSpeed = 8f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            Move();
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        protected override void Move()
        {
            float h, v;
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            transform.Translate(h * moveSpeed, 0, v * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + turnSpeed * Input.GetAxis("Mouse X"), 0), 0.8f);
        }

        protected override void Shoot()
        {

            base.Shoot();
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), projInst.GetComponent<SphereCollider>());
            projInst.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * projectileSpeed);
            Destroy(projInst.gameObject, 4f);

        }
    }
}