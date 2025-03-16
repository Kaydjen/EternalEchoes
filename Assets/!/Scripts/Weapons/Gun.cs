using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Gun : AbstractWeapon
{
    public float _nextFireTime = 0;
    private GunScriptable _data; 
    protected override void Start() => _data = Data as GunScriptable;
    public override void Attack()
    {
        // слой €кий рейкаст ≥гнорить (там дальше в if(Physics.Raycast(...)) закомент≥ровано лейер маск шоб работало розкоментуй

        //int layerMask = ~LayerMask.GetMask("LayerA");

        if (Time.time >= _nextFireTime && !_data._isMagEmpty)
        {
            RaycastHit hit;

            //«вук стрельби

            /*aSource.clip = shootClipPistol;
            aSource.Play();*/

            if (Physics.Raycast(AimDirection.Direction.position, AimDirection.Direction.forward, out hit, _data._range/*, layerMask*/))
            {
                IDamageGetter target = hit.collider.GetComponentInParent<IDamageGetter>();

                //якшо при попадан≥њ об'Їкту не наноситьс€ дамаг то спавнитьс€ дира в≥д пул≥

                /*if (target == null)
                {
                    float liftHeight = 0.01f;
                    Vector3 hitNormal = hit.normal;
                    Quaternion rotation = Quaternion.LookRotation(hitNormal, Vector3.up);
                    GameObject bulletLandedInstance = Instantiate(Bulletlanded, hit.point + hitNormal * liftHeight, rotation);
                    bulletLandedInstance.transform.SetParent(hit.collider.gameObject.transform);
                }*/

                if (target != null)
                {
                    target.GetDamage(_data._damage);
                }

                //якшо у об'Їкта Ї р≥ж≥дбод≥ то його т≥па назад в≥дкине наверно € хз €к це работаЇ вообще ≥ зв≥дки ц€ херь вз€лась в код≥ 

                /*Rigidbody targetRigidbody = hit.collider.GetComponent<Rigidbody>();
                if (targetRigidbody != null)
                {
                    targetRigidbody.AddForceAtPosition(fpsCam.transform.forward * 2f, hit.point, ForceMode.Impulse);
                }*/
            }

            _data._currentAmmo--;

            //вивод текста ск≥ки пуль

            //magTextPistol.text = $"{_data._currentAmmo}/{_data._totalAmmo}";

            if (_data._currentAmmo <= 0)
            {
                _data._isMagEmpty = true;
                StartCoroutine(Reload());
            }

            _nextFireTime = Time.time + _data._fireRate;
        }
    }

    IEnumerator Reload()
    {
        //«вук перезар€дки ≥ ан≥мка
        /*aSource.clip = reloadClipPistol;
        aSource.Play();
        animator.SetBool("IsReloading", true);*/
        yield return new WaitForSeconds(_data._reloadTime);
        _data._currentAmmo = _data._totalAmmo;
        _data._isMagEmpty = false;
        //обновка текста ≥ ан≥мка к≥нець(ан≥мки нада буде переробити а то € через bool робив)
        /*magTextPistol.text = $"{_data._currentAmmo}/{_data._totalAmmo}";
        animator.SetBool("IsReloading", false);*/
    }
    public override void EnterAimingMode()
    {
        throw new System.NotImplementedException();
    }
    public override void ExitAimingMode()
    {
        throw new System.NotImplementedException();
    }
}
