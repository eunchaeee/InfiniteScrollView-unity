using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollMove : MonoBehaviour
{
    public RectTransform scrollViewContent;
    public float speed = 0.8f;

    public float spacing;
    public float distance;
    float currentTime;

    public static ScrollMove instance;
    Coroutine movecoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        distance = scrollViewContent.GetChild(1).GetComponent<RectTransform>().anchoredPosition.x - scrollViewContent.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x;
        spacing = distance - scrollViewContent.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;

        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move Right
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 newPos = scrollViewContent.anchoredPosition + new Vector2(distance, 0);
            if(movecoroutine != null)
            {
                StopCoroutine(movecoroutine);
            }
            movecoroutine = StartCoroutine(moveContent(newPos));            
        }

        // Move Left
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 newPos = scrollViewContent.anchoredPosition - new Vector2(distance, 0);
            if (movecoroutine != null)
            {
                StopCoroutine(movecoroutine);
            }
            movecoroutine = StartCoroutine(moveContent(newPos));

        }
    }

    IEnumerator moveContent(Vector2 newPos)
    {
        while (true)
        {
            currentTime += Time.deltaTime;
            scrollViewContent.anchoredPosition = Vector2.Lerp(scrollViewContent.anchoredPosition, newPos, currentTime * 0.8f);
            yield return null;

            if(Vector2.Distance(scrollViewContent.anchoredPosition, newPos) < 5)
            {
                scrollViewContent.anchoredPosition = newPos;
                currentTime = 0;
                break;
            }
        }
    }
}
