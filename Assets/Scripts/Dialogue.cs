using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textdisplay;
    public string[] sentences;

    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public bool isImgOn;
    public Image backgroundImage;

    void Start() {
        StartCoroutine(Type());
        backgroundImage.enabled = true;
        isImgOn = true;
    }

    void Update() {
        if(textdisplay.text == sentences[index]){
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textdisplay.text += letter;
            yield return new  WaitForSeconds(0.02f);
        }
    }

    public void NextSentence() {
        continueButton.SetActive(false);

        if(index < sentences.Length -1){
            index++;
            textdisplay.text = "";
            StartCoroutine(Type());
        } else { //desabilita texto e background
            textdisplay.text = "";
            continueButton.SetActive(false);
            backgroundImage.enabled = false;
            isImgOn = false;
        }
    }
}
