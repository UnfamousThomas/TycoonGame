using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceTycoon/Settings/SettingsContainer")]
public class SettingsContainer : ScriptableObject
{
   [SerializeField]
   public List<AudioTypeSetting> audioTypeSettings;
}