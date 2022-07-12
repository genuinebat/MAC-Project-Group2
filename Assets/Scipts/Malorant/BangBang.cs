using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malorant
{
    public enum Malware {Adware, Botware, Ransomware, Trojan}

    class Weapon
    {
        public string Name { get; private set; }
        public Malware Target { get; private set; }

        public Weapon(string _name, Malware _target)
        {
            Name = _name;
            Target = _target;
        }
    }

    public class BangBang : MonoBehaviour
    {
        List<Weapon> weapons = new List<Weapon>();
        Weapon weaponEquipped;

        // Start is called before the first frame update
        void Start()
        {
            CreateWeapons();
            EquipWeapon(0);
        }

        // Update is called once per frame
        void Update()
        {
        }

        void CreateWeapons()
        {
            Weapon adware = new Weapon("Adware", Malware.Adware);
            Weapon botware = new Weapon("Adware", Malware.Botware);
            Weapon ransomware = new Weapon("Adware", Malware.Ransomware);
            Weapon trojan = new Weapon("Adware", Malware.Trojan);

            weapons.Add(adware);
            weapons.Add(botware);
            weapons.Add(ransomware);
            weapons.Add(trojan);
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
                    FPSEnemy enemy = hit.transform.gameObject.GetComponent<FPSEnemy>();

                    if (enemy.Type == weaponEquipped.Target)
                    {
                        enemy.TakeDamage(true);
                    }
                    else
                    {
                        enemy.TakeDamage(false);
                    }
                }

            }
        }

        public void EquipWeapon(int num)
        {
            weaponEquipped = weapons[num];
        }
    }
}
