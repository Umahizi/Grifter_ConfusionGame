﻿

#pragma checksum "C:\Users\Iskra\Documents\Visual Studio 2013\Projects\ConfusionGame\ConfusionGame\ConfusionGame.Shared\GamePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AE92AB906D70B03B3541027573C41E30"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConfusionGame
{
    partial class GamePage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 13 "..\..\GamePage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).ManipulationDelta += this.Rectangle_ManipulationDelta;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 33 "..\..\GamePage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Dificulty_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


