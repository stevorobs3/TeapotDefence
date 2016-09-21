using UnityEngine;
using UnityEngine.UI;

    
public class HotbarManager : MonoBehaviour
{
    
    Image[] _slots;

    Image _selectedSlot;
    SelectedItem _selectedItem;

    private Color UNSELECTED_COLOUR = Color.black;
    private Color SELECTED_COLOUR = new Color32(102, 51, 0, 255);

    public delegate void ItemSelectedHandler(SelectedItem item);
    public event ItemSelectedHandler ItemSelected;

    // Use this for initialization
    void Awake()
    {
        Transform slotsParent = transform.Find("Slots");
        _slots = new Image[slotsParent.childCount];

        for (int i = 0; i < slotsParent.childCount; i++)
        {
            _slots[i] = slotsParent.GetChild(i).GetComponent<Image>();
        }
        SelectSlot(0);

        AddButtonEvents();
    }

    private void AddButtonEvents()
    {
        for(int i = 0; i < _slots.Length; i++)
        {
            int j = i;
            _slots[i].GetComponent<Button>().onClick.AddListener(() => SelectSlot(j));
        }
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) && ItemSelected != null)
            ItemSelected(SelectedItem.SteamDamageUpgrade);
        else if (Input.GetKeyDown(KeyCode.Alpha4) && ItemSelected != null)
            ItemSelected(SelectedItem.SteamRangeUpgrade);
    }

    public SelectedItem CurrentlySelectedItem()
    {
        return _selectedItem;
    }


    public void SelectSlot(int index)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].enabled = false;
        }
        _selectedSlot = _slots[index];
        _selectedSlot.enabled = true;
        _selectedItem = (SelectedItem)index;

        if (index == 2 && ItemSelected != null)
            ItemSelected(SelectedItem.SteamDamageUpgrade);
        else if (index == 3 && ItemSelected != null)
            ItemSelected(SelectedItem.SteamRangeUpgrade);
    }
}