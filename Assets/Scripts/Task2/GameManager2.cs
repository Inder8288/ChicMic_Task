using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace ChicMicTask2
{
    public class GameManager2 : MonoBehaviour
    {
        // Start is called before the first frame update
        public static GameManager2 Instance;
        public GameObject objectPointerPrefab;
        public Transform pointersParent;
        public GameObject player;
        public Camera mainCam;
        public float pointerOffset = 0.2f;

        public List<GameObject> objectList = new List<GameObject>();
        [HideInInspector]
        public List<ObjectPointer> objectPointers = new List<ObjectPointer>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        void Start()
        {
          
                for (int i = 0; i < objectList.Count; i++)
                {
                
                    GameObject newPointer = (GameObject)Instantiate(objectPointerPrefab);
                    objectPointers.Add(newPointer.GetComponent<ObjectPointer>());
                    newPointer.transform.SetParent(pointersParent);
                    newPointer.GetComponent<ObjectPointer>().objectToPoint= objectList[i];
                }
               
            
           
        }


        public void GoToTask1()
        {
            SceneManager.LoadScene(0);
            
        }



    }
}
