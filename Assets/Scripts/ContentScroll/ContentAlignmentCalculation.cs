using Scroll;
using UnityEngine;

namespace ContentScroll
{
    public class ContentAlignmentCalculation:MonoBehaviour
    {
        [SerializeField] private InfinityScroll _infinityScroll;
        
        public void CenterContentOnMiddleItem(RectTransform scrollContent)
        {
            float centerY = CalculateCenterYForMiddleItem();
            Vector2 currentContentPosition = scrollContent.anchoredPosition;

            currentContentPosition.y = centerY;

            scrollContent.anchoredPosition = currentContentPosition;
        }

        private float CalculateCenterYForMiddleItem()
        {
            int itemCount = _infinityScroll.ItemList.Length;
            if (itemCount == 0)
                return 0;

            int middleIndex = itemCount / 2;
            RectTransform middleItem = _infinityScroll.ItemList[middleIndex];

            float topPadding = _infinityScroll.VerticalLayoutGroup.padding.top;
            float spacing = _infinityScroll.VerticalLayoutGroup.spacing;

            float centerY = middleItem.anchoredPosition.y;
            centerY += middleItem.rect.height * 0.5f + topPadding;

            if (middleIndex > 0)
            {
                centerY += spacing * 0.5f;
            }

            return centerY;
        }
    }
}