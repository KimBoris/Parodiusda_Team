using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearManager : MonoBehaviour
{
    public Text textClearMent;

    private void OnEnable()
    {
        textClearMent.text = "플레이해주셔서 감사합니다\n끝난 줄 아시겠지만, 여기서 R 키를 누르면\n또 다른 세계가 열리게 됩니다\n\n궁금하면 눌러봐요!!";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            textClearMent.text = "뻥입니다\n사실 R 키가 아니에요 ㅋㅋㅋ\n키보드 어딘가의 키를 누르면\n숨겨진 스테이지로 이동합니다\n궁금하면 제작진에게 물어보세요\n타이틀화면으로 가려면 T 를 누르세요";
        }
    }
}
