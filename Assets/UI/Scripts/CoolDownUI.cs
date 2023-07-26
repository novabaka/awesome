using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    public Image fill;
    public float maxCooldown = 5f;
    private float currentCooldown;
    private bool useSkill = false;

    void Awake()
    {
        currentCooldown = maxCooldown;
    }
    public void onClickButton()
    {
        useSkill = true;
        this.GetComponent<Button>().interactable = false;
    }

    public void SetMaxCooldown(in float value)
    {
        maxCooldown = value;
        UpdateFiilAmount();
    }

    public void SetCurrentCooldown(in float value)
    {
        currentCooldown = value;
        UpdateFiilAmount();
    }

    private void UpdateFiilAmount()
    {
        fill.fillAmount = currentCooldown / maxCooldown;
    }

    private void Update()
    {
        if (useSkill)
        {
            SetCurrentCooldown(currentCooldown - Time.deltaTime);

            if (currentCooldown < 0f)
            {
                currentCooldown = maxCooldown;
                useSkill = false;
                this.GetComponent<Button>().interactable = true;
            }
        }
        
    }

}
