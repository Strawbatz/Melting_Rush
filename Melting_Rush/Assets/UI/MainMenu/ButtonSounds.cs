using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler
{
    private Button btn;
    private void OnEnable() {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(ButtonClick);
    }

    private void OnDisable() {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void ButtonClick() {
        SoundManager.instance.PlaySound(SoundManager.Sound.Click);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.MouseOver);
    }

}
