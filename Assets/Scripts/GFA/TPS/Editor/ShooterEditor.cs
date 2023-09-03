using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GFA.TPS.WeaponSystem;

namespace GFA.TPS.Editor
{
    [CustomEditor(typeof(Shooter))]
    public class ShooterEditor : UnityEditor.Editor
    {
        private Weapon _weapon;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _weapon = EditorGUILayout.ObjectField(_weapon, typeof(Weapon)) as Weapon;



            if (GUILayout.Button("Switch Weapon"))
            {
                var shooter = target as Shooter;
                shooter.EquipWeapon(_weapon);
            }
        }
    }
}