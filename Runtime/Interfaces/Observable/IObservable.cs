using System;

namespace Commons
{
    /// <summary>
    /// Interface indicating that object that can be changed
    /// </summary>
    /// <example>
    /// <code>
    /// public class MyObservableClass : IObservable
    /// {
    ///     public event Action update;
    /// 
    ///     private int value;
    ///     public int Value
    ///     {
    ///         get => value;
    ///         set
    ///         {
    ///             if(this.value == value) return;
    ///             this.value = value;
    ///             if(Update != null) Update();
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface IObservable
    {
        /// <summary>
        /// Event that is fired when object is changed
        /// </summary>
        public event Action Update;
    }
}