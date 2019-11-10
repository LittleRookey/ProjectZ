using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eEnemyStatus
{
    dead, Idle, Attack
};


public class Enemy : MonoBehaviour
{
    public Animator anim;

    private VFXPool vPool;

    public HPAndSpeedManager health;
    public Text healthText;

    [Header("Basic Inf")]
    [SerializeField]
    private string char_name;
    [SerializeField]
    private float currentHP;
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float attack;
    [SerializeField]
    private float defense;
    [SerializeField]
    private int forwardNumber; // bigger the forward number is, enemy will stand at the front line
    [SerializeField]
    private double dropGold;
    [SerializeField]
    private int dropExp;
    [SerializeField]
    private int id;
    public eEnemyStatus enemyStatus;
    [SerializeField]
    public bool isBoss, isEnemy;
    public bool IsAlive
    {
        get
        {
            return currentHP > 0;
        }
    }



    [Header("ActSpeed")]
    [SerializeField]
    private float actSpeed;

    [SerializeField]
    private float maxEnergy = 5f;
    [SerializeField]
    private float startEnergy = 0f;
    [SerializeField]
    private float currentEnergy;

    [SerializeField]
    private Image speedImage;

    private static float ONE_BASE_HP = 100;
    private static float ONE_BASE_ATTACK = 10;
    private static float ONE_BASE_DEFENSE = 5;

    public bool attackAnimationPlaying;

    [SerializeField]
    private bool duringAtk;

    [SerializeField]
    private GameObject floatingTextPrefab;

    //[SerializeField]
    //private List<Skill> skills;

    private float saveSpeed;

    private void OnEnable()
    {
        ResetMonster();
    }

    public void Init(float hp, float attk, float def, int m_dropGold, int m_dropExp)
    {
        maxHP = hp;
        currentHP = maxHP;
        attack = attk;
        defense = def;
        dropGold = m_dropGold;
        dropExp = m_dropExp;
    }

    private void ResetMonster()
    {
        currentHP = maxHP;
        currentEnergy = 0f;
        duringAtk = false;
        //for(int i = 0; i < spriteMon.Length; i++)
        //{
        //    spriteMon[i].color += Color.black * 255f;
        //}
        
    }

    public void SetVFXPool(VFXPool vp)
    {
        vPool = vp;
    }

    public void UseEffect(eEffectType eEffect)
    {
        Timer t = vPool.GetFromPool((int)eEffect);
        t.transform.position = transform.position;
        t.transform.position += Vector3.up * 2;
    }

    public void UseEffects(List<eEffectType> eEffectTypes)
    {
        for(int i = 0; i < eEffectTypes.Count; ++i)
        {
            Timer t = vPool.GetFromPool((int)eEffectTypes[i]);
            t.transform.position = transform.position;
            t.transform.position += Vector3.up * 2;
        }
    }

    public int getID()
    {
        return id;
    }

    public void Attack(PlayerController target)
    {
        target.LoseHealth(attack);

        target.health.ShowHPPlayer(target.getCurrentHP(), target.getMaxHP());

        // if dead gameover
        if (!target.IsAlive)
        {

            //Show GUI to player penalty
            // give penalty to player

            // Reload scene by -10 stage

        }

    }

    private void ShowFloatingText()
    {
        Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
    }

    // damage received
    public float CalculateDamage(float atk)
    {
        PlayerData playerData = GameController.Instance.GetPlayerData();
        if (atk - defense <= 0)
        {
            return 1;
        }
       
        float randNum = Random.Range(0f, 100f);
        if (randNum <= playerData.player_critRate)
        {
            return (atk - defense) * playerData.player_critDamage;
        }
        return atk - defense;

    }

    public void LoseHealth(float atk)
    {
        currentHP -= atk;
    }

    public double getDropGold()
    {
        return dropGold;
    }

    public int getDropExp()
    {
        return dropExp;
    }

    public int getForwardNumber()
    {
        return forwardNumber;
    }

    public eEnemyStatus getEnemyState()
    {
        return enemyStatus;
    }

    public string getName()
    {
        return char_name;
    }

    public float getMaxHP()
    {
        return maxHP;
    }

    public float getCurrentHP()
    {
        return currentHP;
    }

    public float getAttack()
    {
        return attack;
    }

    public float getDefense()
    {
        return defense;
    }

    public void SetHealth(HPAndSpeedManager hps)
    {
        health = hps;
    }
    public void LoseOneHP()
    {
        currentHP--;
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Action());
        enemyStatus = eEnemyStatus.Idle;
        attackAnimationPlaying = false;
    }

    
    public IEnumerator Action(int enemyBehavePattern)
    {
        while (true)
        {
            switch (enemyStatus)
            {
                case eEnemyStatus.Idle:
                    // fade in
                    
                    break;
                case eEnemyStatus.Attack:
                    // constantly attacks player
                    int patt = Random.Range(0, 5);

                    anim.SetTrigger("attack");
                    break;
                case eEnemyStatus.dead:
                    anim.SetTrigger("dead");
                    //fade away
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    //public Enemy Clone()
    //{
    //    return 
    //}
    private IEnumerator AttackPlayer(PlayerController player)
    {
        // appear aura

        yield return new WaitForSeconds(2f);
        Attack(player);

    }
    
    public void SetAttack()
    {
        anim.SetTrigger("Attack");
        attackAnimationPlaying = false;
    }

    public void SetDamaged()
    {
        anim.SetTrigger("damage");
        attackAnimationPlaying = true;
    }

    public void SetIdle()
    {
        anim.SetTrigger("Idle");
        attackAnimationPlaying = false;
    }

    public void SetDead()
    {
        StopRunning(true);
        anim.SetTrigger("dead");
        attackAnimationPlaying = false;
        health.gameObject.SetActive(false);
    }

    public void RenewSpeed()
    {
        currentEnergy = startEnergy;

    }
    public void SetActSpeed(float given)
    {
        actSpeed = given;
    }

    public void SetSpeed(Image speedImg)
    {
        speedImage = speedImg;
    }

    public void AddEnergy()
    {
        currentEnergy += Time.deltaTime * actSpeed;
        ShowSpeed(currentEnergy, maxEnergy);
    }

    public void ShowSpeed(float current, float max)
    {
        speedImage.fillAmount = current / max;
    }

    // if false, run speed
    // else stop speed and run atk animation
    public void StopRunning(bool isRun)
    {
        duringAtk = isRun;
    }

    private IEnumerator Timer(float secs)
    {
        yield return new WaitForSeconds(secs);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!duringAtk)
        {
            if (currentEnergy >= maxEnergy)
            {
                // enemy attack player
                SetAttack();
                currentEnergy = startEnergy;
            }
            AddEnergy();
        }
    }
}
