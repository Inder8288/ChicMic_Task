using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChicMicTask2
{
    public class ObjectPointer : MonoBehaviour
    {

        public GameObject objectToPoint;
        private Vector3 worldToViewportTarget;
        private Image imgComponent;
        private bool isObjectOnScreen = false;
    



    





        private void Start()
        {
            imgComponent = GetComponent<Image>();

            worldToViewportTarget = new Vector3();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateLocAndRot();

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

        void UpdateLocAndRot()
        {

            //calculate vector from player position to target position
            worldToViewportTarget = GameManager2.Instance.mainCam.WorldToScreenPoint(objectToPoint.transform.position);
            Transform playerPos = GameManager2.Instance.player.transform;
            //offset-----------
            //----set pointer pos
            /*            Vector3 rawPointerPos = GameManager2.Instance.mainCam.WorldToScreenPoint(playerPos.position);
            */
            //We can hide pointer if enmy is visible by camera
            float xClamped, yClamped=0;
            Vector3 worldToScreenTarget = GameManager2.Instance.mainCam.WorldToScreenPoint(objectToPoint.transform.position);

            if (worldToViewportTarget.x < 0  || worldToViewportTarget.x > Screen.width || worldToViewportTarget.y > Screen.height)
            {

                // ActiveDeactivatePointerOnVisible(false);
                imgComponent.rectTransform.localScale = new Vector3(1, imgComponent.rectTransform.localScale.y, imgComponent.rectTransform.localScale.z);
                xClamped = Mathf.Clamp(worldToScreenTarget.x, imgComponent.rectTransform.sizeDelta.x / 2, Screen.width - imgComponent.rectTransform.sizeDelta.x);

                yClamped = Mathf.Clamp(worldToScreenTarget.y, 0, Screen.height);
                isObjectOnScreen = false;
            }
            else
            {
                imgComponent.rectTransform.localScale = new Vector3(imgComponent.rectTransform.localScale.x, imgComponent.rectTransform.localScale.y, imgComponent.rectTransform.localScale.z);

                //  ActiveDeactivatePointerOnVisible(true);
                yClamped = Mathf.Clamp(worldToScreenTarget.y, 0, Screen.height);
                xClamped = Mathf.Clamp(worldToScreenTarget.x, imgComponent.rectTransform.sizeDelta.x / 2, Screen.width - imgComponent.rectTransform.sizeDelta.x);

                isObjectOnScreen = true;

            }

            if (worldToScreenTarget.z < 0)
            {
                if (worldToViewportTarget.z < 0)
                {
                    imgComponent.rectTransform.localScale = new Vector3(-1, imgComponent.rectTransform.localScale.y, imgComponent.rectTransform.localScale.z);
                    xClamped = Mathf.Clamp(-worldToScreenTarget.x, imgComponent.rectTransform.sizeDelta.x / 2, Screen.width - imgComponent.rectTransform.sizeDelta.x);

                    yClamped = worldToScreenTarget.z+2*imgComponent.rectTransform.sizeDelta.y;
                }
                
                isObjectOnScreen = false;
            }






            Vector3 rawPointerPos = new Vector3(xClamped, yClamped, 0);


            //find angle by tan inverse y/x
            float angle = Mathf.Atan2(worldToScreenTarget.y - GameManager2.Instance.mainCam.WorldToScreenPoint(playerPos.position).y, worldToScreenTarget.x - GameManager2.Instance.mainCam.WorldToScreenPoint(playerPos.position).x) * Mathf.Rad2Deg;

            if (!isObjectOnScreen)
            {
                transform.position = rawPointerPos;
                transform.eulerAngles = new Vector3(0, 0, angle);
                imgComponent.color = Color.yellow;
            }
            else
            {
                ChangeSpriteAndLocOnScreen(worldToScreenTarget);
            }


        }

        void ChangeSpriteAndLocOnScreen(Vector3 pos)
        {
            imgComponent.color = Color.red;
            transform.position = new Vector3(pos.x,pos.y+GameManager2.Instance.pointerOffset,pos.z);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -90f);
        }


        void CheckIfObjectIsOnScreen()
        {
           
        }

        public void HandleIndicatorOnAttack(int _enemyInd)
        {

            if (GameManager2.Instance.objectPointers[_enemyInd] == this)
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

   
    }
}
