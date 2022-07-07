using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangBang : MonoBehaviour
{

    public string weaponEquipped;
    // Start is called before the first frame update
    void Start()
    {
        weaponEquipped = "Adware";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Bang()
    {
        //GetComponent<Collider>().Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)));
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "Enemy")
            {
                //Destroy(hit.transform.gameObject);
                FPSEnemy enemyScript = hit.transform.gameObject.GetComponent<FPSEnemy>();
                if (enemyScript.type == weaponEquipped)
                {
                    enemyScript.health = enemyScript.health - 10;
                }

                else if (enemyScript.type != weaponEquipped)
                {
                    enemyScript.health = enemyScript.health - 5;
                }

                Debug.Log(enemyScript.type);
            }

        }
    }

    public void WeaponPicker(string weaponName)
    {
        weaponEquipped = weaponName;
    }
}
