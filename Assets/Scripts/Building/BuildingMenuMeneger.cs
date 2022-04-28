using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenuMeneger : MonoBehaviour
{
    private GameObject _buildingMenu;
    private Camera _mainCamera;
    void Start()
    {
        _buildingMenu = GameObject.FindGameObjectWithTag("BuildingMenu");
        _buildingMenu.SetActive(false);
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _buildingMenu.SetActive(!_buildingMenu.activeSelf);
            if (_buildingMenu.activeSelf)
            {
                Vector3 vector3 = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                vector3.z = 0f;
                _buildingMenu.transform.position = vector3;
            }
        }
    }
}
