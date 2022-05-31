using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationDelegate : MonoBehaviour
{
    public GameObject leftArmAttackPoint, rightArmAttackPoint,
        leftLegAttackPoint, rightLegAttackPoint;

    public float standUpTimer = 2f;

    private CharacterAnimation characterScript;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip whooshSound, fallSound, groundHitSound, deadSound;

    private EnemyMovement enemyMovement;

    private ShakeCamera shakeCamera;

    private void Awake()
    {
        characterScript = GetComponent<CharacterAnimation>();
        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag(Tags.ENEMY_TAG))
        {
            enemyMovement = GetComponentInParent<EnemyMovement>();
        }
        //burada ShakeCamera() scriptine erişmek için cameranın tagini kullanarak camerayı bulduk,
        //sonra cameradan da bileşenlerine erişerek ShakeCamera scriptine ulaştık.
        shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<ShakeCamera>();
    }
    #region Attacks On/Off for legs and arms
    void LeftArmAttackOn()
    {
        leftArmAttackPoint.SetActive(true);
    }

    void LeftArmAttackOff()
    {
        if (leftArmAttackPoint.activeInHierarchy)
        {
            leftArmAttackPoint.SetActive(false);
        }
    }
    void RightArmAttackOn()
    {
        rightArmAttackPoint.SetActive(true);
    }

    void RightArmAttackOff()
    {
        if (leftArmAttackPoint.activeInHierarchy)
        {
            leftArmAttackPoint.SetActive(false);
        }
    }
    void LeftLegAttackOn()
    {
        leftLegAttackPoint.SetActive(true);
    }

    void LeftLegAttackOff()
    {
        if (leftArmAttackPoint.activeInHierarchy)
        {
            leftLegAttackPoint.SetActive(false);
        }
    }
    void RightLegAttackOn()
    {
        rightLegAttackPoint.SetActive(true);
    }

    void RightLegAttackOff()
    {
        if (rightLegAttackPoint.activeInHierarchy)
        {
            rightLegAttackPoint.SetActive(false);
        }
    }
    #endregion

    #region Attack Tags On/Off for Player Left Sides 
    void TagLeftArm()
    {
        leftArmAttackPoint.tag = Tags.LEFT_ARM_TAG;
    }

    void UnTagLeftArm()
    {
        leftArmAttackPoint.tag = Tags.UNTAGGED_TAG;
    }
    void TagLeftLeg()
    {
        leftLegAttackPoint.tag = Tags.LEFT_ARM_TAG;
    }

    void UnTagLeftLeg()
    {
        leftLegAttackPoint.tag = Tags.UNTAGGED_TAG;
    }
    #endregion

    #region Enemy Stand Up Coroutine
    void EnemyStandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);
        characterScript.StandUp();
    }
    #endregion

    #region Game Sounds

    public void AttackFXSound()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = whooshSound;
        audioSource.Play();
    }

    void CharacterDiedSound()
    {
        audioSource.volume = 1f;
        audioSource.clip = deadSound;
        audioSource.Play();
    }

    void EnemyKnockedDownSound()
    {
        audioSource.clip = fallSound;
        audioSource.Play();
    }

    void EnemyHitGroundSound()
    {
        audioSource.clip = groundHitSound;
        audioSource.Play();
    }

    #endregion

    #region Enemy Movement Script Enable/Disable
    void DisableEnemyMovement()
    {
        enemyMovement.enabled = false;

        //set the enemy parent to default layer(0)
        transform.parent.gameObject.layer = 0;
    }

    void EnableEnemyMovement()
    {
        enemyMovement.enabled = true;

        //set the enemy parent to enemy layer(10)
        transform.parent.gameObject.layer = 10;
    }
    #endregion

    #region Camera Shake
    void ShakeCameraFall()
    {
        shakeCamera.ShouldShake = true;
    }
    #endregion

    void CharacterDied()
    {
        Invoke("DeactivateGameObject", 2f);
    }

    void DeactivateGameObject()
    {
        //singleton usage
        EnemyManager.instance.SpawnEnemy();

        gameObject.SetActive(false);
    }
}
