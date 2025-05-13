using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadKeyText : MonoBehaviour
{
   private TMP_Text text;

   private void Start()
   {
      text = transform.GetChild(0).GetComponent<TMP_Text>();

      text.text = PlayerPrefs.GetString(gameObject.name, text.text);
   }
}
