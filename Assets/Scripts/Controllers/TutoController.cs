using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoController : MonoBehaviour
{
    public GameObject backgound;
    public Text currentText;
    public Text nextBtnText;
    public GameObject TutoCanvas;
    int cnt = 1;
    private void Start()
    {
        cnt = 1;
    }

    public void NextTuto()
    {
        if(cnt == 1) backgound.transform.position = new Vector3(960, 540, 0);
        if(cnt == 2) backgound.transform.position = new Vector3(-960, 540, 0);
        if(cnt == 3) HideCanvas();
        cnt++;
        nextBtnText.text = cnt == 3 ? "�غ� �Ǿ����ϴ�!" : "����";
        currentText.text = $"(�� 3������ �� {cnt} ��° ������)";
        
    }

    void HideCanvas()
    {
        TutoCanvas.SetActive(false);
    }
}
