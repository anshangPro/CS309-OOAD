using TMPro;
using UnityEngine;

public class LeftDownInfoPanelController : MonoBehaviour
{
    public int maxHealth = 10;
    public int maxMagic = 10;
    public int maxEnergy = 10;
    public int health;
    public int magic;
    public int energy;

    private RectTransform HealthBar, MagicBar, EnergyBar;
    private float oriHealthLen, oriMagicLen, oriEnergyLen;

    private TextMeshProUGUI tmpHP, tmpMP, tmpSP;


    // Start is called before the first frame update
    void Start()
    {
        HealthBar = transform.Find("HealthBar/Mask").GetComponent<RectTransform>();
        MagicBar = transform.Find("MagicBar/Mask").GetComponent<RectTransform>();
        EnergyBar = transform.Find("EnergyBar/Mask").GetComponent<RectTransform>();
        oriHealthLen = HealthBar.rect.width;
        oriMagicLen = MagicBar.rect.width;
        oriEnergyLen = EnergyBar.rect.width;
        tmpHP = transform.Find("Right Words/HPP").GetComponent<TextMeshProUGUI>();
        tmpMP = transform.Find("Right Words/MPP").GetComponent<TextMeshProUGUI>();
        tmpSP = transform.Find("Right Words/SPP").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per 40ms default
    void FixedUpdate()
    {
        HealthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, oriHealthLen * health / maxHealth);
        MagicBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, oriMagicLen * magic / maxMagic);
        EnergyBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, oriEnergyLen * energy / maxEnergy);
        tmpHP.SetText($"{health}/{maxHealth}");
        tmpSP.SetText($"{energy}/{maxEnergy}");
        tmpMP.SetText($"{magic}/{maxMagic}");
    }
}
