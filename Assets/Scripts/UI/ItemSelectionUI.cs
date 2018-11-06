using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionUI : MonoBehaviour, IPauseable
{
    public Text selectItemText;
    public Image item1;
    public Image item2;
    public Image item3;
    public Image selector;
    private bool itemsEnabled;
    private int selectedIndex;
    private Item[] currentItems;

    private bool paused = false;

    void Awake()
    {
        SetItemsEnabled(false);
        SceneMessenger.Instance.AddListener(Message.LEVEL_COMPLETED, new SceneMessenger.LevelCallback(GenerateItemOptions));
        SceneMessenger.Instance.AddListener(Message.LEVEL_STARTED, new SceneMessenger.VoidCallback(HideItemOptions));
    }
	
	void Update () 
	{
        if (!itemsEnabled || paused)
        {
            return;
        }
        
        // Moving selection
        if (Input.GetButtonDown("BumperLeft") && selectedIndex > 0)
        {
            selector.rectTransform.localPosition += (150 * Vector3.left);
            selectedIndex--;
        }
        else if (Input.GetButtonDown("BumperRight") && selectedIndex < 2)
        {
            selector.rectTransform.localPosition += (150 * Vector3.right);
            selectedIndex++;
        }

        // Selecting item
        if (Input.GetButtonDown("Fire1"))
        {
            ItemExecutor.ExecuteItem(currentItems[selectedIndex]);
            HideItemOptions();
            SceneMessenger.Instance.Invoke(Message.READY_TO_START_LEVEL, null);
        }
    }

    public void GenerateItemOptions(Level level, bool isLastLevel)
    {
        if (!isLastLevel)
        {
            currentItems = ItemLoader.GetVictoryItems(level.LevelNumber);
            item1.sprite = currentItems[0].sprite;
            item2.sprite = currentItems[1].sprite;
            item3.sprite = currentItems[2].sprite;
            SetItemsEnabled(true);
            selectedIndex = 1;
            selector.rectTransform.localPosition = new Vector3(0, selector.rectTransform.localPosition.y, selector.rectTransform.localPosition.z);
        }
    }

    public void HideItemOptions()
    {
        SetItemsEnabled(false);
    }

    public void SetItemsEnabled(bool enabled)
    {
        selectItemText.enabled = enabled;
        item1.enabled = enabled;
        item2.enabled = enabled;
        item3.enabled = enabled;
        selector.enabled = enabled;
        itemsEnabled = enabled;
    }

    public void OnPause()
    {
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }
}
