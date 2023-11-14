using System.Collections.Generic;

namespace Cephei
{
    public static class TypeFinder
    {
        public static OutputType GetObjectByType<InputType, OutputType>(IEnumerable<InputType> collection) 
            where OutputType : InputType
        {
            foreach (var item in collection)
            {
                if (item.GetType() == typeof(OutputType))
                    return (OutputType)item;
            }

            return default;
        }

        public static bool GetObjectByType<InputType, OutputType>(IEnumerable<InputType> collection, out OutputType pattern) 
            where OutputType : InputType
        {
            foreach (var patternlLocal in collection)
            {
                if (patternlLocal.GetType() == typeof(OutputType))
                {
                    pattern = (OutputType)patternlLocal;
                    return true;
                }
            }

            pattern = default;
            return false;
        }

        public static OutputType GetObjectIs<InputType, OutputType>(IEnumerable<InputType> collection) 
            where OutputType : InputType
        {
            foreach (var pattern in collection)
            {
                if (pattern is OutputType)
                    return (OutputType)pattern;
            }

            return default;
        }

        public static bool GetObjectIs<InputType, OutputType>(IEnumerable<InputType> collection, out OutputType pattern) 
            where OutputType : InputType
        {
            foreach (var patternlLocal in collection)
            {
                if (patternlLocal is OutputType)
                {
                    pattern = (OutputType)patternlLocal;
                    return true;
                }
            }

            pattern = default;
            return false;
        }
    }
}
