namespace ArtPainter
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Contains all members of a ShapeManagerCollection.
    /// </summary>
    public class ShapeManagerCollection : KeyedCollection<string, ShapeManager>
    {
        /// <summary>
        /// Saves the drawing.
        /// </summary>
        /// <param name="path">The path to save.</param>
        /// <param name="collection">The ShapeManagerCollection to serialize.</param>
        public static void Save(string path, ShapeManagerCollection collection)
        {
            using (var sw = new StreamWriter(path))
            {
                var xs = new XmlSerializer(typeof(List<SaveItem>));
                var save = new List<SaveItem>();
                foreach (var manager in collection)
                {
                    save.Add(new SaveItem() 
                    {
                        Name = manager.Name, 
                        Shapes = manager.Shapes.ToList(), 
                        FormBackColor = manager.FormBackGroundColor
                    });
                }

                xs.Serialize(sw, save);
            }
        }

        /// <summary>
        /// Loads the drawing.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>Returns the ShapeManagerCollection.</returns>
        public static ShapeManagerCollection Load(string path)
        {
            using (var sw = new StreamReader(path))
            {
                var xs = new XmlSerializer(typeof(List<SaveItem>));
                var shapeList = (List<SaveItem>)xs.Deserialize(sw);
                var collection = new ShapeManagerCollection();
                foreach (var item in shapeList)
                {
                    var manager = new ShapeManager(item.Name);
                    manager.AddShapes(item.Shapes);
                    manager.FormBackGroundColor = item.FormBackColor;
                    collection.Add(manager);
                }

                return collection;
            }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="item">The value.</param>
        /// <returns>The key.</returns>
        protected override string GetKeyForItem(ShapeManager item)
        {
            return item.Name;
        }

        /// <summary>
        /// The item to save.
        /// </summary>
        [Serializable]
        public class SaveItem
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the shapes to serialize.
            /// </summary>
            public List<Shape> Shapes { get; set; }

            /// <summary>
            /// Gets or sets the form back color of the drawing.
            /// </summary>
            public CustomColor FormBackColor { get; set; }
        }
    }
}
