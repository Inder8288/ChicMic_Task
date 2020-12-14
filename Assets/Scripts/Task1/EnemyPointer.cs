using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChicMicTask1
{
    public class EnemyPointer : MonoBehaviour
    {

        public GameObject enemy;


        private Image imgComponent;
        private bool isEnemyOnScreen = false;

        private void OnEnable()
        {
            if (EventManager.OnEnemyShoot != null)
            {
                EventManager.OnEnemyShoot.AddListener(HandleIndicatorOnAttack);
            }



            imgComponent = GetComponent<Image>();
        }



        void ActiveDeactivatePointerOnVisible(bool onScreen)
        {
            if (onScreen)
            {
                imgComponent.color = new Color(1, 1, 1, 0);
            }
            else
            {
                imgComponent.color = new Color(1, 1, 1, 1);

            }
        }





        private void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            UpdateLocAndRot();

        }

        void UpdateLocAndRot()
        {
            
            //calculate vector from player position to target position
            Transform playerPos = GameManager.Instance.player.transform;

            //We can hide pointer if enmy is visible by camera
            float xClamped, yClamped = 0;
            Vector3 worldToScreenTarget = GameManager.Instance.mainCam.WorldToScreenPoint(enemy.transform.position);

            #region Get and Set Pointer Position to screen
            //Get World To screen position and clamp it to screen size
            if (worldToScreenTarget.x < 0 || worldToScreenTarget.x > Screen.width || worldToScreenTarget.y > Screen.height)
            {

                // ActiveDeactivatePointerOnVisible(false);
                imgComponent.rectTransform.localScale = new Vector3(2, 2, 2);
                xClamped = Mathf.Clamp(worldToScreenTarget.x, imgComponent.rectTransform.sizeDelta.x / 2, Screen.width - imgComponent.rectTransform.sizeDelta.x);

                yClamped = Mathf.Clamp(worldToScreenTarget.y, 0, Screen.height);
                isEnemyOnScreen = false;
            }
            else
            {
                imgComponent.rectTransform.localScale = new Vector3(2, 2, 2);

                //  ActiveDeactivatePointerOnVisible(true);
                yClamped = Mathf.Clamp(worldToScreenTarget.y, 0, Screen.height);
                xClamped = Mathf.Clamp(worldToScreenTarget.x, imgComponent.rectTransform.sizeDelta.x / 2, Screen.width - imgComponent.rectTransform.sizeDelta.x);

                isEnemyOnScreen = true;

            }

            if (worldToScreenTarget.z < 0)
            {
                if (worldToScreenTarget.z < 0)
                {
                    imgComponent.rectTransform.localScale = new Vector3(-2, 2, 2);
                    xClamped = Mathf.Clamp(-worldToScreenTarget.x, imgComponent.rectTransform.sizeDelta.x / 2, Screen.width - imgComponent.rectTransform.sizeDelta.x);

                    yClamped = worldToScreenTarget.z + 2 * imgComponent.rectTransform.sizeDelta.y;
                }

                isEnemyOnScreen = false;
            } 
            #endregion






            Vector3 rawPointerPos = new Vector3(xClamped, yClamped, 0);
            
            //find angle by tan inverse y/x
            float angle = Mathf.Atan2(worldToScreenTarget.y - GameManager.Instance.mainCam.WorldToScreenPoint(playerPos.position).y, worldToScreenTarget.x - GameManager.Instance.mainCam.WorldToScreenPoint(playerPos.position).x) * Mathf.Rad2Deg;

            //set final position of pointer
            transform.position = rawPointerPos;
            transform.eulerAngles = new Vector3(0, 0, angle);



        }


        public void HandleIndicatorOnAttack(int _enemyInd)
        {

            if (GameManager.Instance.enemyPointers[_enemyInd] == this)
            {
                StartCoroutine(BlinkAttackColor());
            }
        }

        IEnumerator BlinkAttackColor()
        {
            float timer = 5f;
            Image img = GetComponent<Image>();
            img.color = new Color(1, 1, 1, 1);
            while (timer > 0)
            {
                yield return new WaitForSeconds(0.3f);
                if (Time.frameCount % 2 == 0)
                {
                    img.color = Color.red;
                }
                else
                {
                    img.color = Color.white;
                }
                timer--;
                yield return null;
            }


            img.color = new Color(1, 1, 1, 0);

        }

        private void OnDisable()
        {
            if (EventManager.OnEnemyShoot != null)
            {
                EventManager.OnEnemyShoot.RemoveListener(HandleIndicatorOnAttack);
            }

        }
    }
}
