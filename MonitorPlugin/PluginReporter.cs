using System.Collections.Generic;

namespace Monitor_Plugin
{
    internal class PluginReporter
    {
        public delegate void AddedError();

        public event AddedError OnAdd;

        #region Public Methods

        public static PluginReporter Instance()
        {
            if (_instance == null)
            {
                _instance = new PluginReporter();
            }

            return _instance;
        }
        public void Add(TypeError typeError, string error)
        {
            _errors[typeError] = error;
            _lastAddedError = typeError;
            OnAdd.Invoke();
        }

        public string GetLastError()
        {
            return _errors[_lastAddedError];
        }

        public Dictionary<TypeError, string> GetAllErrors()
        {
            return _errors;
        }

        #endregion

        #region Properties

        public TypeError LastAddedError { get => _lastAddedError; }

        #endregion

        #region Fields

        private static PluginReporter _instance;
        private Dictionary<TypeError, string> _errors;
        private TypeError _lastAddedError;

        #endregion

        #region Private Methods

        private PluginReporter()
        {
            _errors = new Dictionary<TypeError, string>()
            {
                { TypeError.ErrorAPI, @"" },
                { TypeError.ErrorStandHeight, @"" },
                { TypeError.ErrorStandDiameter, @"" },
                { TypeError.ErrorLegHeight, @"" },
                { TypeError.ErrorLegWidth, @"" },
                { TypeError.ErrorLegThikness, @"" },
                { TypeError.ErrorScreenHeight, @"" },
                { TypeError.ErrorScreenWidth, @"" },
                { TypeError.ErrorScreenThikness, @"" },
            };
        }

        #endregion

        internal enum TypeError
        {
            ErrorAPI = 1,
            ErrorStandHeight,
            ErrorStandDiameter,
            ErrorLegHeight,
            ErrorLegWidth,
            ErrorLegThikness,
            ErrorScreenHeight,
            ErrorScreenWidth,
            ErrorScreenThikness,
        }
    }
}