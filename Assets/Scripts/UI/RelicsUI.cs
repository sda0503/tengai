using UnityEngine;
using UnityEngine.UI;

public class RelicsUI : MonoBehaviour
{
    public Button button; // 누르면 유물 인포나오게
    public Image icon;
    public GameObject relicsDescriptionPopup;
    private RelcisSlot curSlot;

    public int index;

    public void Set( RelcisSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.relics.icon;
    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);

    }

    public void OnButtonClick()
    {
        // 유물 정보 팝업이 뜨게 할거임.
    }
}
