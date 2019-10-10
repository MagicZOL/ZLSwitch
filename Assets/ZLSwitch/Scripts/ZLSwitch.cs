using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ZLSwitch : MonoBehaviour
{
    public bool isOn;                   //스위치 on/off 상태
    [Range(0, 3)]                       //스위치 이동시간 조절바
    public float moveDuration = 3f;     //스위치 이동 애니메이션 시간

    const float totalHandleMoveLength = 72f;
    const float halfMoveLength = totalHandleMoveLength / 2;

    Image handleImage;                  //스위치 핸들 이미지
    Image backgroundImage;              //스위치 배경이미지
    RectTransform handleRectTransform;  //스위치 핸들 RectTransform

    //Coroutine : 코루틴이 실행중인지 확인하기위한 변수 선언
    Coroutine moveHandleCoroutine;      //handle 이동 코루틴
    Coroutine backgroundColorCoroutine; //배경색 변경 코루틴

    //Color
    public Color handleColor = Color.white;
    public Color offBackgroundColor = Color.blue;
    public Color OnBackgroundColor = Color.red;

    void Start()
    {
        //Handle 초기화
        GameObject handleObject = transform.Find("Handle").gameObject;

        handleRectTransform = handleObject.GetComponent<RectTransform>();

        if (isOn)
            handleRectTransform.anchoredPosition = new Vector2(halfMoveLength, 0);
        else
            handleRectTransform.anchoredPosition = new Vector2(-halfMoveLength, 0);

        //Background Image
        backgroundImage = GetComponent<Image>();
        backgroundImage.color = offBackgroundColor;

        //handle Image
        handleImage = handleObject.GetComponent<Image>();
        handleImage.color = handleColor;
    }

    public void OnClickSwitch()
    {
        isOn = !isOn;

        Vector2 fromPosition = handleRectTransform.anchoredPosition;
        Vector2 toPosition = (isOn) ? new Vector2(halfMoveLength, 0) : new Vector2(-halfMoveLength, 0);
        Vector2 distance = toPosition - fromPosition; // 시작지점과 목표지점 거리

        float ratio = Mathf.Abs(distance.x) / totalHandleMoveLength;
        //Mathf.Abs : 절대값을 반환 ex) -23 = 23, 23 = 23
        float duration = moveDuration * ratio;

        moveHandleCoroutine = StartCoroutine(moveHandle(fromPosition, toPosition, duration));

        //Handle Move Coroutine
        if (moveHandleCoroutine != null)
        {
            StopCoroutine(moveHandleCoroutine);
            moveHandleCoroutine = null;
        }
        StartCoroutine(moveHandle(fromPosition, toPosition, duration));

        //Backgroud Color Change Coroutine
        Color fromColor = backgroundImage.color;
        Color toColor = (isOn) ? OnBackgroundColor : offBackgroundColor;
        if(backgroundColorCoroutine != null)
        {
            StopCoroutine(backgroundColorCoroutine);
            backgroundColorCoroutine = null;
        }

        backgroundColorCoroutine = StartCoroutine(changeBackgroundColor(fromColor, toColor, duration));
    }

    /// <summary>
    /// 핸들을 이동하는 함수
    /// </summary>
    /// <param name="fromPosition">핸들의 시작 위치</param>
    /// <param name="toPosition">핸들의 목적지 위치</param>
    /// <param name="duration">핸들이 이동하는 시간</param>
    /// <returns>없음</returns>
    IEnumerator moveHandle(Vector2 fromPosition, Vector2 toPosition, float duration)
    {
        float currentTime = 0f;

        while (currentTime <= duration)
        {
            float t = currentTime / duration; // 현재흐른 시간 / 핸들이 이동하는 시간
            Vector2 newPosition = Vector2.Lerp(fromPosition, toPosition, t);
            handleRectTransform.anchoredPosition = newPosition;

            currentTime += Time.deltaTime; //직전프레임부터 현재프레임까지의 시간을 더해봄
            yield return null;
        }
    }

    /// <summary>
    /// 스위치 배경 색 변경 함수
    /// </summary>
    /// <param name="fromColor">초기 색상</param>
    /// <param name="toColor">변경할 색상</param>
    /// <param name="duration">변경 시간</param>
    /// <returns>없음</returns>
    IEnumerator changeBackgroundColor(Color fromColor, Color toColor, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            Color newColor = Color.Lerp(fromColor, toColor, t);
            backgroundImage.color = newColor;
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
