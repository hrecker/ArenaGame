using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionUI : MonoBehaviour 
{
    public Image item1;
    public Image item2;
    public Image item3;
    public Image selector;
    private bool moveSelectReady;

    void Awake()
    {
        /*item1.enabled = false;
        item2.enabled = false;
        item3.enabled = false;
        selector.enabled = false;     */ 
    }
	
	void Update () 
	{
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

    public void EnableItems()
    {
        item1.enabled = true;
        item2.enabled = true;
        item3.enabled = true;
        selector.enabled = true;
    }
}
