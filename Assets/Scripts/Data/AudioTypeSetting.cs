using UnityEngine;

[CreateAssetMenu(menuName = "SpaceTycoon/Settings/AudioTypeSetting")]
public class AudioTypeSetting : ScriptableObject
{
   [SerializeField]
   public AudioClipType audioType;

   [Range(0f, 1f)] [SerializeField] private float volume = 1;

   public float Volume
   {
      get { return volume; }
      set { volume = value; }
   }
}