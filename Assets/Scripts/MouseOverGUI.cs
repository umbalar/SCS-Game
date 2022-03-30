using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
    }

    private void OnMouseOver()
    {
        Debug.Log("OnMouseOver");
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }
}
