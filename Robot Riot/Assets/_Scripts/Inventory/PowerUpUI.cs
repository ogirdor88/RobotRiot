using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private RawImage powerUpIcon;
    [SerializeField] private TMP_Text powerUpName;
    [SerializeField] private TMP_Text powerUpTimeRemaining;
    [SerializeField] private UnityEngine.UI.Image powerUpBar;
    [SerializeField] private UnityEngine.UI.Image backPowerUpBar;

    private void Start()
    {
        UpdatePowerup(null, null, -1, -1);
    }
    public void UpdatePowerup(Texture2D powerupImage, string name, int time, int maxTime)
    {

        // Assign textures
        if (powerupImage != null)
        {
            powerUpIcon.GetComponent<RawImage>().texture = powerupImage;
            powerUpIcon.color = new Color(powerUpIcon.color.r, powerUpIcon.color.g, powerUpIcon.color.b, 100);
        }
        else
            powerUpIcon.color = new Color(powerUpIcon.color.r, powerUpIcon.color.g, powerUpIcon.color.b, 0);
        if (powerUpName != null && name != null)
        {
            //weaponName.text = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(currentWeapon.GetComponent<Weapon>().weapon));
            powerUpName.text = name;
        }
        else
            powerUpName.text = string.Empty;
        if (powerUpTimeRemaining != null && powerUpBar != null && time >= 0)
        {
            powerUpTimeRemaining.text = "TIME: " + time.ToString();
            powerUpBar.fillAmount = (float)time / (float)maxTime;
            powerUpBar.enabled = true;
            backPowerUpBar.enabled = true;
        }
        else
        {
            powerUpTimeRemaining.text = string.Empty;
            powerUpBar.enabled = false;
            backPowerUpBar.enabled = false;
        }
            
    }
}
