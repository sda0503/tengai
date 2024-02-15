using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStart;
    [SerializeField] private TextMeshProUGUI encyclopedia;
    [SerializeField] private TextMeshProUGUI statistics;
    [SerializeField] private TextMeshProUGUI setting;
    [SerializeField] private TextMeshProUGUI patchNotes;
    [SerializeField] private TextMeshProUGUI end;

    [SerializeField] private GameObject gameMenuPopup;
    [SerializeField] private GameObject gameStartPopup;
    [SerializeField] private GameObject characterSelectPopup;
    [SerializeField] private GameObject gameStartBtn;
    [SerializeField] private GameObject gameSettingPopup;

    [Header("CharacterPortrait")]
    [SerializeField] private Image characterSelectBG;
    [SerializeField] private Sprite ironBG;
    [SerializeField] private Sprite silentBG;
    [SerializeField] private Sprite defectBG;
    [SerializeField] private Sprite watcherBG;

    [Header("StartRelics")]
    [SerializeField] private Image startRelics;
    [SerializeField] private Sprite ironR;
    [SerializeField] private Sprite silentR;
    [SerializeField] private Sprite defectR;
    [SerializeField] private Sprite watcherR;

    [Header("CharacterDescription")]
    [SerializeField] private GameObject characterDescription;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI charDescriptionText;
    [SerializeField] private TextMeshProUGUI curHPText;
    [SerializeField] private TextMeshProUGUI maxHPText;
    [SerializeField] private TextMeshProUGUI RelicsNameText;
    [SerializeField] private TextMeshProUGUI RelicsDescriptionText;

    void OnEable()
    {
        characterSelectBG = GetComponent<Image>();
        startRelics = GetComponent<Image>();
    }

    public void OnClickGameStart()
    {
        gameMenuPopup.SetActive(false);

        gameStartPopup.SetActive(true);
    }

    public void OnClickNomalMode()
    {
        gameStartPopup.SetActive(false);

        characterSelectPopup.SetActive(true);
    }

    public void OnClickSetting()
    {
        gameSettingPopup.SetActive(true);

        gameMenuPopup.SetActive(false);
    }

    public void OnClickCancel_a()
    {
        gameMenuPopup.SetActive(true);

        gameStartPopup.SetActive(false);
    }

    public void OnClickCancel_b()
    {
        gameStartPopup.SetActive(true);

        characterSelectPopup.SetActive(false);
        characterDescription.SetActive(false);

        gameStartBtn.SetActive(false);
        RefreshBG();
    }

    public void OnClickCancel_c()
    {
        gameMenuPopup.SetActive(true);

        gameSettingPopup.SetActive(false);
    }


    public void OnClickSelectIronclad()
    {
        characterSelectBG.sprite = ironBG;
        characterSelectBG.color = new Color(255, 255, 255, 255);

        characterDescription.SetActive(true);

        characterNameText.text = "아이언클래드";
        curHPText.text = "80";
        maxHPText.text = "80";
        charDescriptionText.text = "아이언클래드의 살아남은 병사입니다.\r\n악마의 힘을 사용하기 위해 영혼을 팔았습니다.";

        startRelics.sprite = ironR;
        RelicsNameText.text = "불타는 혈액";
        RelicsDescriptionText.text = "전투 종료 시 체력을 6 회복합니다.";

        gameStartBtn.SetActive(true);
    }

    public void OnClickSelectSilent()
    {
        characterSelectBG.sprite = silentBG;
        characterSelectBG.color = new Color(255, 255, 255, 255);

        characterDescription.SetActive(true);

        characterNameText.text = "사일런트";
        curHPText.text = "70";
        maxHPText.text = "70";
        charDescriptionText.text = "안개 지대에서 온 치명적인 사냥꾼입니다.\r\n단검과 독으로 적들을 박멸합니다.";

        startRelics.sprite = silentR;
        RelicsNameText.text = "뱀의 반지";
        RelicsDescriptionText.text = "첫 턴에만 2장의 카드를 추가로 뽑습니다.";

        gameStartBtn.SetActive(true);
    }
    
    public void OnClickSelectDefect()
    {
        characterSelectBG.sprite = defectBG;
        characterSelectBG.color = new Color(255, 255, 255, 255);

        characterDescription.SetActive(true);

        characterNameText.text = "디펙트";
        curHPText.text = "75";
        maxHPText.text = "75";
        charDescriptionText.text = "자아를 깨달은 전투 자동인형입니다.\r\n고대의 기술로 구체를 만들 수 있습니다.";

        startRelics.sprite = defectR;
        RelicsNameText.text = "부서진 핵";
        RelicsDescriptionText.text = "전투 시작 시 전기를 1 번 영창합니다.";

        gameStartBtn.SetActive(true);
    }

    public void OnClickSelectWatcher()
    {
        characterSelectBG.sprite = watcherBG;
        characterSelectBG.color = new Color(255, 255, 255, 255);

        characterDescription.SetActive(true);

        characterNameText.text = "와쳐";
        curHPText.text = "72";
        maxHPText.text = "72";
        charDescriptionText.text = "첨탑을 \"평가\" 하기 위해 찾아온 눈먼 수도사입니다.\r\n강림의 경지에 이른 고수입니다.";

        startRelics.sprite = watcherR;
        RelicsNameText.text = "순수한 물";
        RelicsDescriptionText.text = "매 전투 시작 시 기적을 손으로 가져옵니다.";

        gameStartBtn.SetActive(true);
    }

    public void OnClickGameStartBtn()
    {
        SceneManager.LoadScene("Map");
    }

    public void OnClickExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void RefreshBG()
    {
        characterSelectBG.sprite = null;
        characterSelectBG.color = new Color(0, 0, 0, 240/255f);
    }
}
