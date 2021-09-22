using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeTesterObject : MonoBehaviour
{
    private ConeTester coneTester = null;

    private void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        if(coneTester == null)
        {
            coneTester = GameObject.FindObjectOfType<ConeTester>();
            if (coneTester == null)
                return;
        }

        Gizmos.color = coneTester.TestCone(transform.position) ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
