using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseTower : MonoBehaviour
{
    const float FIRE_COOLDOWN = 2.5f;

    public GameObject projectilePrefab;

    private List<GameObject> targets;

    private float cooldown = FIRE_COOLDOWN;

    private GameObject target;

    public int towerLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.towerLevel = 0;
        this.targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown < 0)
        {
            target = pickTarget();
            if (target != null)
            {
                GetComponent<Animator>().SetTrigger("Fire");
            }
        }

        cooldown -= Time.deltaTime;
    }


    void DoFire()
    {
        if (target != null)
        {
            GetComponent<AudioSource>().Play();
            cooldown = FIRE_COOLDOWN;
            var p = Instantiate(projectilePrefab);
            p.transform.position = transform.Find("Emitter").transform.position; // transform.position;
            p.GetComponent<BaseMissile>().SetTarget(target);
            GetComponent<Animator>().ResetTrigger("Fire");
        }
    }

    GameObject pickTarget()
    {
        if (this.targets.Count == 0)
        {
            return null;
        }

        return this.targets[Random.Range(0, this.targets.Count)];

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.targets.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            this.targets.Remove(collider.gameObject);
        }
    }

    public void UpgradeTower()
    {
        var cost = StaticVar.upgradeBearCosts[towerLevel];

        if (towerLevel < StaticVar.upgradeBearCosts.Length - 1 && StaticVar.Ressource >= cost)
        {
            StaticVar.Ressource = -cost;
            towerLevel++;
        }
    }
}
