using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuFiller : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buttons;
    public List<int> _counters;

    void Start()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            GameObject button = Instantiate(_buttons[i], transform);
            button.GetComponent<BuildObjectSelecter>()._counter = _counters[i];
            button.GetComponentInChildren<Text>().text = _counters[i].ToString();
        }
    }
}
