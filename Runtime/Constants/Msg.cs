namespace Commons.Constants
{
    public readonly struct Msg
    {
        #region Default
        
        public const string SPACE = " ";
        public const string BETWEEN_BRACKETS = "[{0}]";
        
        // Errors
        public const string NOT_INITIALIZED = "[{0}] {1} not initialized!";
        public const string VMB_FIELD_NULL = "[{0}] Required variable \"{1}\" is NULL!";

        #endregion
        
        #region Coroutines
        
        public const string COROUTINE_DISPATCHED = "Coroutine Started";
        public const string COROUTINE_TERMINATED = "Coroutine Stopped";

        #endregion
        
        #region Singleton
        
        public const string MONO_SINGLETON_NAME = "[ Singleton <{0}> ]";

        #endregion
        
        #region Object Pool
        
        public const string OBJECT_POOL_PARENT_NAME = "[ POOL <{0}> ]";
        public const string OBJECT_POOL_COUNT_NAME = "{0} {1}/{2}";
        public const string OBJECT_POOL_DISPOSE_NAME = "{0} {1}/{2} ({3}s)";
        
        // Errors
        public const string OBJECT_POOL_TYPE_NOT_SUPPORTED = "[{0}] {1} is not supported! Please create child class for advanced usages.";


        #endregion
        
        #region GameObject Names
        
        public const string UI_LAYER = "[ Layer ] {0}";
        public const string UI_VIEW = "[ View ] {0}";
        public const string NETWORKED_OBJECT = "[ NET ] {0}";

        #endregion
        
        #region Editor

        public const string SETTINGS_FORMAT = "{0} Settings";
        public const string GENERAL_SETTINGS = "General Settings";
        public const string SPECIFIC_SETTINGS = "Specific Settings";

        #endregion
    }
}
