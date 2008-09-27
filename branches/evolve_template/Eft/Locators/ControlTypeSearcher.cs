using System.Collections.Generic;
using System.Windows.Automation;
using Eft.Exception;

namespace Eft.Locators
{
    public class ControlTypeSearcher
    {
        private static Dictionary<string, int> controlTypeMap;

        public static ControlType GetControlType(string controlLocalName)
        {
            if (controlTypeMap == null)
            {
                InitializeTypeMap();
            }
            if (!controlTypeMap.ContainsKey(controlLocalName))
            {
                throw new SelectorTranslationException("Unknown element type " + controlLocalName);
            }
            return ControlType.LookupById(controlTypeMap[controlLocalName]);
        }

        private static void InitializeTypeMap()
        {
            controlTypeMap = new Dictionary<string, int>();
            controlTypeMap.Add("Button", ControlType.Button.Id);
            controlTypeMap.Add("Calendar", ControlType.Calendar.Id);
            controlTypeMap.Add("CheckBox", ControlType.CheckBox.Id);
            controlTypeMap.Add("ComboBox", ControlType.ComboBox.Id);
            controlTypeMap.Add("Custom", ControlType.Custom.Id);
            controlTypeMap.Add("DataGrid", ControlType.DataGrid.Id);
            controlTypeMap.Add("DataItem", ControlType.DataItem.Id);
            controlTypeMap.Add("Document", ControlType.Document.Id);
            controlTypeMap.Add("Edit", ControlType.Edit.Id);
            controlTypeMap.Add("Group", ControlType.Group.Id);
            controlTypeMap.Add("Header", ControlType.Header.Id);
            controlTypeMap.Add("HeaderItem", ControlType.HeaderItem.Id);
            controlTypeMap.Add("Hyperlink", ControlType.Hyperlink.Id);
            controlTypeMap.Add("Image", ControlType.Image.Id);
            controlTypeMap.Add("List", ControlType.List.Id);
            controlTypeMap.Add("ListItem", ControlType.ListItem.Id);
            controlTypeMap.Add("Menu", ControlType.Menu.Id);
            controlTypeMap.Add("MenuBar", ControlType.MenuBar.Id);
            controlTypeMap.Add("MenuItem", ControlType.MenuItem.Id);
            controlTypeMap.Add("Pane", ControlType.Pane.Id);
            controlTypeMap.Add("ProgressBar", ControlType.ProgressBar.Id);
            controlTypeMap.Add("RadioButton", ControlType.RadioButton.Id);
            controlTypeMap.Add("ScrollBar", ControlType.ScrollBar.Id);
            controlTypeMap.Add("Separator", ControlType.Separator.Id);
            controlTypeMap.Add("Slider", ControlType.Slider.Id);
            controlTypeMap.Add("Spinner", ControlType.Spinner.Id);
            controlTypeMap.Add("SplitButton", ControlType.SplitButton.Id);
            controlTypeMap.Add("StatusBar", ControlType.StatusBar.Id);
            controlTypeMap.Add("Tab", ControlType.Tab.Id);
            controlTypeMap.Add("TabItem", ControlType.TabItem.Id);
            controlTypeMap.Add("Table", ControlType.Table.Id);
            controlTypeMap.Add("Thumb", ControlType.Thumb.Id);
            controlTypeMap.Add("Text", ControlType.Text.Id);
            controlTypeMap.Add("TitleBar", ControlType.TitleBar.Id);
            controlTypeMap.Add("ToolBar", ControlType.ToolBar.Id);
            controlTypeMap.Add("ToolTip", ControlType.ToolTip.Id);
            controlTypeMap.Add("Tree", ControlType.Tree.Id);
            controlTypeMap.Add("TreeItem", ControlType.TreeItem.Id);
            controlTypeMap.Add("Window", ControlType.Window.Id);
        }
    }
}