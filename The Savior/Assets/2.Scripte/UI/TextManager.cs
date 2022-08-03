using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 스크립트 내용 요약 //
 * 
 * 
 * - 텍스트 추가시 요령
 * 
 * Unity 에디터상 text와 UIManager의 정보값(넘버)을 일치시키고
 * 거기에 해당하는 넘버를 switch(num)의 케이스로 추가시킴.
 * 
 * + 주의 : 씬마다 할당된 고유 넘버를 넘지않도록 주의할것 
 *          고유넘버 할당량은 해당 씬의 UIManager들이 가지고 있음. [UI_fixedvalue]
 * 
 * 
 * - 언어 추가시 요령 
 *  
 * Language에 해당하는 값을 추가하고 lan값을 사용하는 switch마다 해당 언어에 해당하는
 * 내용을 대응하여 추가하면됨. 
 * 
 * 
 */
public class TextManager : MonoBehaviour
{
    public static TextManager instance = null;
    private enum Language { KOR, ENG }; 
    private Language lan = Language.KOR;
    private string text = null;
    private string dialog = "";

    private void Awake()
    {
        instance = this;
    }

    public string ChangeLanguageText(int num)
    {
        lan = (Language)GameManager.instance.data.Language;
        switch (num)
        {
            case 0:
                switch (lan)
                {
                    case Language.KOR: text = "게임 버전 : " + GameManager.GameVersion; break;
                    case Language.ENG: text = "GameVersion : " + GameManager.GameVersion; break;
                }
                break;
            case 1:
                switch (lan)
                {
                    case Language.KOR: text = "새로운 게임"; break;
                    case Language.ENG: text = "New Game"; break;
                }
                break;
            case 2:
                switch (lan)
                {
                    case Language.KOR: text = "불러오기"; break;
                    case Language.ENG: text = "Game Load"; break;
                }
                break;
            case 3:
                switch (lan)
                {
                    case Language.KOR: text = "업적"; break;
                    case Language.ENG: text = "Achievement"; break;
                }
                break;
            case 4:
                switch (lan)
                {
                    case Language.KOR: text = "옵션 / 설정"; break;
                    case Language.ENG: text = "Option / Setting"; break;
                }
                break;
            case 5:
                switch (lan)
                {
                    case Language.KOR: text = "게임 종료"; break;
                    case Language.ENG: text = "Game Exit"; break;
                }
                break;

            case 6:
                switch (lan)
                {
                    case Language.KOR: text = "환경 설정"; break;
                    case Language.ENG: text = "Game Setting"; break;
                }
                break;
            case 7:
                switch (lan)
                {
                    case Language.KOR: text = "전체 소리크기"; break;
                    case Language.ENG: text = "Total Sound"; break;
                }
                break;
            case 8:
                switch (lan)
                {
                    case Language.KOR: text = "배경음"; break;
                    case Language.ENG: text = "BGM"; break;
                }
                break;
            case 9:
                switch (lan)
                {
                    case Language.KOR: text = "효과음"; break;
                    case Language.ENG: text = "SFX"; break;
                }
                break;
            case 10:
                switch (lan)
                {
                    case Language.KOR: text = "언어 설정"; break;
                    case Language.ENG: text = "Language"; break;
                }
                break;
            case 11:
                switch (lan)
                {
                    case Language.KOR: text = "닫기"; break;
                    case Language.ENG: text = "Close"; break;
                }
                break;
            case 12:
                switch (lan)
                {
                    case Language.KOR: text = "이미 저장된 데이터가 있습니다. 기존 데이터를 지우고 새로운 게임을 시작하시겠습니까? "; break;
                    case Language.ENG: text = "You already have saved data. Clear existing data and start a new game?"; break;
                }
                break;
            case 13:
                switch (lan)
                {
                    case Language.KOR: text = "예"; break;
                    case Language.ENG: text = "Yes"; break;
                }
                break;
            case 14:
                switch (lan)
                {
                    case Language.KOR: text = "아니오"; break;
                    case Language.ENG: text = "No"; break;
                }
                break;
            /*
             * play 씬에서 사용될 텍스트는 150부터 시작합니다.
             */
            case 150:
                switch (lan) 
                {
                    case Language.KOR: text = "카 드 설 정"; break;
                    case Language.ENG: text = "Card Setting"; break;
                }
                break;
            case 151:
                switch (lan)
                {
                    case Language.KOR: text = "사용 카드"; break;
                    case Language.ENG: text = "Active Card"; break;
                }
                break;
            case 152:
                switch (lan)
                {
                    case Language.KOR: text = "유 물"; break;
                    case Language.ENG: text = "Passive Card"; break;
                }
                break;
            case 153:
                switch (lan)
                {
                    case Language.KOR: text = "캐릭터"; break;
                    case Language.ENG: text = "Character"; break;
                }
                break;
            case 154:
                switch (lan)
                {
                    case Language.KOR: text = "계속하기"; break;
                    case Language.ENG: text = "Continue"; break;
                }
                break;
            case 155:
                switch (lan)
                {
                    case Language.KOR: text = "메인화면"; break;
                    case Language.ENG: text = "MainTitle"; break;
                }
                break;
            case 156:
                switch (lan)
                {
                    case Language.KOR: text = "옵션"; break;
                    case Language.ENG: text = "Option"; break;
                }
                break;
            case 157:
                switch (lan)
                {
                    case Language.KOR: text = "게임종료"; break;
                    case Language.ENG: text = "GameExit"; break;
                }
                break;



            /*
             * 캐릭터 이름 코드 부여
             * 주석 첨삭 해주셔야 눈이 덜 피곤합니다^^
             * 1000 ~ ?
             */
            case 1000:
                switch (lan)
                {
                    case Language.KOR: text = "주인공"; break;          // 주인공 이름
                    case Language.ENG: text = "Player"; break;
                }
                break;
            case 1001:
                switch (lan)
                {
                    case Language.KOR: text = "엄마"; break;          // 엄마
                    case Language.ENG: text = "Mother"; break;
                }
                break;
            case 1002:
                switch (lan)
                {
                    case Language.KOR: text = "병사A"; break;          // 병사A
                    case Language.ENG: text = "SoldierA"; break;
                }
                break;
            case 1003:
                switch (lan)
                {
                    case Language.KOR: text = "병사B"; break;          // 병사B
                    case Language.ENG: text = "SoldierB"; break;
                }
                break;
            default: 
                break;
        }
        return text;
    }

        
    /*
     * 대화에 사용될 대사 모음.
     * 
     */
    public string DialogChange(int num)
    {
        lan = (Language)GameManager.instance.data.Language;

        switch (num)
        {
            /*
             * 오프닝에서 쓰이는 대사 
             *  #1 오프닝 / 집에서 엄마와 대화하는 주인공
             *  0 ~ 5
             *  
             *  #2 오프닝 / 길에서 병사들의 대화
             *  6 ~ 7
             *  
             *  #3 오프닝 / 집에 병사가 찾아왔다.
             *  [#3으로 넘어올 때 ] 말 발굽 소리 사운드
             *  11번 텍스트와 12번 텍스트 사이에 편지를 보여줌.
             *  8 ~ 12
             */
            case 0:
                switch (lan) 
                {
                    case Language.KOR: dialog = "얘야 밥먹어라~"; break;
                    case Language.ENG: dialog = "Come and eat, son"; break;
                }
                break;
            case 1:
                switch (lan)
                {
                    // 나무를 부수는 사운드
                    case Language.KOR: dialog = "네 어머니. 이 장작만 패고 갈게요!"; break;
                    case Language.ENG: dialog = "All right, mother. I'll just cut this firewood."; break;
                }
                break;
            case 2:
                switch (lan)
                {
                    case Language.KOR: dialog = "최근 마왕군이 옆 마을을 습격했다고 하더라... \n우린 괜찮을까?"; break;
                    case Language.ENG: dialog = "I heard that the devil king's legion had recently attacked a nearby village... \nAre we okey?"; break;
                }
                break;
            case 3:
                switch (lan)
                {
                    case Language.KOR: dialog = "괜찮아요 어머니. \n위험해지면 제가 구해드릴게요."; break;
                    case Language.ENG: dialog = "It's okay, mother. \nI'll save you if you're in danger."; break;
                }
                break;
            case 4:
                switch (lan)
                {
                    case Language.KOR: dialog = "아이고 말이라도 고맙다. \n그러면 더욱 수련을 해야겠네 우리 아들!"; break;
                    case Language.ENG: dialog = "Oh, thank you for saying that. \nThen I'll have to train more My son."; break;
                }
                break;
            case 5:
                switch (lan)
                {
                    case Language.KOR: dialog = "네! (다행이다. 어머니는 나의 능력을 아직 모르는듯 하네)"; break;
                    case Language.ENG: dialog = "yes! (Fortunately, my mother doesn't seem to know my abilities yet.)"; break;
                }
                break;

            case 6:
                switch (lan)
                {
                    case Language.KOR: dialog = "이쯤인 거 같은데..."; break;
                    case Language.ENG: dialog = "I think it's around here..."; break;
                }
                break;
            case 7:
                switch (lan)
                {
                    case Language.KOR: dialog = "이런 곳에 정말로 구원자가 있다는 거야?"; break;
                    case Language.ENG: dialog = "Is there really a savior in this place like this?"; break;
                }
                break;
            case 8:
                switch (lan)
                {
                    case Language.KOR: dialog = "밖에 누가 왔나보다. 나가보렴."; break;
                    case Language.ENG: dialog = "I think there's someone out there, son. Go take a look"; break;
                }
                break;
            case 9:
                switch (lan)
                {
                    // 나무 문이 열리는 사운드
                    case Language.KOR: dialog = "네 어머니."; break;
                    case Language.ENG: dialog = "Yes, Mother."; break;
                }
                break;
            case 10:
                switch (lan)
                {
                    case Language.KOR: dialog = "클라센베르크 왕성에서 왔습니다."; break;
                    case Language.ENG: dialog = "We're from the Klaasenberg."; break;
                }
                break;
            case 11:
                switch (lan)
                {
                    case Language.KOR: dialog = "클라센 르덴 왕께서 보내는 명령서입니다."; break;
                    case Language.ENG: dialog = "This is an order from King Klassen Leden."; break;
                }
                break;
            case 12: // 편지를 띄운다.
                switch (lan)
                {
                    case Language.KOR: dialog = "\n최고 신인 빛의 여신 페어슈프레디의\n이름으로 명하니라\n\n당신은 빛의 구원자로\n어둠의 신인 시크잔트라하트를 멸하기 위해\n운명의 밤이 오기 전 이 세상을 구원하리니\n물의 여신플류트레네\n생명의 여신 에이비리베\n불의 신 라이덴샤프트의 이름으로\n\n소울을 다루는 빛의 약속이 되라라.\n\n클라센베르크 왕성으로부터\nKlassenberg"; break;
                    case Language.ENG: dialog = "asdf"; break;
                }
                break;
            case 13:
                switch (lan)
                {
                    case Language.KOR: dialog = "하... 세상 구하기 귀찮아..."; break;
                    case Language.ENG: dialog = "Sigh... It's bothering to save the world...."; break;
                }
                break;
                /*
                 * 14 ~ 16 월드맵으로 넘어가서 진행되는 대화.
                 */
            case 14:
                switch (lan)
                {
                    case Language.KOR: dialog = "어머니, 다녀올게요."; break;
                    case Language.ENG: dialog = ""; break;
                }
                break;
            case 15:
                switch (lan)
                {
                    case Language.KOR: dialog = "믿기지 않지만, 건강해야한다. 아들..."; break;
                    case Language.ENG: dialog = ""; break;
                }
                break;
            case 16:
                switch (lan)
                {
                    case Language.KOR: dialog = "이쪽으로 오십시오. 구원자님."; break;
                    case Language.ENG: dialog = ""; break;
                }
                break;
            default:
                break;
        }
        return dialog;
    }
}