#pragma checksum "C:\Users\User\Dropbox\4 - Business\Programming Workspace\Sela\Courses\8- Project O-O Design And .NET Framework - Elizur\LibrarySystem\LibrarySystem\CustomersList.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "30D2A2B660823CEE1E3F07EEAA2CE2F0A178B576DC7EA95651E5FA7BB6D4CE76"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibrarySystem
{
    partial class CustomersList : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // CustomersList.xaml line 12
                {
                    this.layoutRoot1 = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 3: // CustomersList.xaml line 37
                {
                    this.stack_panel2 = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 4: // CustomersList.xaml line 44
                {
                    this.ListView2 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.ListView2).SelectionChanged += this.ListView2_SelectionChanged;
                }
                break;
            case 6: // CustomersList.xaml line 14
                {
                    this.Panel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 7: // CustomersList.xaml line 15
                {
                    this.CustPhoneTxt = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8: // CustomersList.xaml line 17
                {
                    this.btnAddCust = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnAddCust).Click += this.btnAddCust_Click;
                }
                break;
            case 9: // CustomersList.xaml line 18
                {
                    this.btnGoBackCustList = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnGoBackCustList).Click += this.btnGoBackCustList_Click;
                }
                break;
            case 10: // CustomersList.xaml line 21
                {
                    this.CustBalanceTxt = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11: // CustomersList.xaml line 22
                {
                    this.btnUpdateCust = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnUpdateCust).Click += this.btnUpdateCust_Click;
                }
                break;
            case 12: // CustomersList.xaml line 23
                {
                    this.btnDeleteCust = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnDeleteCust).Click += this.btnDeleteCust_Click;
                }
                break;
            case 13: // CustomersList.xaml line 25
                {
                    this.txtBoxID = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14: // CustomersList.xaml line 27
                {
                    this.SelectedCustIdTxt = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 15: // CustomersList.xaml line 28
                {
                    this.CustNameTxt = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 16: // CustomersList.xaml line 30
                {
                    this.comboSort = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.comboSort).SelectionChanged += this.comboSort_SelectionChanged;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

