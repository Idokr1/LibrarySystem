﻿#pragma checksum "C:\Users\User\Dropbox\4 - Business\Programming Workspace\Sela\Courses\8- Project O-O Design And .NET Framework - Elizur\LibrarySystem\LibrarySystem\HomePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BC4A277B9C08497191A2BFDFA3396001A04144E5E52325373CBD0E5F087A3FC8"
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
    partial class HomePage : 
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
            case 2: // HomePage.xaml line 13
                {
                    this.BtnBooks = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnBooks).Click += this.BtnBooks_Click;
                }
                break;
            case 3: // HomePage.xaml line 14
                {
                    this.BtnJournals = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnJournals).Click += this.BtnJournals_Click;
                }
                break;
            case 4: // HomePage.xaml line 15
                {
                    this.BtnIssueReturnBooks = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnIssueReturnBooks).Click += this.BtnIssueReturnBooks_Click;
                }
                break;
            case 5: // HomePage.xaml line 16
                {
                    this.BtnIssueReturnJournals = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnIssueReturnJournals).Click += this.BtnIssueReturnJournals_Click;
                }
                break;
            case 6: // HomePage.xaml line 17
                {
                    this.BtnEmployees = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnEmployees).Click += this.BtnEmployees_Click;
                }
                break;
            case 7: // HomePage.xaml line 18
                {
                    this.BtnCustomers = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnCustomers).Click += this.BtnCustomers_Click;
                }
                break;
            case 8: // HomePage.xaml line 19
                {
                    this.BtnLogout = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnLogout).Click += this.BtnLogout_Click;
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
