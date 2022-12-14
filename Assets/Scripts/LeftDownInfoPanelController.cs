using StateMachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeftDownInfoPanelController : MonoBehaviour
{
    

    public static LeftDownInfoPanelController Instance
    {
        get; private set;
    }
    
    public int maxHealth = 10;
    public int maxMagic = 10;
    public int maxEnergy = 10;
    public float health;
    public int magic;
    public int energy;
    public int atk;
    public int def;
    public int move;
    public int level;
    public string name;
    public int AtkRange;

    public TextMeshProUGUI tmpAtk, tmpDef, tmpMove, tmpLv, tmpName, tmpRange;
    
    

    private RectTransform HealthBar, MagicBar, EnergyBar;
    private float oriHealthLen, oriMagicLen, oriEnergyLen;

    private TextMeshProUGUI tmpHP, tmpMP, tmpSP;
    
    private void Awake()
    {
        if (Instance != null && Instance != this && !Instance.gameObject.IsDestroyed())
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        HealthBar = transform.Find("HealthBar/Mask").GetComponent<RectTransform>();
        // MagicBar = transform.Find("MagicBar/Mask").GetComponent<RectTransform>();
        EnergyBar = transform.Find("EnergyBar/Mask").GetComponent<RectTransform>();
        oriHealthLen = HealthBar.rect.width;
        // oriMagicLen = MagicBar.rect.width;
        oriEnergyLen = EnergyBar.rect.width;
        tmpHP = transform.Find("Right Words/HPP").GetComponent<TextMeshProUGUI>();
        // tmpMP = transform.Find("Right Words/MPP").GetComponent<TextMeshProUGUI>();
        tmpSP = transform.Find("Right Words/SPP").GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    // Update is called once per 40ms default
    public void FixedUpdate()
    {
        HealthBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (health == 0) ? 0 : oriHealthLen * health / maxHealth);
        // MagicBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (magic == 0) ? 0 : oriMagicLen * magic / maxMagic);
        EnergyBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (energy == 0) ? 0 : oriEnergyLen * energy / maxEnergy);
        tmpHP.SetText($"{health:#0.##}/{maxHealth}");
        tmpSP.SetText($"{energy}/{maxEnergy}");
        // tmpMP.SetText($"{magic}/{maxMagic}");
        
        tmpAtk.SetText($"Atk: {atk}");
        tmpDef.SetText($"Def: {def}");
        tmpMove.SetText($"Move: {move}");
        tmpRange.SetText($"AtkRange: {AtkRange}");
        tmpLv.SetText($"Lv: {level}");
        tmpName.SetText(name);
    }
}
