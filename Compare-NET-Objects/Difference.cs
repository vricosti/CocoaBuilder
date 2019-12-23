using System;

namespace KellermanSoftware.CompareNetObjects
{
    /// <summary>
    /// Detailed information about the difference
    /// </summary>
    public class Difference
    {
        /// <summary>
        /// Name of Expected Object
        /// </summary>
        public string ExpectedName { get; set; }

        /// <summary>
        /// Name of Actual Object
        /// </summary>
        public string ActualName { get; set; }

        /// <summary>
        /// The breadcrumb of the property leading up to the value
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The child property name
        /// </summary>
        public string ChildPropertyName { get; set; }

        /// <summary>
        /// Object1 Value
        /// </summary>
        public string Object1Value { get; set; }

        /// <summary>
        /// Object2 Value
        /// </summary>
        public string Object2Value { get; set; }

        /// <summary>
        /// Prefix to put on the beginning of the message
        /// </summary>
        public string MessagePrefix { get; set; }

        /// <summary>
        /// Nicely formatted string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message;

            if (!String.IsNullOrEmpty(PropertyName))
            {
                if (String.IsNullOrEmpty(ChildPropertyName))
                    message = String.Format("{0}.{2} != {1}.{2} ({3},{4})", ExpectedName, ActualName,PropertyName,Object1Value, Object2Value);
                else
                    message = String.Format("{0}.{2}.{5} != {1}.{2}.{5} ({3},{4})", ExpectedName, ActualName, PropertyName, Object1Value, Object2Value, ChildPropertyName);
            }
            else
            {
                message = String.Format("{0} != {1} ({2},{3})", ExpectedName, ActualName, Object1Value, Object2Value);
            }

            if (!String.IsNullOrEmpty(MessagePrefix))
                message = String.Format("{0}: {1}", MessagePrefix, message);

            return message;
        }
    }
}