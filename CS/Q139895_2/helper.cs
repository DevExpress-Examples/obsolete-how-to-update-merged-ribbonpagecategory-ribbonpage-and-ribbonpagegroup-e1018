using DevExpress.XtraBars.Ribbon;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace DXSample {
    public interface IItemRefreshSupport {
        RibbonPageCategory GetCategory(string text);
        RibbonPage GetPage(string text);
        RibbonPageGroup GetGroup(string text);
        bool IsMerged { get; }
    }

    public enum ItemType { Category, Page, Group }
    public enum ItemRefreshHelperState { Merged, Unmerged }

    public class ItemRefreshHelper {
        private IItemRefreshSupport master;
        private IItemRefreshSupport owner;
        private Dictionary<string, string> items;

        public ItemRefreshHelper(IItemRefreshSupport master, IItemRefreshSupport owner) {
            this.master = master;
            this.owner = owner;
            items = new Dictionary<string, string>();
        }

        public void AddItem(string name, string text) { items.Add(name, text); }

        public void AddItems(CollectionBase collection) {
            if (collection.Count == 0) return;
            RibbonPageCategoryCollection categories = collection as RibbonPageCategoryCollection;
            RibbonPageCollection pages = collection as RibbonPageCollection;
            RibbonPageGroupCollection groups = collection as RibbonPageGroupCollection;
            if (categories != null) foreach (RibbonPageCategory category in categories) items.Add(category.Name, category.Text);
            if (pages != null) foreach (RibbonPage page in pages) items.Add(page.Name, page.Text);
            if (groups != null) foreach (RibbonPageGroup group in groups) items.Add(group.Name, group.Text);
        }

        public void SetItemText(string itemName, string itemText, ItemType itemType) {
            Component item = null;
            if (owner.IsMerged) {
                string oldText = items[itemName];
                if (string.IsNullOrEmpty(oldText)) return;
                else items[itemName] = itemText;
                switch (itemType) {
                    case ItemType.Category: item = master.GetCategory(oldText); break;
                    case ItemType.Group: item = master.GetGroup(oldText); break;
                    case ItemType.Page: item = master.GetPage(oldText); break;
                }
            } else {
                if (!string.IsNullOrEmpty(items[itemName])) items[itemName] = itemText;
                switch (itemType) {
                    case ItemType.Category: item = owner.GetCategory(itemName); break;
                    case ItemType.Group: item = owner.GetGroup(itemName); break;
                    case ItemType.Page: item = owner.GetPage(itemName); break;
                }
            }
            SetItemTextInternal(item, itemText);
        }

        private void SetItemTextInternal(Component item, string text) {
            RibbonPageCategory category = item as RibbonPageCategory;
            RibbonPageGroup group = item as RibbonPageGroup;
            RibbonPage page = item as RibbonPage;
            if (category != null) category.Text = text;
            if (group != null) group.Text = text;
            if (page != null) page.Text = text;
        }
    }
}