using UnityEngine;
using TMPro;

public class TextBlink : MonoBehaviour
{
    public TMP_Text Text;
    public float Speed = 1f;

    private bool isMaxAlfa = true;
    private float alfa = 1f;

    public void Update()
    {
        if (isMaxAlfa == true) Decrease();
        else Increase();
    }

    public void Increase()
    {
        alfa += Speed * Time.deltaTime;
        Text.color = new Color(1, 1, 1, alfa);
        if (alfa > 0.99) isMaxAlfa = true;
    }

    public void Decrease()
    {
        alfa -= Speed * Time.deltaTime;
        Text.color = new Color(1, 1, 1, alfa);
        if (alfa < 0.01) isMaxAlfa = false;
    }      
}

