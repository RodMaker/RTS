﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekScript : MonoBehaviour
{
    BaseBehaviour bb;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        bb = gameObject.GetComponent<BaseBehaviour>();
        target = bb.target;

        
        if(bb.seekScript == null)
        {
            bb.seekScript = gameObject.AddComponent<Seek>();
            bb.seekScript.target = target;
            bb.seekScript.weight = 0.7f;
            bb.seekScript.enabled = true;

            
            bb.boidcoh = gameObject.AddComponent<BoidCohesion>();
            bb.boidcoh.targets = bb.target.GetComponent<SquadParent>().children;
            bb.boidcoh.weight = 0.4f;
            bb.boidcoh.enabled = true;

            bb.boidsep = gameObject.AddComponent<BoidSeparation>();
            bb.boidsep.targets = bb.target.GetComponent<SquadParent>().children;
            bb.boidsep.weight = 70.0f;
            bb.boidsep.enabled = true;
            
        }
        
    }

    
    private void OnDestroy()
    {
        DestroyImmediate(bb.seekScript);
    }
    

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.Label(transform.position + Vector3.up * 3, "Seek");
    }
}
