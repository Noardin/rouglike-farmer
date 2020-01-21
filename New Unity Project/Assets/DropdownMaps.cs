using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownMaps : MonoBehaviour
{
   public Dropdown dropdown;
   public ImageLoader ImageLoader;

   private void Start()
   {
      PopulateList();
   }

   public void PopulateList()
   {
      List<string> names = ImageLoader.GetMapsList();
      dropdown.options = new List<Dropdown.OptionData>();
      dropdown.AddOptions(names);
   }

   public void ListChoice(int item)
   {
      Dropdown.OptionData data = dropdown.options[item];
      StartCoroutine(ImageLoader.GetImage(data.text));
   }

   public void SetListTo(string filename)
   {
      foreach (var o in dropdown.options)
      {
         if (o.text == filename)
         {
            dropdown.value = o.GetHashCode();
            break;
         }
      }
   }
}
