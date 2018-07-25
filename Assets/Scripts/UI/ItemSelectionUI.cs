using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionUI : MonoBehaviour 
{
    public Text selectItemText;
    public Image item1;
    public Image item2;
    public Image item3;
    public Image selector;
    public Item[] itemSprites;
    private bool moveSelectReady;
    private bool itemsEnabled;

    void Awake()
    {
        selectItemText.enabled = false;
        item1.enabled = false;
        item2.enabled = false;
        item3.enabled = false;
        selector.enabled = false;
        itemsEnabled = false;
        SceneMessenger.Instance.AddListener(Message.LEVEL_COMPLETED, new SceneMessenger.LevelCallback(DisplayItemOptions));
    }
	
	void Update () 
	{
        if (!itemsEnabled)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (moveSelectReady)
        {
            if (horizontal < -0.5f && selector.rectTransform.localPosition.x >= 0)
            {
                selector.rectTransform.localPosition += (150 * Vector3.left);
                moveSelectReady = false;
            }
            else if (horizontal > 0.5f && selector.rectTransform.localPosition.x <= 0)
            {
                selector.rectTransform.localPosition += (150 * Vector3.right);
                moveSelectReady = false;
            }
        }
        else
        {
            if (Mathf.Abs(horizontal) < 0.5f)
            {
                moveSelectReady = true;
            }
        }
    }

    public void DisplayItemOptions(Level level, bool isLastLevel)
    {
        if (!isLastLevel)
        {
            //ItemType[] items = ItemRandomizer.RandomizeItems(level.LevelNumber);
            item1.sprite = itemSprites[0].sprite;
            item2.sprite = itemSprites[1].sprite;
            item3.sprite = itemSprites[2].sprite;
            EnableItems();
        }
    }

    public void EnableItems()
    {
        selectItemText.enabled = true;
        item1.enabled = true;
        item2.enabled = true;
        item3.enabled = true;
        selector.enabled = true;
        itemsEnabled = true;
    }
}
