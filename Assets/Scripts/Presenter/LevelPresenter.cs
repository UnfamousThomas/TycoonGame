using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPresenter : MonoBehaviour
{
   public TextMeshProUGUI levelText;
   private void Awake()
   {
      Events.OnLevelChange += onLevelChange;
   }

   private void OnDestroy()
   {
      Events.OnLevelChange -= onLevelChange;
   }

   public void onLevelChange(float level)
   {
      levelText.text = "Level: " + level.ToString();
   }
}
