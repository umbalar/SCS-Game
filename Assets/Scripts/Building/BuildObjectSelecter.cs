using UnityEngine;
using UnityEngine.UI;

public class BuildObjectSelecter : MonoBehaviour
{
    private Builder _builder;
    public Object _objectToBuild;
    public int _counter;
    public GameObject _buildMarker;
    [SerializeField] private GameObject _buildMarkerPrefub;
    private Sprite _sprite;
    private GameObject _buildingMenu;

    void Start()
    {
        _builder = GameObject.FindGameObjectWithTag("Grid").GetComponent<Builder>();
        _sprite = GetComponent<Image>().sprite;
        _buildingMenu = transform.parent.gameObject;
    }
    public void Select()
    {
        if (_counter > 0)
        {
            _builder._activeButtonSqcript = this;
            _buildMarker = Instantiate(_buildMarkerPrefub);
            _buildMarker.GetComponent<SpriteRenderer>().sprite = _sprite;
            _buildingMenu.SetActive(false);
        }
    }
}
