using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ��ũ��Ʈ ���� ��� //
 * 
 * 
 * - �ؽ�Ʈ �߰��� ���
 * 
 * Unity �����ͻ� text�� UIManager�� ������(�ѹ�)�� ��ġ��Ű��
 * �ű⿡ �ش��ϴ� �ѹ��� switch(num)�� ���̽��� �߰���Ŵ.
 * 
 * + ���� : ������ �Ҵ�� ���� �ѹ��� �����ʵ��� �����Ұ� 
 *          �����ѹ� �Ҵ緮�� �ش� ���� UIManager���� ������ ����. [UI_fixedvalue]
 * 
 * 
 * - ��� �߰��� ��� 
 *  
 * Language�� �ش��ϴ� ���� �߰��ϰ� lan���� ����ϴ� switch���� �ش� �� �ش��ϴ�
 * ������ �����Ͽ� �߰��ϸ��. 
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
                    case Language.KOR: text = "���� ���� : " + GameManager.GameVersion; break;
                    case Language.ENG: text = "GameVersion : " + GameManager.GameVersion; break;
                }
                break;
            case 1:
                switch (lan)
                {
                    case Language.KOR: text = "���ο� ����"; break;
                    case Language.ENG: text = "New Game"; break;
                }
                break;
            case 2:
                switch (lan)
                {
                    case Language.KOR: text = "�ҷ�����"; break;
                    case Language.ENG: text = "Game Load"; break;
                }
                break;
            case 3:
                switch (lan)
                {
                    case Language.KOR: text = "����"; break;
                    case Language.ENG: text = "Achievement"; break;
                }
                break;
            case 4:
                switch (lan)
                {
                    case Language.KOR: text = "�ɼ� / ����"; break;
                    case Language.ENG: text = "Option / Setting"; break;
                }
                break;
            case 5:
                switch (lan)
                {
                    case Language.KOR: text = "���� ����"; break;
                    case Language.ENG: text = "Game Exit"; break;
                }
                break;

            case 6:
                switch (lan)
                {
                    case Language.KOR: text = "ȯ�� ����"; break;
                    case Language.ENG: text = "Game Setting"; break;
                }
                break;
            case 7:
                switch (lan)
                {
                    case Language.KOR: text = "��ü �Ҹ�ũ��"; break;
                    case Language.ENG: text = "Total Sound"; break;
                }
                break;
            case 8:
                switch (lan)
                {
                    case Language.KOR: text = "�����"; break;
                    case Language.ENG: text = "BGM"; break;
                }
                break;
            case 9:
                switch (lan)
                {
                    case Language.KOR: text = "ȿ����"; break;
                    case Language.ENG: text = "SFX"; break;
                }
                break;
            case 10:
                switch (lan)
                {
                    case Language.KOR: text = "��� ����"; break;
                    case Language.ENG: text = "Language"; break;
                }
                break;
            case 11:
                switch (lan)
                {
                    case Language.KOR: text = "�ݱ�"; break;
                    case Language.ENG: text = "Close"; break;
                }
                break;
            case 12:
                switch (lan)
                {
                    case Language.KOR: text = "�̹� ����� �����Ͱ� �ֽ��ϴ�. ���� �����͸� ����� ���ο� ������ �����Ͻðڽ��ϱ�? "; break;
                    case Language.ENG: text = "You already have saved data. Clear existing data and start a new game?"; break;
                }
                break;
            case 13:
                switch (lan)
                {
                    case Language.KOR: text = "��"; break;
                    case Language.ENG: text = "Yes"; break;
                }
                break;
            case 14:
                switch (lan)
                {
                    case Language.KOR: text = "�ƴϿ�"; break;
                    case Language.ENG: text = "No"; break;
                }
                break;
            /*
             * play ������ ���� �ؽ�Ʈ�� 150���� �����մϴ�.
             */
            case 150:
                switch (lan) 
                {
                    case Language.KOR: text = "ī �� �� ��"; break;
                    case Language.ENG: text = "Card Setting"; break;
                }
                break;
            case 151:
                switch (lan)
                {
                    case Language.KOR: text = "��� ī��"; break;
                    case Language.ENG: text = "Active Card"; break;
                }
                break;
            case 152:
                switch (lan)
                {
                    case Language.KOR: text = "�� ��"; break;
                    case Language.ENG: text = "Passive Card"; break;
                }
                break;
            case 153:
                switch (lan)
                {
                    case Language.KOR: text = "ĳ����"; break;
                    case Language.ENG: text = "Character"; break;
                }
                break;
            case 154:
                switch (lan)
                {
                    case Language.KOR: text = "����ϱ�"; break;
                    case Language.ENG: text = "Continue"; break;
                }
                break;
            case 155:
                switch (lan)
                {
                    case Language.KOR: text = "����ȭ��"; break;
                    case Language.ENG: text = "MainTitle"; break;
                }
                break;
            case 156:
                switch (lan)
                {
                    case Language.KOR: text = "�ɼ�"; break;
                    case Language.ENG: text = "Option"; break;
                }
                break;
            case 157:
                switch (lan)
                {
                    case Language.KOR: text = "��������"; break;
                    case Language.ENG: text = "GameExit"; break;
                }
                break;



            /*
             * ĳ���� �̸� �ڵ� �ο�
             * �ּ� ÷�� ���ּž� ���� �� �ǰ��մϴ�^^
             * 1000 ~ ?
             */
            case 1000:
                switch (lan)
                {
                    case Language.KOR: text = "���ΰ�"; break;          // ���ΰ� �̸�
                    case Language.ENG: text = "Player"; break;
                }
                break;
            case 1001:
                switch (lan)
                {
                    case Language.KOR: text = "����"; break;          // ����
                    case Language.ENG: text = "Mother"; break;
                }
                break;
            case 1002:
                switch (lan)
                {
                    case Language.KOR: text = "����A"; break;          // ����A
                    case Language.ENG: text = "SoldierA"; break;
                }
                break;
            case 1003:
                switch (lan)
                {
                    case Language.KOR: text = "����B"; break;          // ����B
                    case Language.ENG: text = "SoldierB"; break;
                }
                break;
            default: 
                break;
        }
        return text;
    }

        
    /*
     * ��ȭ�� ���� ��� ����.
     * 
     */
    public string DialogChange(int num)
    {
        lan = (Language)GameManager.instance.data.Language;

        switch (num)
        {
            /*
             * �����׿��� ���̴� ��� 
             *  #1 ������ / ������ ������ ��ȭ�ϴ� ���ΰ�
             *  0 ~ 5
             *  
             *  #2 ������ / �濡�� ������� ��ȭ
             *  6 ~ 7
             *  
             *  #3 ������ / ���� ���簡 ã�ƿԴ�.
             *  [#3���� �Ѿ�� �� ] �� �߱� �Ҹ� ����
             *  11�� �ؽ�Ʈ�� 12�� �ؽ�Ʈ ���̿� ������ ������.
             *  8 ~ 12
             */
            case 0:
                switch (lan) 
                {
                    case Language.KOR: dialog = "��� ��Ծ��~"; break;
                    case Language.ENG: dialog = "Come and eat, son"; break;
                }
                break;
            case 1:
                switch (lan)
                {
                    // ������ �μ��� ����
                    case Language.KOR: dialog = "�� ��Ӵ�. �� ���۸� �а� ���Կ�!"; break;
                    case Language.ENG: dialog = "All right, mother. I'll just cut this firewood."; break;
                }
                break;
            case 2:
                switch (lan)
                {
                    case Language.KOR: dialog = "�ֱ� ���ձ��� �� ������ �����ߴٰ� �ϴ���... \n�츰 ��������?"; break;
                    case Language.ENG: dialog = "I heard that the devil king's legion had recently attacked a nearby village... \nAre we okey?"; break;
                }
                break;
            case 3:
                switch (lan)
                {
                    case Language.KOR: dialog = "�����ƿ� ��Ӵ�. \n���������� ���� ���ص帱�Կ�."; break;
                    case Language.ENG: dialog = "It's okay, mother. \nI'll save you if you're in danger."; break;
                }
                break;
            case 4:
                switch (lan)
                {
                    case Language.KOR: dialog = "���̰� ���̶� ����. \n�׷��� ���� ������ �ؾ߰ڳ� �츮 �Ƶ�!"; break;
                    case Language.ENG: dialog = "Oh, thank you for saying that. \nThen I'll have to train more My son."; break;
                }
                break;
            case 5:
                switch (lan)
                {
                    case Language.KOR: dialog = "��! (�����̴�. ��Ӵϴ� ���� �ɷ��� ���� �𸣴µ� �ϳ�)"; break;
                    case Language.ENG: dialog = "yes! (Fortunately, my mother doesn't seem to know my abilities yet.)"; break;
                }
                break;

            case 6:
                switch (lan)
                {
                    case Language.KOR: dialog = "������ �� ������..."; break;
                    case Language.ENG: dialog = "I think it's around here..."; break;
                }
                break;
            case 7:
                switch (lan)
                {
                    case Language.KOR: dialog = "�̷� ���� ������ �����ڰ� �ִٴ� �ž�?"; break;
                    case Language.ENG: dialog = "Is there really a savior in this place like this?"; break;
                }
                break;
            case 8:
                switch (lan)
                {
                    case Language.KOR: dialog = "�ۿ� ���� �Գ�����. ��������."; break;
                    case Language.ENG: dialog = "I think there's someone out there, son. Go take a look"; break;
                }
                break;
            case 9:
                switch (lan)
                {
                    // ���� ���� ������ ����
                    case Language.KOR: dialog = "�� ��Ӵ�."; break;
                    case Language.ENG: dialog = "Yes, Mother."; break;
                }
                break;
            case 10:
                switch (lan)
                {
                    case Language.KOR: dialog = "Ŭ�󼾺���ũ �ռ����� �Խ��ϴ�."; break;
                    case Language.ENG: dialog = "We're from the Klaasenberg."; break;
                }
                break;
            case 11:
                switch (lan)
                {
                    case Language.KOR: dialog = "Ŭ�� ���� �ղ��� ������ ��ɼ��Դϴ�."; break;
                    case Language.ENG: dialog = "This is an order from King Klassen Leden."; break;
                }
                break;
            case 12: // ������ ����.
                switch (lan)
                {
                    case Language.KOR: dialog = "\n�ְ� ���� ���� ���� ����������\n�̸����� ���ϴ϶�\n\n����� ���� �����ڷ�\n����� ���� ��ũ��Ʈ����Ʈ�� ���ϱ� ����\n����� ���� ���� �� �� ������ �����ϸ���\n���� �����÷�Ʈ����\n������ ���� ���̺񸮺�\n���� �� ���̵�����Ʈ�� �̸�����\n\n�ҿ��� �ٷ�� ���� ����� �Ƕ��.\n\nŬ�󼾺���ũ �ռ����κ���\nKlassenberg"; break;
                    case Language.ENG: dialog = "asdf"; break;
                }
                break;
            case 13:
                switch (lan)
                {
                    case Language.KOR: dialog = "��... ���� ���ϱ� ������..."; break;
                    case Language.ENG: dialog = "Sigh... It's bothering to save the world...."; break;
                }
                break;
                /*
                 * 14 ~ 16 ��������� �Ѿ�� ����Ǵ� ��ȭ.
                 */
            case 14:
                switch (lan)
                {
                    case Language.KOR: dialog = "��Ӵ�, �ٳ�ðԿ�."; break;
                    case Language.ENG: dialog = ""; break;
                }
                break;
            case 15:
                switch (lan)
                {
                    case Language.KOR: dialog = "�ϱ��� ������, �ǰ��ؾ��Ѵ�. �Ƶ�..."; break;
                    case Language.ENG: dialog = ""; break;
                }
                break;
            case 16:
                switch (lan)
                {
                    case Language.KOR: dialog = "�������� ���ʽÿ�. �����ڴ�."; break;
                    case Language.ENG: dialog = ""; break;
                }
                break;
            default:
                break;
        }
        return dialog;
    }
}