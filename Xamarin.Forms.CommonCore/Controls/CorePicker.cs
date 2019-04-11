using System;
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Xamarin.Forms.CommonCore
{
    //public class CorePickerItem
    //{
    //    public int ID { get; set; }
    //    public string OptionText { get; set; }
    //}
    public class CorePicker : Picker
    {

        public static readonly BindableProperty IsEntryUnderlineProperty =
            BindableProperty.Create("IsEntryUnderline",
                                    typeof(bool),
                                    typeof(CorePicker),
                                    true);


        public bool IsEntryUnderline
        {
            get { return (bool)this.GetValue(IsEntryUnderlineProperty); }
            set { this.SetValue(IsEntryUnderlineProperty, value); }
        }


        public static readonly BindableProperty EmptyDataMessageProperty =
            BindableProperty.Create("EmptyDataMessage",
                                    typeof(string),
                                    typeof(CorePicker),
                                    null);


        public string EmptyDataMessage
        {
            get { return (string)this.GetValue(EmptyDataMessageProperty); }
            set { this.SetValue(EmptyDataMessageProperty, value); }
        }

        public static readonly BindableProperty BindingPathProperty =
            BindableProperty.Create("BindingPath",
                                    typeof(string),
                                    typeof(CorePicker),
                                    string.Empty);
        public string BindingPath
        {
            get { return (string)this.GetValue(BindingPathProperty); }
            set { this.SetValue(BindingPathProperty, value); }
        }

        public static readonly BindableProperty EntryColorProperty =
            BindableProperty.Create("EntryColor",
                                    typeof(Color),
                                    typeof(CorePicker),
                                    Color.Black);
        public Color EntryColor
        {
            get { return (Color)this.GetValue(EntryColorProperty); }
            set { this.SetValue(EntryColorProperty, value); }
        }

        //public CorePicker()
        //{
            
        //    if (Xamarin.Forms.Device.RuntimePlatform =="Android")
        //    {
        //        this.SelectedIndexChanged += SelectedIndexHasChanged;
        //    }
        //    else {
        //        this.Unfocused += OnUnfocused;
        //    }
        //}
        //~CorePicker()
        //{
        //    if (Xamarin.Forms.Device.RuntimePlatform == "Android")
        //    {
        //        this.SelectedIndexChanged -= SelectedIndexHasChanged;
        //    }
        //    else {
        //        this.Unfocused -= OnUnfocused;
        //    }

        //}
        //public void Dispose()
        //{
        //    if (Xamarin.Forms.Device.RuntimePlatform == "Android")
        //    {
        //        this.SelectedIndexChanged -= SelectedIndexHasChanged;
        //    }
        //    else {
        //        this.Unfocused -= OnUnfocused;
        //    }
        //}
        //private void OnUnfocused(object sender, EventArgs args)
        //{
        //    if (this.SelectedIndex == -1)
        //        SelectedItem = null;
        //    else
        //        SelectedItem = ItemsSource[this.SelectedIndex];
        //}
        //private void SelectedIndexHasChanged(object sender, EventArgs args)
        //{
        //    if (this.SelectedIndex == -1)
        //        SelectedItem = null;
        //    else
        //        SelectedItem = ItemsSource[this.SelectedIndex];
        //}


        //private static void OnItemsSourcePropertyChanged(BindableObject bindable, object value, object newValue)
        //{
        //    var picker = (CorePicker)bindable;
        //    var notifyCollection = newValue as INotifyCollectionChanged;
        //    if (notifyCollection != null)
        //    {
        //        notifyCollection.CollectionChanged += (sender, args) =>
        //        {
        //            picker.Items.Clear();
        //            foreach (var item in ItemSourceBindableList(bindable, (IEnumerable)notifyCollection))
        //            {
        //                picker.Items.Add((item ?? "").ToString());
        //            }
        //        };
        //    }

        //    if (newValue == null)
        //        return;



        //    foreach (var item in ItemSourceBindableList(bindable, (IEnumerable)newValue))
        //        picker.Items.Add((item ?? "").ToString());
        //}

        //private static List<string> ItemSourceBindableList(BindableObject bindable, IEnumerable collection)
        //{

        //    var list = new List<string>();
        //    var picker = (CorePicker)bindable;
        //    if (!string.IsNullOrEmpty(picker.BindingPath))
        //    {
        //        var iList = collection as ICollection;

        //        if (collection != null && iList.Count > 0)
        //        {
        //            PropertyInfo prop = null;
        //            foreach (var obj in collection)
        //            {
        //                if (prop == null)
        //                    prop = obj.GetType().GetProperty(picker.BindingPath);

        //                list.Add(prop.GetValue(obj, null).ToString());
        //            }
        //        }
        //    }

        //    return list;

        //}

        //private static void OnSelectedItemPropertyChanged(BindableObject bindable, object value, object newValue)
        //{
        //    var picker = (CorePicker)bindable;
        //    if (picker.ItemsSource != null)
        //        picker.SelectedIndex = picker.ItemsSource.IndexOf(picker.SelectedItem);
        //}
    }
}

