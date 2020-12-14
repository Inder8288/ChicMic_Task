using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
   
    public static IntEvent RegisterEnemyPointerIdx = new IntEvent();
    public static IntEvent OnEnemyShoot=new IntEvent();
    public static BoolEvent OnEnemyObjectRender = new BoolEvent();
}
