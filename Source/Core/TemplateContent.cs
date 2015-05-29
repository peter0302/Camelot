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
using System.Xml;

namespace Camelot.Core
{
    /// <summary>
    /// Implements the record and playback logic that templates use for deferring content when they interact with XAML readers and writers.
    /// </summary>
    [XamlDeferedLoad(typeof(TemplateContentLoader), typeof(FrameworkElement))]
    public class TemplateContent
    {
        XamlReader _XamlReader;
        IServiceProvider _ServiceProvider;

        internal TemplateContent()
        {

        }

        internal TemplateContent(XamlReader xamlReader, IServiceProvider serviceProvider)
        {
            _XamlReader = xamlReader;
            _ServiceProvider = serviceProvider;
        }

        public virtual object Play ()
        {
            object result = _XamlReader.LoadNode(_XamlReader.Value);
            if (result is FrameworkElement)
                ((FrameworkElement)result).ApplyAllStyles();
            return result;
        }
    }

    public class TemplateContentLoader : XamlDeferringLoader
    {
        XamlReader _XamlReader;

        public TemplateContentLoader()
        {
                
        }

        public override object Load(XamlReader xamlReader, IServiceProvider serviceProvider)
        {
            return new TemplateContent(xamlReader, serviceProvider);
        }

        public override XamlReader Save(object value, IServiceProvider serviceProvider)
        {
            throw new System.NotImplementedException();
        }
    }
}