﻿using System.Collections.Generic;

namespace Blazorous.Internal
{
    internal class ThemesProvider : IThemes
    {
        private static readonly ITheme EmptyThemeObj = new Theme("Empty");
        internal static ThemesProvider Instance { get; private set; } = new ThemesProvider();
        
        private readonly IDictionary<string, Theme> _themes = new Dictionary<string, Theme>();

        IRelaxedDictionary<string, ITheme> IThemes.Themes
        {
            get
            {
                IRelaxedDictionary<string, ITheme> result = new RelaxedDictionary<string, ITheme>(EmptyThemeObj);
                foreach (var key in _themes.Keys) result.Add(key, _themes[key]);
                return result;
            }
        }

        public ITheme Current { get; set; }

        public ITheme EmptyTheme => EmptyThemeObj;

        public ITheme CreateTheme(string name)
        {
            var theme = new Theme(name);
            _themes.Add(name, theme);
            return theme;
        }

        public void SetCurrentToEmpty()
        {
            Current = EmptyTheme;
        }
    }
}
