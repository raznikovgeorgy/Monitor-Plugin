using System.Collections.Generic;

namespace Monitor_Plugin
{
    /// <summary>
    /// Reporter of all plugin errors
    /// </summary>
    internal class PluginReporter
    {
        /// <summary>
        /// Delegate after adding error
        /// </summary>
        public delegate void AddedError();

        /// <summary>
        /// Error log event
        /// </summary>
        public event AddedError OnAdd;

        #region Public Methods

        /// <summary>
        /// Creating an instance of a class
        /// </summary>
        /// <returns> Class instance </returns>
        public static PluginReporter Instance()
        {
            if (_instance == null)
            {
                _instance = new PluginReporter();
            }

            return _instance;
        }

        /// <summary>
        /// Adding a new error to the collection
        /// </summary>
        /// <param name="typeError"> Type of new error </param>
        /// <param name="error"> New error </param>
        public void Add(TypeError typeError, string error)
        {
            _errors[typeError] = error;
            _lastAddedError = typeError;
            OnAdd.Invoke();
        }

        /// <summary>
        /// Get last added error
        /// </summary>
        /// <returns> Last added error </returns>
        public string GetLastError()
        {
            return _errors[_lastAddedError];
        }

        /// <summary>
        /// Get all added error
        /// </summary>
        /// <returns> Collection of all errors </returns>
        public Dictionary<TypeError, string> GetAllErrors()
        {
            return _errors;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Last added errors
        /// </summary>
        public TypeError LastAddedError { get => _lastAddedError; }

        #endregion

        #region Fields
        /// <summary>
        /// Instance for plugin
        /// </summary>
        private static PluginReporter _instance;

        /// <summary>
        /// Collection of errors
        /// </summary>
        private Dictionary<TypeError, string> _errors;

        /// <summary>
        /// Last added errors
        /// </summary>
        private TypeError _lastAddedError;

        #endregion

        #region Private Methods

        /// <summary>
        /// Class constructor
        /// </summary>
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

        /// <summary>
        /// New data type to identify errors
        /// </summary>
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