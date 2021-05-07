using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCollision : MonoBehaviour
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

        RectTransform firstChild = scrollViewContent.GetChild(0).GetComponent<RectTransform>();
        float newPosX = firstChild.anchoredPosition.x - collision.GetComponent<RectTransform>().sizeDelta.x - spacing;

        Vector2 newAnchoredPosition = collision.GetComponent<RectTransform>().anchoredPosition;
        newAnchoredPosition.x = newPosX;
        collision.GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
        collision.transform.SetAsFirstSibling();
    }
}
