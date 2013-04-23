using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    //https://github.com/gnustep/gnustep-gui/blob/master/Source/NSApplication.m
    public class NSApplication : NSResponder 
    {
        new public static Class Class = new Class(typeof(NSApplication));

        //NSGraphicsContext _default_context;
        NSEvent _current_event;
        //NSModalSession _session;
        NSWindow _key_window;
        NSWindow _main_window;
        id _delegate;
        id _listener;
        NSMenu _main_menu;
        NSMenu _windows_menu;
        // 6 bits
        bool _app_is_launched;
        bool _app_is_running;
        bool _app_is_active;
        bool _app_is_hidden;
        bool _unhide_on_activation;
        bool _windows_need_update;
        NSImage _app_icon;
        NSWindow _app_icon_window;
        NSMutableArray _hidden;
        NSMutableArray _inactive;
        NSWindow _hidden_key;
        NSWindow _hidden_main;
        //GSInfoPanel _infoPanel;



    }
}
