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
using System.Collections.Generic;
using System.ComponentModel;


namespace Camelot.Core
{


	public class Control : FrameworkElement
	{
		public Control() : base()
		{


		}



        
        #region Brush Foreground dependency property
       	public static DependencyProperty ForegroundProperty = DependencyProperty.Register(  "Foreground", typeof(Brush), typeof(Control), new FrameworkPropertyMetadata((Brush)null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
       	public Brush Foreground
       	{
            get
            {
                return (Brush)GetValue(ForegroundProperty);
            }
            set
            {
                SetValue(ForegroundProperty, value);
            }
        }
        #endregion
        
        #region Brush Background dependency property
       	public static DependencyProperty BackgroundProperty = DependencyProperty.Register(  "Background", typeof(Brush), typeof(Control), new FrameworkPropertyMetadata((Brush)null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
       	public Brush Background
       	{
            get
            {
                return (Brush)GetValue(BackgroundProperty);
            }
            set
            {
                SetValue(BackgroundProperty, value);
            }
        }
        #endregion
        
        #region Brush BorderBrush dependency property
       	public static DependencyProperty BorderBrushProperty = DependencyProperty.Register(  "BorderBrush", typeof(Brush), typeof(Control), new FrameworkPropertyMetadata((Brush)null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
       	public Brush BorderBrush
       	{
            get
            {
                return (Brush)GetValue(BorderBrushProperty);
            }
            set
            {
                SetValue(BorderBrushProperty, value);
            }
        }

        #endregion
        
        #region Thickness BorderThickness dependency property
        public static DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(Control), new FrameworkPropertyMetadata((Thickness)new Thickness(0,0,0,0), FrameworkPropertyMetadataOptions.Inherits));
        public Thickness BorderThickness
       	{
            get
            {
                return (Thickness)GetValue(BorderThicknessProperty);
            }
            set
            {
                SetValue(BorderThicknessProperty, value);
            }
        }
        private void OnBorderThicknessChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion
        
        #region FontFamily FontFamily dependency property
        public static DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(Control), new FrameworkPropertyMetadata((FontFamily)new FontFamily("Arial"), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));
        public FontFamily FontFamily
       	{
            get
            {
                return (FontFamily)GetValue(FontFamilyProperty);
            }
            set
            {
                SetValue(FontFamilyProperty, value);
            }
        }

        #endregion
       
        #region double FontSize dependency property
       	public static DependencyProperty FontSizeProperty = DependencyProperty.Register(  "FontSize", typeof(double), typeof(Control), new FrameworkPropertyMetadata((double)12, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure));
       	public double FontSize
       	{
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        #endregion
        
        #region Thickness Padding dependency property
       	public static DependencyProperty PaddingProperty = DependencyProperty.Register(  "Padding", typeof(Thickness), typeof(Control), new FrameworkPropertyMetadata((Thickness)new Thickness(0,0,0,0), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits));
       	public Thickness Padding
       	{
            get
            {
                return (Thickness)GetValue(PaddingProperty);
            }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }
        #endregion
        
        #region ControlTemplate Template dependency property
       	public static DependencyProperty TemplateProperty = DependencyProperty.Register(  "Template", typeof(ControlTemplate), typeof(Control), new FrameworkPropertyMetadata((ControlTemplate)null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                                                               (obj, args) => { ((Control)obj).OnTemplateChanged(args); }));
       	public ControlTemplate Template
       	{
            get
            {
                return (ControlTemplate)GetValue(TemplateProperty);
            }
            set
            {
                SetValue(TemplateProperty, value);
            }
        }
        private void OnTemplateChanged(DependencyPropertyChangedEventArgs args)
        {
            //ClearChildren();
            //template.ApplyTemplate(this);
            //this.AddSubview (content);

            if ( this.IsInitialized && args.NewValue != null )
            {
                _templateApplied = true;
                this.ApplyTemplate((ControlTemplate)args.NewValue);
            }
            else
            {
                _templateApplied = false;
            }
        }
        #endregion


        bool _templateApplied = false;

        internal void ApplyTemplate()
        {
            if ( !_templateApplied )
                this.ApplyTemplate(this.Template);
        }

        private void ApplyTemplate(ControlTemplate template)
        {
            ControlTemplate templ = template;
            FrameworkElement result = templ.Template.Play() as FrameworkElement;
            if (result == null)
                throw new Exception("Root of template must be a FrameworkElement");

            result.TemplatedParent = this;
            this.VisualContent = result;
            OnApplyTemplate();
        }	

		public virtual void OnApplyTemplate ()
		{
		}


        FrameworkElement _VisualContent;
        private FrameworkElement VisualContent
        {
            get { return _VisualContent; }
            set
            {
                if (this.VisualChildrenCount > 0)
                {
                    this.RemoveVisualChild(this.GetVisualChild(0));
                }
                _VisualContent = value;
                this.AddVisualChild(_VisualContent);
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return this.VisualContent != null ? 1 : 0;
            }
        }

        protected override UIElement GetVisualChild(int index)
        {
            if (this.VisualContent == null || index > 0)
                throw new ArgumentOutOfRangeException();
            else
                return this.VisualContent;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }


	}
}

