using System.Reflection;

namespace Eft.Core.Util
{
    public class PrivateAccessor
    {
        public static void SetField(object obj, string fieldName, object value)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            field.SetValue(obj, value);
        }
    }
}