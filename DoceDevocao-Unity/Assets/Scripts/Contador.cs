using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    private Text textElement;
    private int value = 0;

    public int ID = 0;
    public Button incrementButton;
    public Button decrementButton;

    private void Start()
    {
        textElement = GetComponent<Text>();
        value = ProductDatabase.productValues[ID];
        UpdateText();
        
        incrementButton.onClick.AddListener(Increment);
        decrementButton.onClick.AddListener(Decrement);
    }

    public void Increment()
    {
        value++;
        UpdateText();
    }

    public void Decrement()
    {
        value--;
        UpdateText();
    }

    private void UpdateText()
    {
        ProductDatabase.productValues[ID] = value;
        textElement.text = value.ToString();
    }
}
