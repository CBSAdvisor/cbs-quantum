using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FFG.Controls.ButtonControl
{
    [ToolboxBitmap(typeof(System.Windows.Forms.Button)), ToolboxItem(true),
    ToolboxItemFilter("System.Windows.Forms"), Description("Displays an button with state images.")]
    public partial class ImageLblButton : Label
    {
        public ButtonMouseState _buttonMouseState = ButtonMouseState.Normal;
        private Image _normalImage = null;
        private Image _hoverImage = null;


        #region Constructors
        public ImageLblButton()
            : base()
        {
            InitializeComponent();
            ButtonMouseState = ButtonMouseState.Normal;
        }

        public ImageLblButton(IContainer container)
            : base()
        {
            container.Add(this);

            InitializeComponent();
            ButtonMouseState = ButtonMouseState.Normal;
        }
        #endregion

        #region Hiding
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image Image { get { return base.Image; } set { base.Image = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int ImageIndex { get { return base.ImageIndex; } set { base.ImageIndex = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string ImageKey { get { return base.ImageKey; } set { base.ImageKey = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageList ImageList { get { return base.ImageList; } set { base.ImageList = value; } }
        #endregion

        #region HoverImage
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("Image to show when the button is hovered over.")]
        public Image HoverImage
        {
            get { return _hoverImage; }
            set { 
                _hoverImage = value;
                if (ButtonMouseState == ButtonMouseState.Hover)
                {
                    Image = value;
                }
            }
        }
        #endregion

        #region NormalImage
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("Image to show when the button is not in any other state.")]
        public Image NormalImage
        {
            get { return _normalImage; }
            set 
            { 
                _normalImage = value;
                if (ButtonMouseState == ButtonMouseState.Normal)
                {
                    Image = value;
                }
            }
        }
        #endregion

        public ButtonMouseState ButtonMouseState 
        {
            get { return _buttonMouseState; }
            set { _buttonMouseState = value; OnButtonMouseState(); }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            ButtonMouseState = ButtonMouseState.Hover;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            ButtonMouseState = ButtonMouseState.Normal;
            base.OnMouseLeave(e);
        }

        private void OnButtonMouseState()
        {
            switch (ButtonMouseState)
            {
                case ButtonMouseState.Normal:
                    if (NormalImage != null)
                    {
                        Image = NormalImage;
                    }
                    break;
                case ButtonMouseState.Hover:
                    if (HoverImage != null)
                    {
                        Image = HoverImage;
                    }
                    break;
                default:
                    if (NormalImage != null)
                    {
                        Image = NormalImage;
                    }
                    break;
            }
        }
    }

    public enum ButtonMouseState
    { 
        Normal,
        Hover,
    }
}
