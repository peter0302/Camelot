/***********************************************************************************************
 * © Copyright 2014-2015 Peter Moore. All rights reserved.
 *
 *  This file is part of Camelot.
 *  
 *  Camelot is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *  
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 ***********************************************************************************************/

using System;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Diagnostics;

using Camelot.Core.Internal;

namespace Camelot.Core
{
    public abstract class XamlReader
    {
        protected Hashtable _nsMap;
        protected Hashtable _objectCollection = new Hashtable();
        protected object _eventSink;

        protected Type _CurrentTargetType = null;

        public XamlReader()
        {

        }

        public XamlReader ( object currentValue, Hashtable namespaces, Type templateTarget )
        {
            this.Value = currentValue;
            _nsMap = new Hashtable();
            _CurrentTargetType = templateTarget;
            foreach (var key in namespaces.Keys)
            {
                _nsMap.Add(key, namespaces[key]);
            }
        }


        //public abstract void Load(object target, string xamlFile);
        public abstract void Load(object target, string xamlFile);
        public abstract object Load(string xamlContent, object theObject);
        public abstract object LoadNode(object node);
//        public abstract object Load(Stream xamlFile);

        public object GetElement(string elementName)
        {
            try
            {
                return _objectCollection[elementName];
            }
            catch
            {
                return null;
            }
        }


        public static string AssemblyName
        {
            get
            {
#if __IOS__
                return "Camelot.iOS";
#elif __WIN32__
                return "Camelot.Win32";
#elif __ANDROID__
#elif __WINRT__
#endif
            }
        }

        public void AddInstance(string name, object obj)
        {
            // We don't care if we overwrite an existing object.
            _objectCollection[name] = obj;
        }

        public object Value { get; protected set; }


        internal Type GetTypeFromName (string fullName)
        {
            string prefix = "";
            string name = "";
            if ( fullName.Contains(":"))
            {
                prefix = fullName.LeftOf(':');
                name = fullName.RightOf(':');
            }
            else
            {
                name = fullName;
            }
            return GetTypeFromName(prefix, name);
        }

        /// <summary>
        /// Returns a Type based on the XAML type name, which may or may not be prefixed with a namespace.
        /// </summary>
        /// <param name="name">The name of the type, which may or may not be prefixed.</param>
        /// <returns>A Type object representing the type.</returns>
        internal Type GetTypeFromName(string prefix, string name)
        {
            string ns = prefix == "" ? "XamlDefault" : prefix;
            Trace.Assert(_nsMap.Contains(ns), "Namespace '" + ns + "' has not been declared.");
            string asyName = (string)_nsMap[ns];
            string typeName = asyName.Between(':', ';') + "." + name;
            string assemblyName = asyName.RightOf('=');
            string qname = Assembly.CreateQualifiedName(assemblyName, typeName);
            return Type.GetType(qname, false);
        }

        internal Type GetNodeType(string prefix, string localName, out UniversalPropertyKey propertyInfo)
        {
            // instantiate the clas
            string ns = prefix == "" ? "XamlDefault" : prefix;
            string cname = localName.LeftOf('.');
            Trace.Assert(_nsMap.Contains(ns), "Namespace '" + ns + "' has not been declared.");
            string asyName = (string)_nsMap[ns];
            string typeName = asyName.Between(':', ';') + "." + cname;
            string assemblyName = asyName.RightOf('=');
            string qname = Assembly.CreateQualifiedName(assemblyName, typeName);

            Type t = Type.GetType(qname, false);

            string propertyName = localName.RightOf('.');

            if (propertyName != "")
                propertyInfo = UniversalPropertyKey.Create(t, propertyName);//t.GetProperty(propertyName);
            else
                propertyInfo = null;

            return t;
        }

        protected object EvaluateValue(string value)
        {
            if (IsExtensionExpression(value))
            {
                return ProcessExtensionExpression(value.Between('{','}'));
            }
            else
            {
                return value;
            }
        }

        protected bool IsExtensionExpression(string exp)
        {
            return (exp.StartsWith("{") && exp.EndsWith("}"));
        }

        internal void SetPropertyValueFromString(UniversalPropertyKey pk, string value)
        {
            Type propType = pk.PropertyType;

            if (propType == typeof(DependencyProperty))
            {
                var prop = GetDependencyProperty(this._CurrentTargetType, value);
                pk.SetValue(prop);
                return;
            }
            if (propType == typeof(Type))
            {
                // If type, also easy
                Type typeValue = null;
                if (value.Contains(":"))
                {
                    typeValue = GetTypeFromName(value.LeftOf(':'), value.RightOf(':'));
                }
                else
                {
                    typeValue = GetTypeFromName("", value);
                }
                pk.SetValue(typeValue);
                return;
            }

            object convertedValue = UniversalPropertyKey.ConvertTo(value, propType);
            pk.SetValue(convertedValue);
        }

