namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains all extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Clones a list.
        /// </summary>
        /// <typeparam name="T">The type of the list.</typeparam>
        /// <param name="listToClone">The list to clone.</param>
        /// <returns>Returns the cloned list.</returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
