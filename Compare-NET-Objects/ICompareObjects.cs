using System;
using System.Collections.Generic;

namespace KellermanSoftware.CompareNetObjects
{
    /// <summary>
    /// Public interface for mocking.  Mock yeah, bird yeah, yeah yeah.
    /// </summary>
    public interface ICompareObjects
    {
        /// <summary>
        /// Show breadcrumb at each stage of the comparision.  
        /// This is useful for debugging deep object graphs.
        /// The default is false
        /// </summary>
        bool ShowBreadcrumb { get; set; }

        /// <summary>
        /// Ignore classes, properties, or fields by name during the comparison.
        /// Case sensitive.
        /// </summary>
        /// <example>ElementsToIgnore.Add("CreditCardNumber")</example>
        List<string> ElementsToIgnore { get; set; }

        /// <summary>
        /// Only compare elements by name for classes, properties, and fields
        /// Case sensitive.
        /// </summary>
        /// <example>ElementsToInclude.Add("FirstName")</example>
        List<string> ElementsToInclude { get; set; }

        /// <summary>
        /// If true, private properties and fields will be compared. The default is false.  Silverlight and WinRT restricts access to private variables.
        /// </summary>
        bool ComparePrivateProperties { get; set; }

        /// <summary>
        /// If true, private fields will be compared. The default is false.  Silverlight and WinRT restricts access to private variables.
        /// </summary>
        bool ComparePrivateFields { get; set; }

        /// <summary>
        /// If true, static properties will be compared.  The default is true.
        /// </summary>
        bool CompareStaticProperties { get; set; }

        /// <summary>
        /// If true, static fields will be compared.  The default is true.
        /// </summary>
        bool CompareStaticFields { get; set; }

        /// <summary>
        /// If true, child objects will be compared. The default is true. 
        /// If false, and a list or array is compared list items will be compared but not their children.
        /// </summary>
        bool CompareChildren { get; set; }

        /// <summary>
        /// If true, compare read only properties (only the getter is implemented).
        /// The default is true.
        /// </summary>
        bool CompareReadOnly { get; set; }

        /// <summary>
        /// If true, compare fields of a class (see also CompareProperties).
        /// The default is true.
        /// </summary>
        bool CompareFields { get; set; }

        /// <summary>
        /// If true, compare properties of a class (see also CompareFields).
        /// The default is true.
        /// </summary>
        bool CompareProperties { get; set; }

        /// <summary>
        /// The maximum number of differences to detect
        /// </summary>
        /// <remarks>
        /// Default is 1 for performance reasons.
        /// </remarks>
        int MaxDifferences { get; set; }

        /// <summary>
        /// The differences found during the compare
        /// </summary>
        List<Difference> Differences { get; set; }

        /// <summary>
        /// The differences found in a string suitable for a textbox
        /// </summary>
        string DifferencesString { get; }

        /// <summary>
        /// Reflection properties and fields are cached. By default this cache is cleared after each compare.  Set to false to keep the cache for multiple compares.
        /// </summary>
        /// <seealso cref="Caching"/>
        /// <seealso cref="ClearCache"/>
        bool AutoClearCache { get; set; }

        /// <summary>
        /// By default properties and fields for types are cached for each compare.  By default this cache is cleared after each compare.
        /// </summary>
        /// <seealso cref="AutoClearCache"/>
        /// <seealso cref="ClearCache"/>
        bool Caching { get; set; }

        /// <summary>
        /// A list of attributes to ignore a class, property or field
        /// </summary>
        /// <example>AttributesToIgnore.Add(typeof(XmlIgnoreAttribute));</example>
        List<Type> AttributesToIgnore { get; set; }

        /// <summary>
        /// If true, objects will be compared ignore their type diferences
        /// </summary>
        bool IgnoreObjectTypes { get; set; }

        /// <summary>
        /// Func that determine when use CustomComparer for comparing specific type.
        /// Default value return permanent false value.
        /// </summary>
        Func<Type, bool> IsUseCustomTypeComparer { get; set; }

        /// <summary>
        /// Action that performed for comparing objects.
        /// T1: contain current CompareObjects
        /// T2: object1 for comparing
        /// T3: object1 for comparing
        /// T4: current CompareObjects breadcrumb
        /// </summary>
        Action<CompareObjects, object, object, string> CustomComparer { get; set; }

        /// <summary>
        /// In the differences string, this is the name for expected name, default is Expected 
        /// </summary>
        string ExpectedName { get; set; }

        /// <summary>
        /// In the differences string, this is the name for the actual name, default is Actual
        /// </summary>
        string ActualName { get; set; }

        /// <summary>
        /// Compare two objects of the same type to each other.
        /// </summary>
        /// <remarks>
        /// Check the Differences or DifferencesString Properties for the differences.
        /// Default MaxDifferences is 1 for performance
        /// </remarks>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns>True if they are equal</returns>
        bool Compare(object object1, object object2);

        /// <summary>
        /// Reflection properties and fields are cached. By default this cache is cleared automatically after each compare.
        /// </summary>
        /// <seealso cref="CompareObjects.AutoClearCache"/>
        /// <seealso cref="CompareObjects.Caching"/>
        void ClearCache();
    }
}