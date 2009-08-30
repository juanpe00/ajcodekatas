﻿namespace AjLanguage.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IObject
    {
        object GetValue(string name);

        void SetValue(string name, object value);

        ICollection<string> GetNames();
    }
}