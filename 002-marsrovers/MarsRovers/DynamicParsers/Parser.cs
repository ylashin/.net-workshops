using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarsRoversApp.DynamicParsers
{
    public class DynamicParser
    {
        private IContentProvider ContentProvider;

        public DynamicParser(IContentProvider contentProvider)
        {
            ContentProvider = contentProvider;
        }
        public T Parse<T>(string filePath)
        {
            var obj = Activator.CreateInstance<T>();

            using (var reader = ContentProvider.GetContent(filePath))
            {
                var schemaType = typeof(T);
                var properties = schemaType.GetProperties();

                foreach (var p in properties)
                {
                    var attibutes = p.GetCustomAttributes<FieldAttribute>();
                    if (attibutes != null && attibutes.Count() == 1)
                    {
                        bool isList = p.PropertyType.GetInterfaces()
                            .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>));
                        if (isList && attibutes.First().IsRepeating)
                        {
                            var value = GetRepeatingField(reader, p, attibutes.First());
                            if (value != null)
                            {
                                p.SetValue(obj, value);
                            }
                        }
                        else
                        {
                            var value = GetPropertyValue(reader, p, p.PropertyType, attibutes.First());
                            if (value != null)
                            {
                                p.SetValue(obj, value);
                            }
                        }

                    }
                }
            }
            return obj;
        }

        private IList GetGenericList(Type type)
        {
            Type listType = typeof(List<>).MakeGenericType(new[] { type });
            return (IList)Activator.CreateInstance(listType);
        }
        private object GetRepeatingField(TextReader reader, PropertyInfo p, FieldAttribute fieldAttribute)
        {
            var genericType = p.PropertyType.GenericTypeArguments[0];

            IList result = GetGenericList(genericType);

            while (reader.Peek() != -1)
            {
                var val = GetPropertyValue(reader, p, genericType, fieldAttribute);
                result.Add(val);
            }
            return result;
        }

        private object GetPropertyValue(TextReader reader, PropertyInfo pInfo, Type pType, FieldAttribute fieldAttribute)
        {

            var obj = Activator.CreateInstance(pType);

            var properties = pType.GetProperties();

            var columnFieldType = properties.SelectMany(a => a.CustomAttributes).Any(a => a.AttributeType == typeof(ColumnAttribute));
            var primitiveColumnList = !columnFieldType && pType.GetInterfaces()
                            .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>))
                            && (pType.GenericTypeArguments[0].IsEnum || pType.GenericTypeArguments[0].IsPrimitive);

            if (columnFieldType)
            {
                var line = reader.ReadLine();
                var delimeter = fieldAttribute.Delimeter;
                var parts = line.Split(delimeter);

                foreach (var p in properties)
                {
                    var attibutes = p.GetCustomAttributes<ColumnAttribute>();
                    if (attibutes != null && attibutes.Count() == 1)
                    {
                        var colInfo = attibutes.First();
                        var stringValue = parts[colInfo.ColumnIndex];
                        object value = null;

                        if (p.PropertyType == int.MaxValue.GetType())
                            value = int.Parse(stringValue);
                        if (p.PropertyType == string.Empty.GetType())
                            value = stringValue;

                        if (value != null)
                        {
                            p.SetValue(obj, value);
                        }
                    }
                }
            }
            else if (primitiveColumnList)
            {
                var line = reader.ReadLine();
                var delimeter = fieldAttribute.Delimeter;
                string[] parts = new string[] { };
                if (delimeter != char.MinValue)
                    parts = line.Split(delimeter);
                else
                    parts = line.ToCharArray().Select(a => a.ToString()).ToArray();

                var genType = pType.GenericTypeArguments[0];

                IList result = GetGenericList(genType);
                foreach (var stringValue in parts)
                {
                    object value = null;

                    if (genType == int.MaxValue.GetType())
                        value = int.Parse(stringValue);
                    if (genType == string.Empty.GetType())
                        value = stringValue;
                    if (genType.IsEnum)
                        value = Enum.Parse(genType, stringValue);

                    if (value != null)
                    {
                        result.Add(value);
                    }

                }
                obj = result;
            }
            else
            {
                foreach (var p in properties)
                {
                    var fieldAttibutes = p.GetCustomAttributes<FieldAttribute>();
                    if (fieldAttibutes != null && fieldAttibutes.Count() == 1)
                    {
                        object value = null;

                        var colInfo = fieldAttibutes.First();
                        value = GetPropertyValue(reader, p, p.PropertyType, colInfo);
                        if (value != null)
                        {
                            p.SetValue(obj, value);
                        }
                    }
                }
            }
            return obj;
        }
    }
}
