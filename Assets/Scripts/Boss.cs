using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float _speed = 1.5f;
    private float _direction = 2.5f;
    private bool _reverse = false;
    [SerializeField]
    private float _fireRateBeam = 10f;
    private float _fireRateBeamDuration = 3f;
    private float _fireRateLaser = 0.5f;
    private float _fireRateHoming = 4f;
    private float _canFireBeam = -10f;
    private float _canFireLaser = -1f;
    private float _canFireHoming = -2f;
    private bool _beamActive = false;
    private float _directionShoot = 0.8f;
    public GameObject shieldVisual;
    public GameObject shieldGenerator;
    public GameObject attackGenerator;
    public GameObject bossHead;
    public GameObject e_laserPrefab;
    public GameObject eBeam;
    public GameObject e_HomingPrefab;
    private bool shieldStatus=true;
    // Start is called before the first frame update
    void Start()
    {
        _canFireBeam = Time.time + _fireRateBeam;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        BossShoot();
        if (bossHead == null) Destroy(this.gameObject);

    }
    private void EnemyMovement()
    {



        Vector3 bossDirection;
        if (transform.position.x > 3) _reverse = true;

        if (_reverse)
        {
            bossDirection = new Vector3(_direction * -1, 0, 0);
            if (transform.position.x < -3) _reverse = false;
        }
        else
        {
            bossDirection = new Vector3(_direction, 0, 0);
        }
        transform.Translate(bossDirection * Time.deltaTime);

    }



    private void BossShoot()
    {
        //get positions of child elements
        //activate beam every x
        // fire homing missile every y
        // shoot regular missile ever z


        int shoot_adjust = -2;

      

        if (Time.time > _canFireBeam)
        {

            if (_beamActive)
            {
                _canFireBeam = Time.time + _fireRateBeam;
                _canFireLaser = Time.time + _fireRateLaser;
                _beamActive = false;
                eBeam.SetActive(false);
            }
            else
            {

                _canFireBeam = Time.time + _fireRateBeamDuration;
                _canFireLaser = Time.time + _fireRateLaser;
                _beamActive = true;
                eBeam.SetActive(true);
            }

            //set beam to active

        }
        else if (Time.time > _canFireLaser && !_beamActive)
        {
            // PlaySound(0);
            if (_canFireLaser > 0) Instantiate(e_laserPrefab, transform.position + new Vector3(0, _directionShoot + shoot_adjust, 0), Quaternion.identity);
            _canFireLaser = Time.time + _fireRateLaser;


        }
        if (Time.time > _canFireHoming && attackGenerator != null)
        {
         
          

            if (_canFireLaser > _canFireHoming)
            {
                GameObject spawnedEnemy;
                spawnedEnemy = Instantiate(e_HomingPrefab, attackGenerator.transform.position, Quaternion.identity);
                spawnedEnemy.GetComponent<Enemy>().SetEnemyMovementType(3);
            }

            _canFireHoming = Time.time + _fireRateHoming;
        }



    }

    public void Shield(bool state)
    {

        shieldVisual.SetActive(state);
        shieldStatus = state;
    }
    public bool GetShieldStatus()
    {

        return shieldStatus;
    }
 
}
