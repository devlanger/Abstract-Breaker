using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboIndicator : MonoBehaviour
{
    [SerializeField]
    private Text comboText;

    private void Start()
    {
        ComboManager.Instance.OnComboRaised += ComboManager_OnComboRaised;
        ComboManager.Instance.OnComboFinished += ComboManager_OnComboFinished;
    }

    private void ComboManager_OnComboFinished()
    {
        Deactivate();
    }

    private void ComboManager_OnComboRaised(int combo)
    {
        Activate();
        SetCombo(combo);
    }

    public void Activate()
    {
        if (!comboText.gameObject.activeInHierarchy)
        {
            comboText.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        if (comboText.gameObject.activeInHierarchy)
        {
            comboText.gameObject.SetActive(false);
        }
    }

    public void SetCombo(int combo)
    {
        comboText.transform.localScale = Vector3.one;
        comboText.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
        comboText.text = combo.ToString();
    }
}
