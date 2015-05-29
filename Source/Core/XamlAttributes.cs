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

namespace Camelot.Core
{

    internal class XamlReaderLocal
    {
        internal XamlReaderLocal()
        {
            if ( _XamlReaderType == null )
            {
                FindReader();
            }
            _Reader = (XamlReader)Activator.CreateInstance(_XamlReaderType);
        }

        private static Type _XamlReaderType;

        private XamlReader _Reader = null;
        internal XamlReader Reader
        {
            get
            {
                return _Reader;
            }
        }

        private void FindReader()
        {
#if __IOS__
            _XamlReaderType = typeof(Camelot.iOS.IOSXamlReader);
#elif __WIN32__
            _XamlReaderType = typeof(Camelot.Win32.Win32XamlReader);
#endif
        }
    }



    [AttributeUsage(AttributeTargets.Class)]
    public class XamlReaderAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public class ContentPropertyAttribute : Attribute
    {
        public string Name { get; private set; }

        public ContentPropertyAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class XamlResourceLocationAttribute : Attribute
    {
        public string Location { get; private set; }
        public XamlResourceLocationAttribute(string location)
        {
            Location = location;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited=true)]
    public class XamlDeferedLoadAttribute : Attribute
    {
        public XamlDeferedLoadAttribute(Type loaderType, Type contentType)
        {
            this.LoaderType = loaderType;
            this.ContentType = contentType;
            this.LoaderTypeName = loaderType.Name;
            this.ContentTypeName = contentType.Name;
        }



        public Type ContentType { get; private set; }

        public string ContentTypeName { get; private set; }

        public Type LoaderType { get; private set; }

        public string LoaderTypeName { get; private set; }

    }

    public interface ICreateFromString
    {        
    }

}