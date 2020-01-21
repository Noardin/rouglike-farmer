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
      Debug.Log("item " + item);
   }
}
