using System;

namespace McFairy.Base
{
    public class Validator
    {
        /// <summary>
        /// validate script using Reflection for adapters while using namespace and classname
        /// </summary>
        /// <param name="_namespace">namespace to find</param>
        /// <param name="_classname">script to find</param>
        /// <returns>created network if script found otherwise null</returns>
        public static T validateScript<T>(string _namespace, string _classname)
        {
            var myClassType = Type.GetType(string.Format("{0}.{1}", _namespace, _classname));
            if (myClassType != null)
            {
                var inst = Activator.CreateInstance(myClassType);
                return (T)inst;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// validate script using Reflection for adapters while using classname
        /// </summary>
        /// <param name="_classname">script to find</param>
        /// <returns>created network if script found otherwise null</returns>
        public static T validateScript<T>(string _classname)
        {
            var myClassType = Type.GetType(string.Format("{0}", _classname));
            if (myClassType != null)
            {
                var inst = Activator.CreateInstance(myClassType);
                return (T)inst;
            }
            else
            {
                return default(T);
            }
        }
    }
}