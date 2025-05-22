namespace Commons
{
    
    /// <summary>
    /// Interface indicating that object that can be setup (fed) with model.
    /// </summary>
    /// <typeparam name="TModel">Type of model</typeparam>
    /// <example>
    /// <code>
    /// public class MySetupableClass : ISetupable&lt;int&gt;
    /// {
    ///     public void Setup(int value)
    ///     {
    ///         Debug.Log($"I am setup with model {value}");
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface ISetupable<in TModel>
    {
        /// <summary>
        /// Setups object with model
        /// </summary>
        /// <param name="value"></param>
        void Setup(TModel value);
    }
}