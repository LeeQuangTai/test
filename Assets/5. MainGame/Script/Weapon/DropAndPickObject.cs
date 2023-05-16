using Unity.VisualScripting;
using UnityEngine;

namespace Script.Weapon
{
    public static class DropAndPickObject
    {
        private static UserInput _userInput = GameObject.FindGameObjectWithTag("Player").GetComponent<UserInput>();
        public static bool DropWeapon(GameObject currentWeapon, GameObject player = null)
        {
            if (_userInput.dropWeaponPressed)
            {
                currentWeapon.transform.parent = null;
                return true;
            }
            return false;
        }

        public static void PickWeapon(GameObject weapon, GameObject playerWeaponTransform)
        {
            if (_userInput.pickWeaponPressed)
            {
                weapon.transform.parent = playerWeaponTransform.transform;
                weapon.transform.position = playerWeaponTransform.transform.position + _userInput.mousePosition * 1.5f;
                weapon.transform.rotation = Quaternion.Euler(_userInput.aim.Item3);
            }
        }
    }

}