        internal UniversalPropertyKey GetContentProperty(Type type)
        {
            //Trace.Fail("Direct content not yet supported");
            ContentPropertyAttribute attr = (ContentPropertyAttribute)type.GetTypeInfo().GetCustomAttribute(typeof(ContentPropertyAttribute));
            if (attr == null)
                throw new Exception("Type " + type.Name + " does not support direct content");
            else
                return UniversalPropertyKey.Create(type, attr.Name);//type.GetProperty(attr.Name);

        }


        protected static bool IsSystemType (Type type)
        {
            return (type.GetTypeInfo().Namespace == "System");
        }


        protected object ProcessExtensionExpression(string expression)
        {
            string[] keywords = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string objType = expression.LeftOf(' ');

            string allAttrs = expression.RightOf(' ');
            string[] attrs = allAttrs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            UniversalPropertyKey pi;
            string ns, localName;
            if (objType.Contains(":"))
            {
                ns = objType.LeftOf(':');
                localName = objType.RightOf(':');
            }
            else
            {
                ns = "";
                localName = objType;
            }

            Type t = GetNodeType(ns, localName, out pi);
            object obj = null;

            if ( objType == "x:Type")
            {
                obj = GetTypeFromName(attrs[0]);
                this._CurrentTargetType = (Type)obj;
            }
            else if (attrs.Length == 0)
            {
                obj = Activator.CreateInstance(t);
            }
            else
            {
                foreach (string attribute in attrs)
                {
                    if (attribute.Contains("="))
                    {
                        string pname = attribute.LeftOf('=').RemoveWhitespace(' ');
                        string pvalue = attribute.RightOf('=').RemoveWhitespace(' ');
                        if (obj == null)
                            obj = Activator.CreateInstance(t);
                        ProcessAttribute(obj, "", pname, pvalue, t);
                    }
                    else
                    {
                        // this must be the constructor attribute
                        obj = Activator.CreateInstance(t, new object[] { attribute.RemoveWhitespace(' ') });
                    }
                }
            }

            return obj;
        }




        protected static DependencyProperty GetDependencyProperty ( Type type, string propertyName )
        {
            FieldInfo field = type.GetField(propertyName + "Property", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            if (field != null)
            {
                return (DependencyProperty)field.GetValue(null);
            }
            else
            {
                return null;
            }
        }


        protected static DependencyProperty GetDependencyProperty(object obj, string propertyName)
        {
            return GetDependencyProperty(obj.GetType(), propertyName);
        }


        internal UniversalPropertyKey GetPropertyKey(object parent, string prefix, string propertyName)
        {
            Type t = null;
            UniversalPropertyKey newPk = null;

            if (propertyName.Contains("."))    // attached property
            {
                t = GetTypeFromName(prefix, propertyName.LeftOf('.'));
                newPk = UniversalPropertyKey.Create(t, propertyName.RightOf('.'));
                newPk.Source = parent;
                return newPk;
            }
            else
            {
                return UniversalPropertyKey.Create(parent, propertyName);
            }
        }

        protected void ProcessAttribute(object parent, string prefix, string name, string pvalue, Type t)
        {
            string refName = "";

            //PropertyInfo pi = t.GetProperty(name);

            UniversalPropertyKey pk = GetPropertyKey(parent, prefix, name);
            EventInfo ei = t.GetEvent(name);

            if (name == "xmlns")
            {
                // do nothing
            }
            else if (prefix == "xmlns")
            {
                // do nothing
            }
            else if (pk != null || prefix == "x" || prefix == "d" || prefix == "mc")
            {
                if (name == "Name")
                {
                    refName = pvalue;
                    AddInstance(pvalue, parent);
                }
                if (name == "Class")
                {

                }
                if (pk != null)
                {
                    object objValue;
                    if (pvalue.StartsWith("{") && pvalue.EndsWith("}"))
                    {
                        objValue = ProcessExtensionExpression(pvalue.Substring(1, pvalue.Length - 2));
                        pk.SetValue(objValue);
                    }
                    else if (parent is Style && pk.PropertyName == "TargetType")
                    {
                        if (pvalue.Contains(":"))
                        {
                            _CurrentTargetType = GetTypeFromName(pvalue.LeftOf(':'), pvalue.RightOf(':'));
                        }
                        else
                        {
                            _CurrentTargetType = GetTypeFromName("", pvalue);
                        }
                    }
                    else
                    {
                        SetPropertyValueFromString(pk, pvalue);
                    }
                }
            }
            else if (ei != null)
            {
                Delegate dlgt = null;
                try
                {
                    MethodInfo mi = _eventSink.GetType().GetMethod(pvalue, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    dlgt = Delegate.CreateDelegate(ei.EventHandlerType, _eventSink, mi.Name);
                }
                catch (Exception e)
                {
                    Trace.Fail("Couldn't create a delegate for the event " + pvalue + ":\r\n" + e.Message);
                }

                try
                {
                    ei.AddEventHandler(parent, dlgt);
                }
                catch (Exception e)
                {
                    Trace.Fail("Binding to event " + name + " failed: " + e.Message);
                }
            }
            else
            {
                Trace.Fail("Failed acquiring property information for " + name);
            }
        }
    }
}