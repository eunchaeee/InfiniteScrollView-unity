using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollDrag : MonoBehaviour
{
    public RectTransform scrollViewContent;
    public float speed = 0.8f;

    public float spacing;
    public float distance;
    float currentTime;

    public static ScrollDrag instance;
    Coroutine movecoroutine;

    private Vector3 startTouchPos;
    private Vector3 moveTouchPos;

    bool isDrag;

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

        // Devices Touch check
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                moveTouchPos = touch.position;
                DragCard();
            }
            if (touch.phase == TouchPhase.Ended)
            {
            }
        }

#if UNITY_EDITOR
        // Test Windows MouseClick check
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            moveTouchPos = Input.mousePosition;
            DragCard();
        }

        if (Input.GetMouseButtonUp(0))
        {
        }
#endif

        //// Move Right
        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector2 newPos = scrollViewContent.anchoredPosition + new Vector2(distance, 0);
        //    if (movecoroutine != null)
        //    {
        //        StopCoroutine(movecoroutine);
        //    }
        //    movecoroutine = StartCoroutine(moveContent(newPos));
        //}

        //// Move Left
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 newPos = scrollViewContent.anchoredPosition - new Vector2(distance, 0);
        //    if (movecoroutine != null)
        //    {
        //        StopCoroutine(movecoroutine);
        //    }
        //    movecoroutine = StartCoroutine(moveContent(newPos));

        //}
    }

    void DragCard()
    {
        float dis = Mathf.Abs(startTouchPos.x - moveTouchPos.x);
        if(dis < 100f)
        {
            return;
        }

        // Drag Right
        if (startTouchPos.x > moveTouchPos.x)
        {
            if (!isDrag)
            {
                isDrag = true;

                Vector2 newPos = scrollViewContent.anchoredPosition - new Vector2(distance, 0);
                if (movecoroutine != null)
                {
                    StopCoroutine(movecoroutine);
                }
                movecoroutine = StartCoroutine(moveContent(newPos));
            }

        }

        // Drag left
        if (startTouchPos.x < moveTouchPos.x)
        {
            if (!isDrag)
            {
                isDrag = true;

                Vector2 newPos = scrollViewContent.anchoredPosition + new Vector2(distance, 0);
                if (movecoroutine != null)
                {
                    StopCoroutine(movecoroutine);
                }
                movecoroutine = StartCoroutine(moveContent(newPos));
            }
        }
    }



    IEnumerator moveContent(Vector2 newPos)
    {
        while (true)
        {
            currentTime += Time.deltaTime;
            scrollViewContent.anchoredPosition = Vector2.Lerp(scrollViewContent.anchoredPosition, newPos, currentTime * speed);
            yield return null;

            if(Vector2.Distance(scrollViewContent.anchoredPosition, newPos) < 5)
            {
                scrollViewContent.anchoredPosition = newPos;
                currentTime = 0;
                break;
            }
        }
        Debug.Log("HERE");
        isDrag = false;
    }
}
