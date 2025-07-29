using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public RectTransform barImage;      // BarImage RectTransform
    public RectTransform progressIcon;  // ProgressIcon RectTransform

    // 진행 시간 (예시: 10초 만에 100% 진행)
    public float totalTime = 10f;
    private float curTime = 0f;

    void Update()
    {
        // 테스트용: 시간에 따라 0~1로 진행
        if (curTime < totalTime)
        {
            curTime += Time.deltaTime;
            float progress = Mathf.Clamp01(curTime / totalTime);
            SetProgress(progress);
        }
    }

    public void SetProgress(float progress)
    {
        progress = Mathf.Clamp01(progress);

        // progress 0일 때 -halfBar, 1일 때 +halfBar
        float x = Mathf.Lerp(0, barImage.rect.width, progress);

        progressIcon.anchoredPosition = new Vector2(x, progressIcon.anchoredPosition.y);
    }
}
