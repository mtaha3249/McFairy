using System;
using System.Reflection;

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

        /// <summary>
        /// validate a script exists
        /// </summary>
        /// <param name="_namespace">namespace in which script lie</param>
        /// <param name="_classname">classname to validate</param>
        /// <returns>true if found else false</returns>
        public static bool validateScript(string _namespace, string _classname)
        {
            var myClassType = Type.GetType(string.Format("{0}.{1}", _namespace, _classname));
            if (myClassType != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}