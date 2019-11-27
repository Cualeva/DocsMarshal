using System;
using System.Xml.Serialization;

namespace DocsMarshal.Entities.Enums
{
    public enum EPresenterType    {        TextBox = 1,        Listview = 2,        SpinEdit = 3,        Combobox = 4,        DateEdit = 5,        [XmlEnum("DatePicher")]        Datepicker = 6,        LookupEdit = 7,        PopupCalculator = 8,        Calculator = 9,        PasswordEdit = 10,        Default = 0,        CheckBox = 11,        TableBox = 12,        FileBox = 13,        Hidden = 14,        Label = 15,        EmlViewer = 16,        Title = 17,        Tabbed = 18,        HtmlEditor = 19    }
}
