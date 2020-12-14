using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace ChicMicTask1
{
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static GameManager Instance;
        public GameObject enemyPointer;
        public Transform pointersParent;
        public GameObject player;
        public Camera mainCam;
        public float pointerOffset = 0.2f;

        public List<EnemyCharacter> enemyList = new List<EnemyCharacter>();
        [HideInInspector]
        public List<EnemyPointer> enemyPointers = new List<EnemyPointer>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        void Start()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                GameObject newPointer = Instantiate(enemyPointer);
                enemyPointers.Add(newPointer.GetComponent<EnemyPointer>());
                enemyPointers[i].transform.SetParent(pointersParent);
                enemyPointers[i].enemy = enemyList[i].gameObject;

                enemyList[i].pointer = newPointer.GetComponent<EnemyPointer>();
            }
        }


        public void GoToTask2()
        {
            Debug.Log("Go to task 2");
            SceneManager.LoadScene(1);
        }

    }

   
}
