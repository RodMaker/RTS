using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCohesion : AgentBehaviour
{
    public float neighbourDistance = 15.0f;
    public List<GameObject> targets;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        int count = 0;

        foreach(GameObject other in targets)
        {
            if (other != null)
            {
                float d = (transform.position - other.transform.position).magnitude;
                if ((d > 0) && (d < neighbourDistance))
                {
                    steering.linear += other.transform.position;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            steering.linear /= count;
            steering.linear = steering.linear - transform.position;
        }

        return steering;
    }
}
