using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ScrollerItems : MonoBehaviour
    {
        [SerializeField] private ScrollRect m_scroller;
        [SerializeField] private RectTransform m_scrollerContent;
        [SerializeField] private GameObject m_uiItem;

        [SerializeField] private List<Item> m_data;

        private void Awake()
        {
            SetUpItems(m_data);
        }

        public void SetUpItems(List<Item> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                var itemInstantiated = Instantiate(m_uiItem,
                    m_scrollerContent, true);
                itemInstantiated.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * 120);
                itemInstantiated.GetComponent<OnItemClicked>().data = items[i];
            }

            m_scrollerContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0,items.Count * 120);
        }

    }
}