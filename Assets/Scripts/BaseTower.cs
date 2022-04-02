using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    const float FIRE_COOLDOWN = 0.5f;

    public GameObject projectilePrefab;

    private List<GameObject> targets;

    private float cooldown = FIRE_COOLDOWN;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PLOP");
        this.targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown < 0)
        {
            var t = pickTarget();

            if (t != null)
            {
                Debug.Log("FIRE");
                cooldown = FIRE_COOLDOWN;
                var p = Instantiate(projectilePrefab);
                p.transform.position = transform.Find("Emitter").transform.position; // transform.position;
                p.GetComponent<BaseMissile>().SetTarget(t);
            }
        }

        cooldown -= Time.deltaTime;
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
}
