using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private Image sprite;
    [SerializeField]
    private Button button;

    public void Initialize(string inventoryID, ItemObject item, Action<String> removeItemAction)
    {
        sprite.sprite = item.Icon;
        transform.localScale = Vector3.one;
        button.onClick.AddListener(() => removeItemAction.Invoke(inventoryID));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
