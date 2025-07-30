using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : MonoBehaviour
{
    public enum Grade { Common, Rare, Epic, Legendary }

    [Header("UI")]
    [SerializeField] private Image bgImage;         // Inspector에서 반투명 배경 연결
    [SerializeField] private Image cooldownFill;    // Inspector에서 쿨타임 오버레이 연결 (불투명, Filled)
    [SerializeField] private TMP_Text cooldownText; // Inspector에서 쿨타임 텍스트 연결

    private Sprite[] backgroundSprites; // 등급별 Sprite 코드에서 로드
    private Sprite[] cooldownFillSprites;

    private float cooldownDuration;
    private float cooldownTimer;
    private bool isCooldown = false;

    void Awake()
    {
        // 등급별 배경 스프라이트 Resources에서 코드로만 로드
        backgroundSprites = new Sprite[4];
        backgroundSprites[0] = Resources.Load<Sprite>("Textures/BattleScene/CommonBack");
        backgroundSprites[1] = Resources.Load<Sprite>("Textures/BattleScene/RareBack");
        backgroundSprites[2] = Resources.Load<Sprite>("Textures/BattleScene/EpicBack");
        backgroundSprites[3] = Resources.Load<Sprite>("Textures/BattleScene/LegendaryBack");
        cooldownFillSprites = new Sprite[4];
        cooldownFillSprites[0] = Resources.Load<Sprite>("Textures/BattleScene/Common");
        cooldownFillSprites[1] = Resources.Load<Sprite>("Textures/BattleScene/Rare");
        cooldownFillSprites[2] = Resources.Load<Sprite>("Textures/BattleScene/Epic");
        cooldownFillSprites[3] = Resources.Load<Sprite>("Textures/BattleScene/Legendary");
        if (cooldownFill) cooldownFill.gameObject.SetActive(false);
        if (cooldownText) cooldownText.gameObject.SetActive(false);
    }

    // 등급별로 배경 교체 (Inspector에서 bgImage를 반투명으로 세팅해두고 여기서 Sprite만 바꿈)
    public void SetGrade(Grade grade)
    {
        int idx = (int)grade;
        if (backgroundSprites != null && idx >= 0 && idx < backgroundSprites.Length && bgImage)
            bgImage.sprite = backgroundSprites[idx];
        if (cooldownFillSprites != null && idx >= 0 && idx < cooldownFillSprites.Length && cooldownFill)
            cooldownFill.sprite = cooldownFillSprites[idx];
    }

    //외부에서 쿨타임 시작할 때 호출
    public void StartCooldown(float duration)
    {
        cooldownDuration = duration;
        cooldownTimer = cooldownDuration;
        isCooldown = true;

        if (cooldownFill) cooldownFill.gameObject.SetActive(true);
        if (cooldownText) cooldownText.gameObject.SetActive(true);

        UpdateCooldownUI();
    }

    void Update()
    {
        if (!isCooldown) return;

        cooldownTimer -= Time.deltaTime;
        UpdateCooldownUI();

        if (cooldownTimer <= 0)
        {
            isCooldown = false;
            if (cooldownText) cooldownText.gameObject.SetActive(false);
        }
    }

    private void UpdateCooldownUI()
    {
        if (cooldownFill)
        {
            float ratio = Mathf.Clamp01(cooldownTimer / cooldownDuration);
            cooldownFill.fillAmount = ratio;

            Color c = cooldownFill.color;
            c.a = (ratio > 0) ? 0.0f : 0.9f;
            cooldownFill.color = c;
        }

        if (cooldownText)
        {
            if (cooldownTimer > 0)
            {
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = Mathf.CeilToInt(cooldownTimer).ToString();
            }
            else
            {
                cooldownText.gameObject.SetActive(false);
            }
        }
    }


    // 버튼 클릭 시 쿨타임 테스트 (임시/개발용)
    public void OnClickSkill()
    {
        if (!isCooldown)
            StartCooldown(cooldownDuration); // 예시: 미리 세팅된 쿨타임으로 다시 시작
    }
}
