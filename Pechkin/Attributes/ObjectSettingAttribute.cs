﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TuesPechkin.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ObjectSettingAttribute : WkhtmltopdfSettingAttribute
    {
        public ObjectSettingAttribute(string settingName)
        {
            this.SettingName = settingName;
        }

        public override void Apply(IntPtr config, object value)
        {
            Apply(config, value, PechkinStatic.SetObjectSetting);
        }
    }
}