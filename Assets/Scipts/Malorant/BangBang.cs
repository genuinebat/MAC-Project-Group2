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
        public GameObject hitEffect;
        private bool delayRunning;


        void Start()
        {
            CreateWeapons();
            EquipWeapon(0);
        }

        void CreateWeapons()
        {
            Weapon adware = new Weapon("Adware Gun", Malware.Adware);
            Weapon botware = new Weapon("Botware Gun", Malware.Botware);
            Weapon ransomware = new Weapon("Ransomware Gun", Malware.Ransomware);
            Weapon trojan = new Weapon("Trojan Gun", Malware.Trojan);

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
                Instantiate(hitEffect, hit.transform.position, Quaternion.identity);

                StartCoroutine(DelayShot(hit));



            }
        }

        public void EquipWeapon(int num)
        {
            weaponEquipped = weapons[num];
        }

        IEnumerator DelayShot(RaycastHit hit)
        {
            yield return new WaitForSeconds(0.5f);

            if (hit.transform.tag == "Enemy")
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
}
