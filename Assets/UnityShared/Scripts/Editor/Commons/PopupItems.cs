using System.Collections.Generic;
using UnityEngine;

namespace UnityShared.Editor.Commons
{
    public class PopupItems
    {
        private List<string> Items { get; set; }

        public string this[int index]
        {
            get => Items[index];
        }
        public int this[string item]
        {
            get => Mathf.Max(Items.IndexOf(item), 0);
        }

        public PopupItems()
        {
            Items = new List<string>();
        }

        public void Add(string item) => Items.Add(item);
        public void Add(string[] items) => Items.AddRange(items);

        public static implicit operator string[](PopupItems arg) => arg.Items.ToArray();
    }
}
