using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AEXTRAOxygenBar : MonoBehaviour
{
    /*
    //IMAGE CLASS FOR THE IMAGE OF OXYGEN
    private Oxygen oxygen;
    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();

        oxygen = new Oxygen();
    }

    private void Update()
    {
        oxygen.Update();

        //Fills the meter to full. 1 = Full Bar
        barImage.fillAmount = 1;

        barImage.fillAmount = oxygen.GetOxygenNormalized();

        // Oxygen Depletion Code
        if (Input.GetKey(KeyCode.Space))
        {
            oxygen.TrySpendOxygen(30);
        }

    }

}

//LOGIC CODE OF THE OXYGEN
public class AEXTRAOxygen
{
    public const int OXYGEN_MAX = 100;

    private float oxygenAmount;
    private float OxygenRegenAmount;

    public Oxygen()
    {
        oxygenAmount = 0;
        OxygenRegenAmount = 30f;
    }

    public void Update()
    {
        oxygenAmount += OxygenRegenAmount * Time.deltaTime;
        oxygenAmount = Mathf.Clamp(oxygenAmount, 0f, OXYGEN_MAX);
    }

    public void TrySpendOxygen(int amount)
    {
        if (oxygenAmount >= amount)
        {
            oxygenAmount -= amount;
        }
    }

    public float GetOxygenNormalized()
    {
        return oxygenAmount / OXYGEN_MAX;
    }
    */
}
