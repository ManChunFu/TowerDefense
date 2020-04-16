using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public abstract class ScriptablePoolBase : MonoBehaviour, IPool
{
    [SerializeField] GameObjectScriptablePool ScriptablePool;
    //blic GameObjectScriptablePool ScriptablePool { get ; set; }

    public void KillPool(){}

    
}
