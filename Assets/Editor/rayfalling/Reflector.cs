using System;
using System.Reflection;

namespace Assets.Editor.rayfalling {
    public static class Reflector {
        public static T InvokeConstructor<T>(this Type type, Type[] paramTypes = null, object[] paramValues = null) {
            return (T) type.InvokeConstructor(paramTypes, paramValues);
        }

        public static object InvokeConstructor(this Type type, Type[] paramTypes = null, object[] paramValues = null) {
            if (paramTypes == null || paramValues == null) {
                paramTypes = new Type[] { };
                paramValues = new object[] { };
            }

            var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null, paramTypes, null);
            return constructor?.Invoke(paramValues);
        }

        public static T Invoke<T>(this object obj, string methodName, params object[] args) {
            var value = obj.Invoke(methodName, args);
            if (value != null) {
                return (T) value;
            }

            return default;
        }

        public static T Invoke<T>(this object obj, string methodName, Type[] types, params object[] args) {
            var value = obj.Invoke(methodName, types, args);
            if (value != null) {
                return (T) value;
            }

            return default(T);
        }

        public static object Invoke(this object obj, string methodName, params object[] args) {
            var types = new Type[args.Length];
            for (var i = 0; i < args.Length; i++)
                types[i] = args[i]?.GetType();

            return obj.Invoke(methodName, types, args);
        }

        public static object Invoke(this object obj, string methodName, Type[] types, params object[] args) {
            var tmpType = obj as Type;
            var type = tmpType ?? obj.GetType();
            var method = type.GetMethod(methodName,
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null,
                CallingConventions.Any, types, null);
            if (method == null)
                method = type.GetMethod(methodName,
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            return method?.Invoke(obj, args);
        }

        public static T GetFieldValue<T>(this object o, string name) {
            var value = o.GetFieldValue(name);
            if (value != null) {
                return (T) value;
            }

            return default;
        }

        public static object GetFieldValue(this object o, string name) {
            var field = o.GetType().GetField(name,
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            return field != null ? field.GetValue(o) : null;
        }

        public static void SetFieldValue(this object o, string name, object value) {
            var field = o.GetType().GetField(name,
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null) {
                field.SetValue(o, value);
            }
        }

        public static T GetPropertyValue<T>(this object o, string name) {
            var value = o.GetPropertyValue(name);
            if (value != null) {
                return (T) value;
            }

            return default;
        }

        public static object GetPropertyValue(this object o, string name) {
            var property = o.GetType().GetProperty(name,
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            return property?.GetValue(o, null);
        }

        public static void SetPropertyValue(this object o, string name, object value) {
            var property = o.GetType().GetProperty(name,
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null) {
                property.SetValue(o, value, null);
            }
        }
    }
}