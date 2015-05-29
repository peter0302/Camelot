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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.IO;

using Camelot.Core;
using Camelot.Core.Internal;

#if __IOS__
namespace Camelot.iOS
#elif __WIN32__
namespace Camelot.Win32
#endif
{
    [XamlReader]
    public class 
#if __IOS__

        IOSXamlReader 
#elif __WIN32__
        Win32XamlReader
#endif
        : XamlReader
    {
        public 
#if __IOS__
            IOSXamlReader() 
#elif __WIN32__
            Win32XamlReader()
#endif
            : base()
        {

        }

        public 
#if __IOS__
            IOSXamlReader
#elif __WIN32__
            Win32XamlReader
#endif
            (object currentValue, Hashtable namespaces, Type templateTarget ) : base(currentValue, namespaces, templateTarget)
        {

        }



        

        public override void Load(object target, string xamlFile )
        {

//#if __WIN32__
            var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(xamlFile);
            var reader = new StreamReader(stream);
            string xaml = reader.ReadToEnd();
//#elif __IOS__
//            var xaml = System.IO.File.ReadAllText(xamlFile);
//#endif

            this.Load(xaml, target);
        }

        public override object Load(string xamlContent, object theObject)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xamlContent);
            }
            catch
            {
                throw new Exception("Unable to parse XAML file");
            }

            XmlNode node = doc[doc.DocumentElement.Name];
            ProcessNamespaces(node);

            UniversalPropertyKey key;
            Type t = GetNodeType(node, out key);

            if ( theObject == null )
                theObject = Activator.CreateInstance(t);

            _eventSink = theObject;

            if (theObject is ISupportInitialize)
            {
                ((ISupportInitialize)theObject).BeginInit();
            }
            ProcessAttributes(node, theObject, t);
            ProcessChildNodes(node, theObject);
            if (theObject is ISupportInitialize)
            {
                ((ISupportInitialize)theObject).EndInit();
            }
            if (theObject is FrameworkElement)
            {
                ((FrameworkElement)theObject).ApplyAllStyles();
                ((FrameworkElement)theObject).CompleteLoad();
            }
            return theObject;
        }

        /*
        public void Load(XmlDocument doc, object parent)
        {
            this._eventSink = parent;
            this._objectCollection = new Hashtable();
            XmlNode node = doc[doc.DocumentElement.Name];
            ProcessNamespaces(node);
            ProcessAttributes(node, parent, parent.GetType());
            ProcessChildNodes(node, parent);            
            if ( parent is ISupportInitialize )
            {
                ((ISupportInitialize)parent).EndInit();
            }
            if ( parent is FrameworkElement )
            {
                ((FrameworkElement)parent).ApplyAllStyles();
            }
        }
        */
        /*
        public object Load(XmlDocument doc, string objectName, object eventSink)
        {
            this._eventSink = eventSink;
            this._objectCollection = new Hashtable();

            object ret = null;

            XmlNode node = doc[doc.DocumentElement.Name];

            ProcessNamespaces(node);
            ProcessNode(node, out ret);
            return ret;
        }*/


        protected void ProcessNamespaces(XmlNode node)
        {
            _nsMap = new Hashtable();
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Prefix == "xmlns")
                {
                    _nsMap[attr.LocalName] = attr.Value;
                }
                else if (attr.Name == "xmlns")
                {
                    if (attr.Value == "http://schemas.microsoft.com/winfx/2006/xaml/presentation")
                        _nsMap["XamlDefault"] = "clr-namespace:Camelot.Core;assembly=" + XamlReader.AssemblyName;
                    else
                        _nsMap["XamlDefault"] = attr.Value;
                }
            }
        }

        


        public override object LoadNode(object node)
        {
            object result;
            this.ProcessNode((XmlNode)node, out result);
            return result;
        }

        protected XmlAttribute[] ProcessNode(XmlNode node,  out object newObject)
        {
            if ( node is XmlComment )
            {
                newObject = null;
                return null;
            }
            object ret = null;
            XmlAttribute[] xAttributes = null;
            if (node is XmlElement)
            {
                UniversalPropertyKey propertyInfo;
                Type t = GetNodeType(node, out propertyInfo);

                if (t == null)
                    throw new Exception("Type " + node.LocalName + " could not be determined.");
                else if (t == typeof(string))
                {
                    ret = "";
                }
                else
                {
                    try
                    {
                        ret = Activator.CreateInstance(t);
                        if (ret is ISupportInitialize)
                            ((ISupportInitialize)ret).BeginInit();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Type: " + node.LocalName + " could not be created.\n" + e.Message);
                    }
                }

                xAttributes = ProcessAttributes(node, ret, t);

                if (IsSystemType(t))
                {
                    ret = GetSystemTypeNodeValue (node, t);
                }
                else
                {
                    ProcessChildNodes(node, ret);
                }

                if (ret is ISupportInitialize)
                    ((ISupportInitialize)ret).EndInit();
            }
            newObject = ret;            
            return xAttributes;
        }



        private object GetSystemTypeNodeValue (XmlNode node, Type type)
        {
            
            if (node.ChildNodes.Count != 1)
                throw new Exception("System type can only have one content node of direct content");
            XmlNode child = node.ChildNodes[0];
            if (!(child is XmlText))
                throw new Exception("System type content must be text only");

            return UniversalPropertyKey.ConvertTo(child.Value, type);
        }


        private Type GetNodeType(XmlNode node, out UniversalPropertyKey propertyInfo)
        {
            return GetNodeType(node.Prefix, node.LocalName, out propertyInfo);
        }


 

        private void SetSinglePropertyValue( UniversalPropertyKey property, XmlNode value)
        {
            if (value is XmlElement)
            {
                XamlDeferedLoadAttribute attr = null;
                if ((attr = (XamlDeferedLoadAttribute)property.PropertyType.GetCustomAttribute(typeof(XamlDeferedLoadAttribute))) != null)
                {
                    // deferred load; probably a template
                    XamlDeferringLoader loader = (XamlDeferringLoader)Activator.CreateInstance(attr.LoaderType);
                    var reader = new 
#if __WIN32__
                        Win32XamlReader
#elif __IOS__
                        IOSXamlReader
#endif
                            (value, _nsMap, _CurrentTargetType);
                    property.SetValue(loader.Load(reader, null));                  
                }
                else
                {
                    object parsedValue;
                    ProcessNode(value, out parsedValue);
                    property.SetValue(parsedValue);
                }
            }
            else
            {
                SetPropertyValueFromString(property, value.Value);
                //SetSinglePropertyValue(parent, property, value.Value);
            }
        }

        private void SetProperty( UniversalPropertyKey property, XmlNode[] values)
        {
            object propValue = property.GetValue();
            if (propValue is ICollection)
            {
                UniversalPropertyKey key;
                Type t = GetNodeType(values[0], out key);
                if (property.PropertyType == t)
                {
                    SetSinglePropertyValue(property, values[0]);
                }
                else
                {
                    SetCollectionElements(propValue, values);
                }
            }
            else
            {
                if (values.Length > 1)
                    Trace.Fail("Cannot add more than a single item for a non-collection property");
                SetSinglePropertyValue(property, values[0]);
            }            
        }



        XmlAttribute FindKey (XmlAttribute[] attributes)
        {
            foreach (XmlAttribute attr in attributes)
            {
                if (attr.LocalName == "Key")
                    return attr;
            }
            return null;
        }

        protected void SetCollectionElements(object collection, XmlNode[] content)
        {
            foreach (XmlElement item in content)
            {
                object contentItem;
                var xAttributes = ProcessNode(item, out contentItem);

                MethodInfo mi = collection.GetType().GetMethod("Add", new Type[] { collection.GetType() });
                if (mi != null)
                {
                    try
                    {
                        mi.Invoke(collection, new object[] { contentItem });
                    }
                    catch (Exception e)
                    {
                        Trace.Fail("Adding to collection failed:\r\n" + e.Message);
                    }
                }
                else if (collection is IDictionary)
                {
                    var keyAttr = FindKey(xAttributes);
                    if (keyAttr == null)
                        throw new Exception("ResourceDictionary entry must have a key.");
                    ((IDictionary)collection).Add(EvaluateValue(keyAttr.Value), contentItem);
                }
                else if (collection is IList)
                {
                    try
                    {
                        ((IList)collection).Add(contentItem);
                    }
                    catch (Exception e)
                    {
                        Trace.Fail("List/Collection add failed:\r\n" + e.Message);
                    }
                }
            }
                    
        }



        protected void SetDirectContent(object parent, XmlNode[] content)
        {
            if (parent is ICollection)
            {
                SetCollectionElements(parent, content);                
            }
            else
            {
                UniversalPropertyKey contentProperty = GetContentProperty(parent.GetType());
                contentProperty.Source = parent;
                SetProperty(contentProperty, content);                                
            }
        }

        protected void ProcessChildNodes(XmlNode node, object parent)
        {
            if (node.ChildNodes.Count == 0) return;

            int nodeNumber = 0;
            UniversalPropertyKey propertyInfo = null;

            List<XmlNode> contentNodes = new List<XmlNode>();
            foreach ( XmlNode child in node.ChildNodes )
            {
                if (!(child is XmlComment))
                {
                    Type childType = GetNodeType(child, out propertyInfo);
                    if (propertyInfo != null)
                    {
                        // node is a property of the parent
                        propertyInfo.Source = parent;
                        SetProperty(propertyInfo, child.ChildNodes.ToArray());
                        nodeNumber++;
                    }
                    else
                    {
                        // node must be direct content of the parent
                        contentNodes.Add(child);
                    }
                }
            }
            if ( contentNodes.Count > 0 )
                SetDirectContent(parent, contentNodes.ToArray());

            /*

            // TODO: Fix this to support comments; also direct
            // content order versus attributes only an issue
            // when direct content is ICollection
            do
            {
                // properties must come first
                XmlNode child = node.ChildNodes[nodeNumber];
                if (child is XmlElement)
                {
                    Type childType = GetNodeType(child, out propertyInfo);
                    if (propertyInfo != null)
                    {
                        propertyInfo.Source = parent;
                        SetProperty( propertyInfo, child.ChildNodes.ToArray());
                        nodeNumber++;
                    }
                }
            } while (propertyInfo != null && nodeNumber < node.ChildNodes.Count);

            // anything that follows is direct content
            if (nodeNumber == node.ChildNodes.Count) return;
            XmlNode[] directContentNodes = new XmlNode[node.ChildNodes.Count - nodeNumber];
            for (int n = nodeNumber, i = 0; n < node.ChildNodes.Count; n++, i++)
            {
                directContentNodes[i] = node.ChildNodes[n];
            }
            SetDirectContent(parent, directContentNodes);*/
        }





        protected XmlAttribute[] ProcessAttributes(XmlNode node, object ret, Type t)
        {
            List<XmlAttribute> xAttributes = new List<XmlAttribute>();            
            // process attributes
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Prefix == "x")
                {
                    xAttributes.Add(attr);
                }
                ProcessAttribute(ret, attr.Prefix, attr.LocalName, attr.Value, t);
            }
            return xAttributes.ToArray();
        }           


        private struct XamlAttribute
        {
            public string Attribute { get; set; }
            public object Value { get; set; }
        }

    }

    public static class XmlExtensions
    {
        public static XmlNode[] ToArray(this XmlNodeList list)
        {
            XmlNode[] array = new XmlNode[list.Count];
            int i = 0;
            foreach (XmlNode node in list)
            {
                array[i++] = node;
            }
            return array;
        }
    }
}
