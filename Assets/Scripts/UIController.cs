using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerAttack playerAttack;

    public Button attackBtn;
    public Button supperAttackBtn;

    public Text attackText;
    public Text supperAttackText;
    public Text hpText;
    public Text wavesText;

    private bool isCanAttack = true;
    private bool isCanSupperAttack = true;

    private float cooldownAttack;
    private float cooldownSupperAttack;
    private void OnEnable()
    {
        playerAttack.onAttack += RestartAttackBtn;
        playerAttack.onSupperAttack += RestartSupperAttackBtn;
    }
    public void RestartAttackBtn(float cooldown)
    {
        attackBtn.interactable = false;
        isCanAttack = false;
        cooldownAttack = cooldown;

    }
    public void RestartSupperAttackBtn(float cooldown)
    {
        supperAttackBtn.interactable = false;
        isCanSupperAttack = false;
        cooldownSupperAttack = cooldown;
    }
    public void HPCounter(float hp, float maxHP)
    {
        hpText.text = $"HP - {hp}/{maxHP}";
    }
    public void ActiveSupperAtack()
    {
        if (cooldownSupperAttack <= 0)
        {
            isCanSupperAttack = true;
            supperAttackBtn.interactable = true;
            supperAttackText.text = "Supper Attack";
        }
    }
    private void Update()
    {
        if (!isCanAttack)
        {
            cooldownAttack -= Time.deltaTime;
            attackText.text = cooldownAttack.ToString("0.0");
            if (cooldownAttack <= 0)
            {
                isCanAttack = true;
                attackBtn.interactable = true;
                attackText.text = "Attack";
            }
        }
        if (!isCanSupperAttack)
        {
            cooldownSupperAttack -= Time.deltaTime;
            supperAttackText.text = cooldownSupperAttack.ToString("0.0");
            if (cooldownSupperAttack <= 0)
            {
                isCanSupperAttack = true;
                supperAttackText.text = "Supper Attack";
            }
        }
    }
    public void ChenngeWaves(int currentWave, int allVWaves)
    {
        wavesText.text = $"Wave {currentWave}/{allVWaves}";
    }
    private void OnDisable()
    {
        playerAttack.onAttack -= RestartAttackBtn;
        playerAttack.onSupperAttack -= RestartSupperAttackBtn;
    }
}




