using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField] private float interactRange;

    private void Start()
    {
        
    }

    private void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, interactRange))
        {
            string tag = hitInfo.transform.tag;

            if (Input.GetMouseButtonDown(0))
            {
                if (tag == "Computer")
                {
                    Computer c = hitInfo.transform.GetComponent<Computer>();
                    if (c)
                    {
                        c.Boot();
                    }
                    else
                    {
                        Debug.LogError("Tried booting computer, but couldn't get a valid computer component.");
                    }
                }
            }
        }
    }

}
