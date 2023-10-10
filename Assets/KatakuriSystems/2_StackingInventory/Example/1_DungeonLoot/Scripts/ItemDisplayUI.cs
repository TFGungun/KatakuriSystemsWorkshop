using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Katakuri.SystemsWorkshop.StackingInventory1.Example1
{
    public class ItemDisplayUI : MonoBehaviour
    {
        [SerializeField] private Image _itemRenderer;
        [SerializeField] private TMP_Text _itemCountContainer;
        [SerializeField] private Button _clickButton;

        public void SetItemDisplay(Sprite itemSprite, int itemCount, System.Action onClickItem)
        {
            _itemRenderer.sprite = itemSprite;
            _itemCountContainer.text = itemCount.ToString();

            _clickButton.onClick.RemoveAllListeners();
            _clickButton.onClick.AddListener(() => onClickItem());
        }
    }

}
