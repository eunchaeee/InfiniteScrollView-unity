using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCollision : MonoBehaviour
{
    float spacing;
    Transform scrollViewContent;

    // Start is called before the first frame update
    void Start()
    {
        scrollViewContent = ScrollMove.instance.scrollViewContent;
        spacing = ScrollMove.instance.spacing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time < 0.5f) return;

        int lastChildIndex = scrollViewContent.childCount - 1;
        RectTransform lastChild = scrollViewContent.GetChild(lastChildIndex).GetComponent<RectTransform>();
        float newPosX = lastChild.anchoredPosition.x + collision.GetComponent<RectTransform>().sizeDelta.x + spacing;

        Vector2 newAnchoredPosition = collision.GetComponent<RectTransform>().anchoredPosition;
        newAnchoredPosition.x = newPosX;
        collision.GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
        collision.transform.SetAsLastSibling();
    }
}
