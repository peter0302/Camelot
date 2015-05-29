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
using System.ComponentModel;


namespace Camelot.Core
{
    /*
	public class AppIconButtonTemplate : ControlTemplate<AppIconButton>
	{
		Ellipse _BackgroundGlyph = new Ellipse();
		UIImageView _IconImageView = new UIImageView();
		UILabel _ButtonLabel = new UILabel();

		public override void ApplyTemplate (AppIconButton templatedParent)
		{
			_BackgroundGlyph = new Ellipse { 
				Width = 44,
				Height = 44,
				Margin = new Thickness(0,-10,0,0),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				BackgroundColor = UIColor.Clear,
				StrokeThickness = 2,
			//	Center = new PointF(templatedParent.Frame.Width / 2, templatedParent.Frame.Height / 2 - 10)
			};			
			templatedParent.Bindings.CreateBinding<Ellipse,UIColor,AppIconButton,bool> (_BackgroundGlyph, "Stroke", templatedParent, "IsSelected", false, 
				(value) => 
				{
					if ( (bool) value )
					{
						return templatedParent.SelectedColor;
					}
					else
					{
						return templatedParent.Foreground;
					}
				});			
					
			_IconImageView = new UIImageView {
				Frame = RectangleF.FromLTRB (0, 0, 32, 32),
				Center = new PointF (templatedParent.Frame.Width / 2, templatedParent.Frame.Height / 2 - 10)};

			templatedParent.Bindings.CreateBinding<Ellipse,UIColor,AppIconButton,bool> (_BackgroundGlyph, "Fill", templatedParent, "IsPressed", false, 
				(value) => 
				{
					if ( (bool)value )
					{
						return templatedParent.Foreground;
					}
					else
					{
						return UIColor.Clear;
					}
				});
			templatedParent.Bindings.CreateBinding<UIImageView,UIImage,AppIconButton,UIImage> (_IconImageView, "Image", templatedParent, "IconImage");

			_ButtonLabel = new UILabel {
				Font = UIFont.FromName("Helvetica", 12),
				BackgroundColor = UIColor.Clear,
				Frame = new RectangleF(0, 50, 50, 25),
				TextAlignment = UITextAlignment.Center,
				Center = new PointF(templatedParent.Frame.Width / 2, templatedParent.Frame.Height / 2 + 20)};
			templatedParent.Bindings.CreateBinding<UILabel,string,AppIconButton,string> (_ButtonLabel, "Text", templatedParent, "Label");
			templatedParent.Bindings.CreateBinding<UILabel,UIColor,AppIconButton,bool> (_ButtonLabel, "TextColor", templatedParent, "IsSelected", false, 
				(value)=>
				{
					if ( (bool)value )
					{
						return templatedParent.SelectedColor;
					}
					else
					{
						return templatedParent.Foreground;
					}
				});

			templatedParent.AddSubview(_BackgroundGlyph);
			templatedParent.AddSubview(_IconImageView);
			templatedParent.AddSubview(_ButtonLabel);
		}


	}

    */


	public class AppIconButton : ButtonBase
    {
//		UIImage _IconImageWhite;
//		UIImage _IconImageBlack;




        public AppIconButton() : base()
        {
            Initialize();
        }


        private void Initialize()
        {

        }


        protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnIsPressedChanged(args);
            if ((bool)args.NewValue)
            {
                //this.IconImage = _IconImageBlack;
            }
            else
            {
                //this.IconImage = _IconImageWhite;
            }
        }

        /*

		private UIImage _IconImage;
		public UIImage IconImage
		{
			get
			{
				return _IconImage;
			}
			set 
			{
				_IconImage = value;
				OnPropertyChanged ("IconImage");
			}
		}*/

        
        #region Brush SelectedColor dependency property
       	public static DependencyProperty SelectedColorProperty = DependencyProperty.Register(  "SelectedColor", typeof(Brush), typeof(AppIconButton), new PropertyMetadata((Brush)null,
                                                               (obj, args) => { ((AppIconButton)obj).OnSelectedColorChanged(args); }));
       	public Brush SelectedColor
       	{
            get
            {
                return (Brush)GetValue(SelectedColorProperty);
            }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }
        private void OnSelectedColorChanged(DependencyPropertyChangedEventArgs args)
        {
            
        }
        #endregion


        
        #region bool IsSelected dependency property
       	public static DependencyProperty IsSelectedProperty = DependencyProperty.Register(  "IsSelected", typeof(bool), typeof(AppIconButton), new PropertyMetadata((bool)false,
                                                               (obj, args) => { ((AppIconButton)obj).OnIsSelectedChanged(args); }));
       	public bool IsSelected
       	{
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }
        private void OnIsSelectedChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion


        
        #region AppButtonIcon Icon dependency property
       	public static DependencyProperty IconProperty = DependencyProperty.Register(  "Icon", typeof(AppButtonIcon), typeof(AppIconButton), new PropertyMetadata((AppButtonIcon)AppButtonIcon.None,
                                                               (obj, args) => { ((AppIconButton)obj).OnIconChanged(args); }));
       	public AppButtonIcon Icon
       	{
            get
            {
                return (AppButtonIcon)GetValue(IconProperty);
            }
            set
            {
                SetValue(IconProperty, value);
            }
        }
        private void OnIconChanged(DependencyPropertyChangedEventArgs args)
        {
            /*
            string iconPathWhite = "Icons/" + _Icon.ToString() + "IconWhite.png";
            string iconPathBlack = "Icons/" + _Icon.ToString() + "IconBlack.png";

            _IconImageBlack = UIImage.FromBundle(iconPathBlack);
            _IconImageWhite = UIImage.FromBundle(iconPathWhite);

            this.IconImage = _IconImageWhite;  */
        }
        #endregion




        
        #region string Label dependency property
       	public static DependencyProperty LabelProperty = DependencyProperty.Register(  "Label", typeof(string), typeof(AppIconButton), new PropertyMetadata((string)"",
                                                               (obj, args) => { ((AppIconButton)obj).OnLabelChanged(args); }));
       	public string Label
       	{
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }
        private void OnLabelChanged(DependencyPropertyChangedEventArgs args)
        {
            // TODO: Add event handler if needed
        }
        #endregion



    }
}
